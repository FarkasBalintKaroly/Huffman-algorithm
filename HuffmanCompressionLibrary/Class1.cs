using HuffmanCompressionSolution.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuffmanCompressionSolution.Interfaces;

namespace HuffmanCompressionSolution.HuffmanCompressionLibrary
{
    /// <summary>
    /// Provides methods for compressing and decompressing strings using Huffman code table.
    /// </summary>
    public class HuffmanCompressor : ICompressor
    {
        /// <summary>
        /// Compresses the specified input string using the provided Huffman code table.
        /// </summary>
        /// <param name="input">String to compress.</param>
        /// <param name="codeTable">Huffman code table.</param>
        /// <returns>The byte array containing the compressed binary data.</returns>
        /// <example>
        /// <code>
        /// // Huffman code table
        /// Dictionary<char, string> codeTable = newDictionary<char, string>
        /// {
        ///     { 'a', "0" },
        ///     { 'b', "10" },
        ///     { 'c', "110" },
        ///     { 'd', "111" }
        /// };
        /// 
        /// // Input string
        /// string input = "abcd"
        /// 
        /// // Create a compressor instance and compress the input
        /// ICompressor compressor = new HuffmanCompressor();
        /// byte[] compressedData = compressor.Compress(input, codeTable);
        /// </code>
        /// </example>
        public byte[] Compress(string input, Dictionary<char, string> codeTable)
        {
            // Binary code from codeTable
            StringBuilder binaryStringBuilder = new StringBuilder();
            foreach (char c in input)
            {
                binaryStringBuilder.Append(codeTable[c]);
            }
            string binaryString = binaryStringBuilder.ToString();

            // Binary code to byte array
            int byteCount = (binaryString.Length + 7) / 8;
            byte[] compressedData = new byte[byteCount];

            for (int i = 0; i < binaryString.Length; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = i % 8;

                if (binaryString[i] == '1')
                {
                    compressedData[byteIndex] |= (byte)(1 << (7 - bitIndex));
                }
            }

            return compressedData;
        }

        /// <summary>
        /// Decompresses the specified byte array using the Huffman code table.
        /// </summary>
        /// <param name="compressedData">The byte array containing the compressed data.</param>
        /// <param name="codeTable">The Huffman code table.</param>
        /// <returns>The decompressed string.</returns>
        /// <example>
        /// <code>
        /// // Huffman code table
        /// Dictionary<char, string> codeTable = newDictionary<char, string>
        /// {
        ///     { 'a', "0" },
        ///     { 'b', "10" },
        ///     { 'c', "110" },
        ///     { 'd', "111" }
        /// };
        /// 
        /// // Compressed data
        /// byte[] compressedData = new byte[] {0b0101101 0b11000000};
        /// 
        /// // Create a compressor instance and decompress the data
        /// ICompressor compressor = new HuffmanCompressor();
        /// string decompressedText = compressor.Decompress(compressedData, codeTable);
        /// </code>
        /// </example>
        public string Decompress(byte[] compressedData, Dictionary<char, string> codeTable)
        {
            // Byte array to binary chars
            StringBuilder binaryStringBuilder = new StringBuilder();
            foreach (byte b in compressedData)
            {
                binaryStringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            string binaryString = binaryStringBuilder.ToString();

            // Decompress with codeTable
            Dictionary<string, char> reverseCodeTable = new Dictionary<string, char>();
            foreach (var kvp in codeTable)
            {
                reverseCodeTable[kvp.Value] = kvp.Key;
            }

            StringBuilder decompressedText = new StringBuilder();
            string currentCode = "";
            foreach (char bit in binaryString)
            {
                currentCode += bit;
                if (reverseCodeTable.ContainsKey(currentCode))
                {
                    decompressedText.Append(reverseCodeTable[currentCode]);
                    currentCode = "";
                }
            }

            return decompressedText.ToString();
        }
    }
}

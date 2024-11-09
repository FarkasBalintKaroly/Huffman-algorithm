using System;
using System.Collections.Generic;
using System.Text;
using HuffmanCompressionSolution.Interfaces;

namespace HuffmanCompressionSolution.Implementations
{
    public class HuffmanCompressor : ICompressor
    {
        /// <summary>
        /// Compresses the input string using the provided Huffman code table.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="codeTable">The Huffman code table mapping characters to binary codes.</param>
        /// <returns>The compressed binary coded string.</returns>
        public string Compress(string input, Dictionary<char, string> codeTable)
        {
            StringBuilder compressedBuilder = new StringBuilder();

            // Iterating through each character in the input
            foreach (char character in input)
            {
                // Look up the Huffman code for the character in the code table
                if (codeTable.TryGetValue(character, out string code))
                {
                    compressedBuilder.Append(code);
                }
                else
                {
                    throw new ArgumentException($"Character '{character}' not found in code table.");
                }
            }

            return compressedBuilder.ToString();
        }

        /// <summary>
        /// Decompresses the binary coded string using the provided Huffman code table.
        /// </summary>
        /// <param name="compressedData">The compressed binary coded string to decompress.</param>
        /// <param name="codeTable">The Huffman code table mapping characters to binary codes.</param>
        /// <returns>The decompressed original string.</returns>
        public string Decompress(string compressedData, Dictionary<char, string> codeTable)
        {
            // Invert the code table for reverse lookup
            Dictionary<string, char> reverseCodeTable = new Dictionary<string, char>();
            foreach (var kvp in codeTable)
            {
                reverseCodeTable[kvp.Value] = kvp.Key;
            }

            StringBuilder decompressedBuilder = new StringBuilder();
            StringBuilder currentCode = new StringBuilder();

            // Iterating through each bit in the compressed data
            foreach (char bit in compressedData)
            {
                currentCode.Append(bit);

                // Check if the current code matches any character in the reverse table
                if (reverseCodeTable.TryGetValue(currentCode.ToString(), out char decodedChar))
                {
                    decompressedBuilder.Append(decodedChar);
                    currentCode.Clear(); // Reset the code to read the next character
                }
            }

            return decompressedBuilder.ToString();
        }
    }
}

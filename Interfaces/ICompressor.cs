using System.Collections.Generic;

namespace HuffmanCompressionSolution.Interfaces
{
    /// <summary>
    /// Defines the interface for compression algorithms, with methods for compressing
    /// and decompressing data.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Compressing the string input by the codetable.
        /// </summary>
        /// <param name="input">String to compress.</param>
        /// <param name="codeTable">Huffman-codetable for the characters.</param>
        /// <returns>The compressed binary coded string.</returns>
        byte[] Compress(string input, Dictionary<char, string> codeTable);

        /// <summary>
        /// Decompressing the binary coded string by the codetable.
        /// </summary>
        /// <param name="input">Binary coded string to decompress.</param>
        /// <param name="codeTable">Huffman-codetable for the characters.</param>
        /// <returns>The decompressed string.</returns>
        string Decompress(byte[] compressedData, Dictionary<char, string> codeTable);
    }
}
using System.Collections.Generic;

namespace HuffmanCompressionSolution.Interfaces
{
    /// <summary>
    /// Defines the interface for Huffman encoding, including methods for counting character frequency,
    /// generating a Huffman code table, and retrieving the code table.
    /// </summary>
    public interface IHuffmanEncoding
    {
        /// <summary>
        /// Counting characters in the string input.
        /// </summary>
        /// <param name="input">String input to compress.</param>
        void CountCharacterFrequency(string input);

        /// <summary>
        /// Generating the Huffman-codetable.
        /// </summary>
        /// <returns>The codetable, where: key: character, value: binary code.</returns>
        Dictionary<char, string> GenerateHuffmanCodeTable();

        /// <summary>
        /// Returns codetable, what the compressing algorithm can use.
        /// </summary>
        /// <returns>Dictionary which includes characters and codes.</returns>
        Dictionary<char, string> GetHuffmanCodeTable();
    }
}
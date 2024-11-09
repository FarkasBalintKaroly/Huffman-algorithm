using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuffmanCompressionSolution.Interfaces;

namespace HuffmanCompressionSolution.Implementations
{
    /// <summary>
    /// Implements the Huffman encoding, including methods for character frequency counting,
    /// Huffman tree generation, and code table creation.
    /// </summary>
    public class HuffmanEncoder : IHuffmanEncoding
    {
        private Dictionary<char, int> _frequencyTable = new Dictionary<char, int>();
        private Dictionary<char, string> _codeTable = new Dictionary<char, string>();

        /// <summary>
        /// Counts the frequency of each character in the input string.
        /// </summary>
        /// <param name="input">The input string to analyze for character frequencies.</param>
        public void CountCharacterFrequency(string input)
        {
            _frequencyTable.Clear();

            foreach (char character in input)
            {
                if (_frequencyTable.ContainsKey(character))
                    _frequencyTable[character]++;
                else
                    _frequencyTable[character] = 1;
            }
        }

        /// <summary>
        /// Generates the Huffman code table based on the character frequencies.
        /// </summary>
        /// <returns>The Huffman code table, mapping characters to binary codes.</returns>
        public Dictionary<char, string> GenerateHuffmanCodeTable()
        {
            if (_frequencyTable.Count == 0)
                throw new InvalidOperationException("Frequency table is empty. Call CountCharacterFrequency first.");

            // Creating a priority queue to build the Huffman tree
            var priorityQueue = new SortedSet<HuffmanNode>(Comparer<HuffmanNode>.Create((a, b) =>
            {
                int result = a.Frequency.CompareTo(b.Frequency);
                return result == 0 ? a.Character.CompareTo(b.Character) : result;
            }));

            // Initialize the priority queue with leaf nodes based on frequency table
            foreach (var kvp in _frequencyTable)
                priorityQueue.Add(new HuffmanNode(kvp.Key, kvp.Value));

            // Build the Huffman tree
            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue.Min;
                priorityQueue.Remove(left);

                var right = priorityQueue.Min;
                priorityQueue.Remove(right);

                var parent = new HuffmanNode('\0', left.Frequency + right.Frequency, left, right);
                priorityQueue.Add(parent);
            }

            // Get the root of the Huffman tree
            var root = priorityQueue.Min;
            _codeTable.Clear();
            BuildCodeTable(root, "");

            return _codeTable;
        }

        /// <summary>
        /// Returns the Huffman code table.
        /// </summary>
        /// <returns>The code table, mapping characters to binary codes.</returns>
        public Dictionary<char, string> GetHuffmanCodeTable()
        {
            return new Dictionary<char, string>(_codeTable);
        }

        /// <summary>
        /// Recursively builds the code table from the Huffman tree.
        /// </summary>
        /// <param name="node">The current Huffman tree node.</param>
        /// <param name="code">The accumulated binary code.</param>
        private void BuildCodeTable(HuffmanNode node, string code)
        {
            if (node == null)
                return;

            if (node.IsLeaf)
                _codeTable[node.Character] = code;

            BuildCodeTable(node.Left, code + "0");
            BuildCodeTable(node.Right, code + "1");
        }

        /// <summary>
        /// Represents a node in the Huffman tree.
        /// </summary>
        private class HuffmanNode
        {
            public char Character { get; }
            public int Frequency { get; }
            public HuffmanNode Left { get; }
            public HuffmanNode Right { get; }

            public bool IsLeaf => Left == null && Right == null;

            public HuffmanNode(char character, int frequency, HuffmanNode left = null, HuffmanNode right = null)
            {
                Character = character;
                Frequency = frequency;
                Left = left;
                Right = right;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HuffmanCompressionSolution.HuffmanEncodingLibrary;
using HuffmanCompressionSolution.HuffmanCompressionLibrary;
using HuffmanCompressionSolution.Interfaces;

namespace HuffmanCompressionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // input and output files
            string inputFilePath = @"D:\Mernokinfo_msc\Huffman-algorithm\test\input.txt";
            string compressedFilePath = @"D:\Mernokinfo_msc\Huffman-algorithm\test\compressed_output.txt";
            string decompressedFilePath = @"D:\Mernokinfo_msc\Huffman-algorithm\test\decompressed_output.txt";

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"File '{inputFilePath}' nem található.");
                Console.ReadLine();
                return;
            }

            string input = File.ReadAllText(inputFilePath);
            Console.WriteLine("Bemeneti szöveg:");
            Console.WriteLine(input);

            // Character Frequency
            IHuffmanEncoding encoder = new HuffmanEncoder();
            encoder.CountCharacterFrequency(input);

            // Code Table
            Dictionary<char, string> codeTable = encoder.GenerateHuffmanCodeTable();

            Console.WriteLine("\nHuffman kód tábla:");
            foreach (var kvp in codeTable)
            {
                Console.WriteLine($"Karakter: '{kvp.Key}', Kód: {kvp.Value}");
            }

            // Compress
            ICompressor compressor = new HuffmanCompressor();
            byte[] compressedData = compressor.Compress(input, codeTable);

            File.WriteAllBytes(compressedFilePath, compressedData);
            Console.WriteLine($"\nTömörített file: '{compressedFilePath}'");

            // Decompress
            byte[] compressedDataFromFile = File.ReadAllBytes(compressedFilePath);
            string decompressedText = compressor.Decompress(compressedDataFromFile, codeTable);

            Console.WriteLine("\nKitömörített szöveg:");
            Console.WriteLine(decompressedText);

            
            File.WriteAllText(decompressedFilePath, decompressedText);
            Console.WriteLine($"\nKitömörített file: '{decompressedFilePath}'");

            Console.ReadKey();
        }
    }
}

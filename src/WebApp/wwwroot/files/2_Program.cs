using System;
using System.Threading.Tasks;

namespace ArithmeticCoding
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var text = await InputOutput.ReadTextFromFile(InputOutput.TextFile);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Исходный текст:");
            Console.ResetColor();
            Console.WriteLine(text);

            var code = await Encoder.Encode(text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Закодированная строка:");
            Console.ResetColor();
            Console.WriteLine(code);
            
            var codeFromFile = await InputOutput.ReadTextFromFile(InputOutput.CodeFile);
            var textLength = int.Parse(await InputOutput.ReadTextFromFile(InputOutput.TextLengthFile));
            var alphabet = await InputOutput.ReadAlphabetFromFile(InputOutput.AlphabetFile);
            var probabilities = await InputOutput.ReadProbabilitiesFromFile(InputOutput.ProbabilitiesFile);
            var decodedText = await Decoder.Decode(codeFromFile, textLength, alphabet, probabilities);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Декодированный текст:");
            Console.ResetColor();
            Console.WriteLine(decodedText);
            
            Console.ReadKey();
        }
    }
}
using System;
using System.Text;

namespace Files.API
{
    public static class TokenMaker
    {
        private static readonly char[] PossibleSymbols = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private const int SymbolsNumber = 10;

        public static string GetToken()
        {
            var random = new Random();
            var token = new StringBuilder();
            for (var i = 0; i < SymbolsNumber; i++)
            {
                var symbolNumber = random.Next(0, SymbolsNumber - 1);
                token.Append(PossibleSymbols[symbolNumber]);
            }

            return token.ToString();
        }
    }
}
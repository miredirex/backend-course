using System;
using System.IO;

namespace RemoveExtraBlanks
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine($"Incorrect amount of arguments. Required: 2, got: {args.Length}");
                Console.WriteLine("Usage: RemoveExtraBlanks.exe <InputFileName.txt> <OutputFileName.txt>");
                return 1;
            }
            
            try
            {
                using var inputInStream = new StreamReader(args[0]);
                using var outputOutStream = new StreamWriter(args[1]);
                
                RemoveExtraBlanksInStream(inputInStream, outputOutStream);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while opening files: {e.Message}");
                return 1;
            }

            return 0;
        }

        public static void RemoveExtraBlanksInStream(StreamReader source, StreamWriter destination)
        {
            string line;
            while ((line = source.ReadLine()) != null)
            {
                var formattedLine = RemoveExcessSpaces(line);
                destination.WriteLine(formattedLine);
            }
            destination.Flush();
        }

        public static string RemoveExcessSpaces(string str)
        {
            var trimmed = str.Trim();

            var formatted = "";
            for (var i = 0; i < trimmed.Length; i++)
            {
                // 2 spaces in a row
                var isExtraSpaceMet = (i != 0) && (trimmed[i] == ' ') && (trimmed[i - 1] == ' ');
                
                // 2 tabs in a row
                var isExtraTabMet = (i != 0) && (trimmed[i] == '\t') && (trimmed[i - 1] == '\t');
                
                // Append line only when no repeating tabs or spaces are met
                if (!isExtraSpaceMet && !isExtraTabMet)
                {
                    formatted += trimmed[i];
                }
            }

            return formatted;
        }
    }
}
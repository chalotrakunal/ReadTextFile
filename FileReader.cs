using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile
{
    class FileReader
    {
        public int ReadLines(string[] lines)   // function for counting each line in text file
        {
            string[] linesInTextFile = lines;
            List<string> countLines = (from line in linesInTextFile select line).ToList();
            return countLines.Count();
        }
        public int ReadWords(string text)   // function for reading each word in text file
        {
            
            // Split on any whitespace character to correctly handle new lines and tabs
            // Using an empty separator array defaults to all whitespace characters
            string[] wordsInTextFile = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            List<string> countWords = (from word in wordsInTextFile select word).ToList();
            return countWords.Count();
        }
        public int ReadChars(string text)    // for rading each character in the file
        {
            // Exclude common punctuation and whitespace characters from the count
            string[] charsInTextFile = text.Split(new char[] { '.', '?', '!', ';', ':', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<char> countChars = (from word in charsInTextFile
                            select word into words
                            from chars in words
                            select chars).ToList();
            return countChars.Count();
        }
        public struct StoreCounts
        {
            public int countOfLines;
            public int countOfWords;
            public int countOfCharacters;
        };
        public struct StoreCommandArgument
        {
            public List<string> commands;
            public List<string> arguments;
        };
        public void GetdataFromDictionary(Dictionary<string, string> dictOfCommandsArguments) // get data from dictionary
        {
            foreach (KeyValuePair<string, string> item in dictOfCommandsArguments)
            {
                if (item.Key.ToLower().Equals("--input"))
                {
                    try
                    {
                        StoreCounts result = ProcessTextFile(item.Value);  // used exception if the path of file is not correct.
                        DisplayResults(result);
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("File path is not correct please give correct file path as a argument");
                    }
                }
            }
        }
        public Dictionary<string, string> StoreCommandsArguments(List<string> commands, List<string> arguments) //mapping of commands as well as arguments in dictionary
        {
            Dictionary<string, string> storeArgumentsCommand = new Dictionary<string, string>();
            int index = 0;
            while (index < commands.Count())
            {
                string command = commands[index];
                string argument = arguments[index];
                storeArgumentsCommand.Add(command, argument);
                index++;
            } 
            return storeArgumentsCommand;
        }
        public StoreCounts ProcessTextFile(string filePath)    //store the result in tuple so that we can access later
        {
            StoreCounts storeCounts;
            FileReader file = new FileReader();
            string[] lines = File.ReadAllLines(filePath);
            string text = File.ReadAllText(filePath);
            int linesCount = file.ReadLines(lines);
            int wordsCount = file.ReadWords(text);
            int charactersCount = file.ReadChars(text);
            storeCounts.countOfLines = linesCount;
            storeCounts.countOfWords = wordsCount;
            storeCounts.countOfCharacters = charactersCount;
            return storeCounts;   
        }
        public StoreCommandArgument ProcessBothCommandsArguments(string[] args)            
        {
            StoreCommandArgument storeCommandsArguments;
            List<string> commands = (from command in args where command.StartsWith("--") select command).ToList();
            List<string> arguments = (from argument1 in args select argument1).Except(commands).ToList();
            storeCommandsArguments.commands = commands;
            storeCommandsArguments.arguments = arguments;
            return storeCommandsArguments;
        }
        public void ProcessCommandLineArguments( List<string> commandsList, List<string> argumentsList) // Proceesing of command line arguments
        {
            bool isCommandArgumentSameCount = commandsList.Count < argumentsList.Count || commandsList.Count > argumentsList.Count ? false : true;
            if(!isCommandArgumentSameCount)
            {
                Console.WriteLine("The no of commands and arguments does not meet requirements");
            }
            else
            {
                Dictionary<string, string> dictOfCommandsArguments = StoreCommandsArguments(commandsList, argumentsList);
                GetdataFromDictionary(dictOfCommandsArguments);
            }
        }
        public void DisplayResults(StoreCounts result)   // displaying the computed result
        {
            Console.WriteLine("Lines count of text file is : " + result.countOfLines);
            Console.WriteLine("Words count of text file is : " + result.countOfWords);
            Console.WriteLine("Characters count of text file is : " + result.countOfCharacters);
        }
    }
}

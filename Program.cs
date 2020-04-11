using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace ReadTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader reader = new FileReader();
            FileReader.StoreCommandArgument storeCommandsArguments = reader.ProcessBothCommandsArguments(args);
            List<string> commands = storeCommandsArguments.commands;
            List<string> arguments = storeCommandsArguments.arguments;
            try
            {
                arguments.Remove(args[0]); 
                reader.ProcessCommandLineArguments(commands, arguments);
            }
            catch(Exception)
            {
                Console.WriteLine("Please provide proper inputs to the program EXAMPLE: --input abc.txt --output def.txt");
            }   
        }
    }
}


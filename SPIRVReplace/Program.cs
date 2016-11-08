using System;
using System.Configuration;

namespace SPIRVReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                try
                {
                    var inputFile = args[0];
                    var entrypoint = args[1];

                    var reader = new AppSettingsReader();

                    var snr = new SPIRVReplaceUtility();
                    snr.Compiler = (string) reader.GetValue("GLSL_COMPILER", typeof(string));
                    snr.Reassemblier = (string) reader.GetValue("SPIRV_REASSEMBLIER", typeof(string));
                    snr.Disassemblier = (string) reader.GetValue("SPIRV_DISASSEMBLIER", typeof(string));
                    var outputFile = snr.Replace(inputFile, entrypoint);

                    Console.WriteLine("Output file : " + outputFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                GetHelp();
            }
        }

        private static void GetHelp()
        {
            Console.WriteLine("Usage:" + System.AppDomain.CurrentDomain.FriendlyName + " [inputFile] [entrypoint]");
        }
    }
}

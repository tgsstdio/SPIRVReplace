using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SPIRVReplace
{
    class Program
    {
        static int Main(string[] args)
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
                    return 0;
                }
                catch (FileNotFoundException fnfe)
                {
                    Console.WriteLine(fnfe.Message);
                    Console.WriteLine(fnfe.FileName + " not found.");
                    return -1;
                }
                catch (CLIProgramException cli)
                {
                    Console.WriteLine(cli.Message);
                    return -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return -1;
                }
            }
            else
            {
                GetHelp();
                return -1;
            }
        }

        private static void GetHelp()
        {
            string version = "1.0.0.0";
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                version = fvi.FileVersion;
            }
            catch(Exception)
            {
                version = "1.0.0.0";
            }

            Console.WriteLine("Version : " + version);
            Console.WriteLine("Usage:" + System.AppDomain.CurrentDomain.FriendlyName + " [inputFile] [entrypoint]");
        }
    }
}

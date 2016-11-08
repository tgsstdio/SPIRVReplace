using System;
using System.Text;

namespace SPIRVReplace
{
    internal class Disassemblier
    {
        private string inputFile;

        public Disassemblier(string inputFile)
        {
            this.inputFile = inputFile;

            Executable = "spirv-dis.exe";

            // Vulkan shader
            OutputFile = System.IO.Path.GetTempFileName();
        }

        public void Run()
        {
            if (!System.IO.File.Exists(Executable))
            {
                throw new System.IO.FileNotFoundException(nameof(Disassemblier) + " EXECUTABLE", Executable);
            }

            if (!System.IO.File.Exists(inputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(Disassemblier) + " INPUT FILE", inputFile);
            }


            var builder = new StringBuilder();
            builder.AppendFormat(" -o {0} {1}", OutputFile, inputFile);

            var program = new CLIProgram { RedirectStdErr = true };
            var exitCode = program.Run(Executable, builder.ToString());

            if (exitCode != 0)
            {
                throw new CLIProgramException(nameof(Disassemblier) + " error; Early exit.");
            }
        }

        public string OutputFile { get; internal set; }
        public string Executable { get; internal set; }
    }
}
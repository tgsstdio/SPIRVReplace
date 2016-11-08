using System;
using System.Text;

namespace SPIRVReplace
{
    internal class Reassemblier
    {
        private string mInputFile;

        public Reassemblier(string originalFile, string inputFile)
        {
            this.mInputFile = inputFile;

            Executable = "spirv-as.exe";

            // Vulkan shader
            OutputFile = originalFile + ".spv";
        }

        public string Executable { get; internal set; }
        public bool RedirectStdErr { get; internal set; }

        internal void Run()
        {
            if (!System.IO.File.Exists(Executable))
            {
                throw new System.IO.FileNotFoundException(nameof(Reassemblier) + " EXECUTABLE", Executable);
            }

            if (!System.IO.File.Exists(this.mInputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(Reassemblier) + " INPUT FILE", this.mInputFile);
            }

            var builder = new StringBuilder();
            builder.AppendFormat(" -o {0} {1}", OutputFile, mInputFile);


            var program = new CLIProgram { RedirectStdErr = true };
            var exitCode = program.Run(Executable, builder.ToString());
            if (exitCode != 0)
            {
                throw new InvalidOperationException(nameof(Reassemblier) + " error; Early exit.");
            }
        }

        public string OutputFile { get; private set; }
    }
}
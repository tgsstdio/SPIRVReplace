using System.Text;

namespace SPIRVReplace
{
    internal class Disassemblier
    {
        private string inputFile;

        public Disassemblier(string inputFile)
        {
            this.inputFile = inputFile;

            ApplicationName = "spirv-dis.exe";

            // Vulkan shader
            OutputFile = System.IO.Path.ChangeExtension(inputFile, ".b");
        }

        public void Run()
        {
            if (!System.IO.File.Exists(ApplicationName))
            {
                throw new System.IO.FileNotFoundException(nameof(Disassemblier) + " missing", ApplicationName);
            }

            if (!System.IO.File.Exists(inputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(Disassemblier) + " missing", inputFile);
            }


            var builder = new StringBuilder();
            builder.AppendFormat(" -o {0} {1}", OutputFile, inputFile);

            var program = new CLIProgram();
            program.Run(ApplicationName, builder.ToString());
        }

        public string OutputFile { get; internal set; }
        public string ApplicationName { get; internal set; }
    }
}
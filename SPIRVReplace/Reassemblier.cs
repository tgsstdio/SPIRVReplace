using System.Text;

namespace SPIRVReplace
{
    internal class Reassemblier
    {
        private string mInputFile;

        public Reassemblier(string originalFile, string inputFile)
        {
            this.mInputFile = inputFile;

            ApplicationName = "spirv-as.exe";

            // Vulkan shader
            OutputFile = System.IO.Path.ChangeExtension(originalFile, ".spv");
        }

        public string ApplicationName { get; internal set; }
        public bool RedirectStdErr { get; internal set; }

        internal void Run()
        {
            if (!System.IO.File.Exists(ApplicationName))
            {
                throw new System.IO.FileNotFoundException(nameof(Reassemblier) + " missing", ApplicationName);
            }

            if (!System.IO.File.Exists(this.mInputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(Reassemblier) + " missing", this.mInputFile);
            }

            var builder = new StringBuilder();
            builder.AppendFormat(" -o {0} {1}", OutputFile, mInputFile);


            var program = new CLIProgram();
            program.RedirectStdErr = RedirectStdErr;
            program.Run(ApplicationName, builder.ToString());
        }

        public string OutputFile { get; private set; }
    }
}
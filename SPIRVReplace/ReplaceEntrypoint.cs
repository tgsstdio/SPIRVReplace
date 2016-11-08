namespace SPIRVReplace
{
    internal class ReplaceEntrypoint
    {
        private string mEntrypoint;
        private string mInputFile;

        public ReplaceEntrypoint(string inputFile, string entrypoint, ShaderStage stage)
        {
            this.mInputFile = inputFile;
            this.mEntrypoint = entrypoint;
            this.OutputFile = System.IO.Path.ChangeExtension(inputFile, ".c");
        }

        public void Run()
        {
            if (!System.IO.File.Exists(mInputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(ReplaceEntrypoint) + " missing", mInputFile);
            }

            using (var fs = new System.IO.StreamReader(mInputFile))
            using (var sw = new System.IO.StreamWriter(OutputFile))
            {
                var line = fs.ReadLine();
                var token = '"' + mEntrypoint + '"';

                while (line != null)
                {
                    var altered = line.Replace("\"main\"", token);
                    sw.WriteLine(altered);
                    line = fs.ReadLine();
                }

            }
        }

        public string OutputFile { get; internal set; }
    }
}
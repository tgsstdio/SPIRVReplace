using System.Text;

namespace SPIRVReplace
{
    class GLSLCompiler
    {
        private string mInputFile;

        public GLSLCompiler(string inputFile)
        {
            mInputFile = inputFile;
            ApplicationName = "glslangValidator.exe";
            OutputFile = System.IO.Path.ChangeExtension(inputFile, ".a");

            string extension = System.IO.Path.GetExtension(mInputFile);
            switch (extension)
            {
                case ".frag":
                    Stage = ShaderStage.Fragment;
                    break;
                case ".comp":
                    Stage = ShaderStage.Compute;
                    break;
                case ".vert":
                    Stage = ShaderStage.Vertex;
                    break;
                case ".tesc":
                    Stage = ShaderStage.TesselationEvaluation;
                    break;
                case ".tese":
                    Stage = ShaderStage.TesselationEvaluation;
                    break;
                case ".geom":
                    Stage = ShaderStage.Geometry;
                    break;
            }
        }

        public void Run()
        {
            if (!System.IO.File.Exists(ApplicationName))
            {
                throw new System.IO.FileNotFoundException(nameof(GLSLCompiler) + " missing", ApplicationName);
            }

            if (!System.IO.File.Exists(mInputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(GLSLCompiler) + " missing", mInputFile);
            }

            var builder = new StringBuilder();
            // Vulkan shader
            builder.Append("-V");
            builder.AppendFormat(" -o {0} {1}", OutputFile, mInputFile);

            var program = new CLIProgram();
            program.Run(ApplicationName, builder.ToString());
        }


        public string OutputFile { get; internal set; }
        public ShaderStage Stage { get; internal set; }
        public string ApplicationName { get; internal set; }
    }
}

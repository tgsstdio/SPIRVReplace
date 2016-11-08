using System;
using System.Text;

namespace SPIRVReplace
{
    class GLSLCompiler
    {
        private string mInputFile;

        public GLSLCompiler(string inputFile)
        {
            mInputFile = inputFile;
            Executable = "glslangValidator.exe";
            OutputFile = System.IO.Path.GetTempFileName();

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
            if (!System.IO.File.Exists(Executable))
            {
                throw new System.IO.FileNotFoundException(nameof(GLSLCompiler) + " EXECUTABLE", Executable);
            }

            if (!System.IO.File.Exists(mInputFile))
            {
                throw new System.IO.FileNotFoundException(nameof(GLSLCompiler) + " INPUT FILE", mInputFile);
            }

            var builder = new StringBuilder();
            // Vulkan shader
            builder.Append("-V");
            builder.AppendFormat(" -o {0} {1}", OutputFile, mInputFile);

            var program = new CLIProgram {  RedirectStdErr = true };
            var exitCode = program.Run(Executable, builder.ToString());

            if (exitCode != 0)
            {
                throw new CLIProgramException(nameof(GLSLCompiler) + " error; Early exit.");
            }
        }


        public string OutputFile { get; internal set; }
        public ShaderStage Stage { get; internal set; }
        public string Executable { get; internal set; }
    }
}

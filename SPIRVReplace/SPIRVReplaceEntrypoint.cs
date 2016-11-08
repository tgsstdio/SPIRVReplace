using System.Collections.Generic;

namespace SPIRVReplace
{
    public class SPIRVReplaceUtility
    {
        public string Compiler { get; set; }
        public string Disassemblier { get; set; }
        public string Reassemblier { get; set; }

        public string Replace(string inputFile, string entrypoint)
        {
            var tempFiles = new List<string>();

            try
            {
                // COMPILER
                var compiler = new GLSLCompiler(inputFile);
                compiler.Executable = Compiler;
                compiler.Run();
                tempFiles.Add(compiler.OutputFile);

                // DISASSEMBLIER
                var disassemblier = new Disassemblier(compiler.OutputFile);
                disassemblier.Executable = Disassemblier;
                disassemblier.Run();

                tempFiles.Add(disassemblier.OutputFile);

                // SEARCH AND REPLACE 
                var sed = new ReplaceEntrypoint(disassemblier.OutputFile, entrypoint, compiler.Stage);
                sed.Run();

                // REASSEMBLIER
                var reassemblier = new Reassemblier(inputFile, sed.OutputFile);
                reassemblier.Executable = Reassemblier;
                reassemblier.RedirectStdErr = true;
                reassemblier.Run();

                tempFiles.Add(sed.OutputFile);
                
                return reassemblier.OutputFile;
            }
            finally
            {
                foreach(var filePath in tempFiles)
                {
                    System.IO.File.Delete(filePath);
                }
            }

        }
    }
}

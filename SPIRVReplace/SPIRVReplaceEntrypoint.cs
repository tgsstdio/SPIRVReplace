namespace SPIRVReplace
{
    public class SPIRVReplaceUtility
    {
        public string Compiler { get; set; }
        public string Disassemblier { get; set; }
        public string Reassemblier { get; set; }

        public string Replace(string inputFile, string entrypoint)
        {
            // COMPILER
            var compiler = new GLSLCompiler(inputFile);
            compiler.ApplicationName = Compiler;
            compiler.Run();                       

            // DISASSEMBLIER
            var disassemblier = new Disassemblier(compiler.OutputFile);
            disassemblier.ApplicationName = Disassemblier;
            disassemblier.Run();

            // SEARCH AND REPLACE 
            var sed = new ReplaceEntrypoint(disassemblier.OutputFile, entrypoint, compiler.Stage);
            sed.Run();

            // REASSEMBLIER
            var reassemblier = new Reassemblier(inputFile, sed.OutputFile);
            reassemblier.ApplicationName = Reassemblier;
            reassemblier.RedirectStdErr = true;
            reassemblier.Run();


            System.IO.File.Delete(compiler.OutputFile);
            System.IO.File.Delete(disassemblier.OutputFile);
            System.IO.File.Delete(sed.OutputFile);

            return reassemblier.OutputFile;
        }
    }
}

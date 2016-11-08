using System;
using System.Diagnostics;

namespace SPIRVReplace
{
    internal class CLIProgram
    {
        public bool RedirectStdOut { get; internal set; }
        public bool RedirectStdErr { get; internal set; }

        public CLIProgram()
        {
            RedirectStdOut = false;
            RedirectStdErr = false;
        }

        public int Run(string applicationName, string arguments)
        {
            using (Process p = new Process
            {
                StartInfo =

                    {
                        FileName = applicationName,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = true,
						// FOR DECODING
						RedirectStandardOutput = RedirectStdOut,
                        RedirectStandardError = RedirectStdErr

                    },
                EnableRaisingEvents = true

            })
            {
                if (RedirectStdOut)
                    p.OutputDataReceived += P_OutputDataReceived;

                if (RedirectStdErr)
                 p.ErrorDataReceived += P_ErrorDataReceived;

                bool started = p.Start();
                if (!started)
                {
                    //you may allow for the process to be re-used (started = false) 
                    //but I'm not sure about the guarantees of the Exited event in such a case
                    throw new InvalidOperationException("Could not start process: " + p);
                }

                if (RedirectStdOut)
                    p.BeginOutputReadLine();

                if (RedirectStdErr)
                    p.BeginErrorReadLine();

                p.WaitForExit();
                return p.ExitCode;
            }
        }

        private void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine(e.Data);
        }

        private static void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}

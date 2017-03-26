using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.IO;

namespace CellInjection
{
    public class io
    {
        /*
        public static void PrintNodeOnFile(Node nodes)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string OutputFileName = "//node_output.txt";
            string fullPath = path + OutputFileName;

            if (!File.Exists(fullPath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    sw.Write(OutputFileName);
                    sw.Write("\t");
                    DateTime millisTime = DateTime.Now;
                    sw.WriteLine(millisTime);
                }
            }
            using (StreamWriter sw = File.AppendText(fullPath))
            {
                sw.Write("\t");
                sw.Write(nodes.X.X.ToString());
                sw.Write("\t");
                sw.Write(nodes.X.Y.ToString());
                sw.Write("\t");
                sw.WriteLine(nodes.X.Z.ToString());
                sw.Write("\t");
            }
        }
         * */
        public static void PrintNodeOnFile(Node[] nodes)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string OutputFileName = "//node_output.txt";
            string fullPath = path + OutputFileName;

            if (!File.Exists(fullPath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(fullPath))
                {
                    //sw.Write(OutputFileName);
                    //sw.Write("\t");
                    //DateTime millisTime = DateTime.Now;
                    //sw.WriteLine(millisTime);
                }
            }
            for (int i = 0; i < 100; i++)
            {
                using (StreamWriter sw = File.AppendText(fullPath))
                {
                    sw.Write("\t");
                    sw.Write(nodes[i].X.X.ToString());
                    sw.Write("\t");
                    sw.Write(nodes[i].X.Y.ToString());
                    sw.Write("\t");
                    sw.WriteLine(nodes[i].X.Z.ToString());
                    sw.Write("\t");
                }
            }
        }
    }
}

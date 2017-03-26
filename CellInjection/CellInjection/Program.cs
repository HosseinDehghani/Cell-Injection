using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace CellInjection
{
    class Program
    {
        static Node[] nodes;
        static Link[,] links;
        static void Main(string[] args)
        {
            int N=100;
            nodes = new Node[100];
            // allocating nodes' position
            String[] values = readNodesFromFile().Split('\t').ToArray();
            for (int i = 0; i < N;i++ )
            {
                nodes[i] = new Node();
                nodes[i].X.X = Convert.ToDouble(values[i]);
                nodes[i].X.Y = Convert.ToDouble(values[i + N]);
                nodes[i].X.Z = Convert.ToDouble(values[i + 2 * N]);
            }
            // initialing links
            links = new Link[100,100];
            for (int i=0;i<N;i++)
            {
                for (int j=0;j<N;j++)
                {
                    links[i, j] = new Link(nodes[i], nodes[j]);
                }
            }

            // setting BC
            //Vector3D bc1 = new Vector3D(10, 10000, 10);
            //nodes[10].setBoundaryCondition(bc1);
            nodes[10].X.X = 2;

            // setting up the solver
            Solver solution = new Solver();
            solution.node_current = nodes;
            solution.transientLinks = links;
            nodes = solution.solve();

            // printing the results
            io.PrintNodeOnFile(nodes);

            /*
            // set timer to trigger real-time solver
            Timer stepTimer = new Timer();
            stepTimer.Interval = 100;
            stepTimer.Tick += StepTimer_Tick;
            stepTimer.Start();
             * */

        }
        static void StepTimer_Tick(object sender, EventArgs e)
        {



        }
        static string readNodesFromFile()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string OutputFileName = "\\nodes.txt";
            string fullPath = path + OutputFileName;
            string text = System.IO.File.ReadAllText(fullPath);
            return text;
        }
    }
}

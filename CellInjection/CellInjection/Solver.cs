using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CellInjection
{
    class Solver
    {
        public double[] x;
        public Node[] node_current;
        public Node[] node_optimized;
        public Link[,] transientLinks;
        public Solver()
        {
        }
        public Node[] solve()
        {
            // setting up the optimized nodes
            node_optimized = new Node[100];

            // initializing the optimization variables
            x = new double[300];
            for (int i = 0; i < 100; i++)
            {
                x[3 * i] = node_current[i].X.X;
                x[3 * i + 1] = node_current[i].X.Y;
                x[3 * i + 2] = node_current[i].X.Z;
                node_optimized[i] = node_current[i];
            }





            //double[] bndl = new double[] { -90, -90, 0, -180 };
            //double[] bndu = new double[] { 90, 90, 180, 180 };
            double epsg = 0.0000000000001;
            double epsf = 0;
            double epsx = 0;
            int maxits = 0;
            alglib.minlmstate state;
            alglib.minlmreport rep;

            alglib.minlmcreatev(300, x, 0.0000000001, out state);
            //alglib.minlmsetbc(state, bndl, bndu);
            alglib.minlmsetcond(state, epsg, epsf, epsx, maxits);
            alglib.minlmoptimize(state, function_fvec, null, null);
            alglib.minlmresults(state, out x, out rep);

            // updating nodes
            for (int i = 0; i < 100; i++)
            {
                node_optimized[i].X.X = x[3 * i];
                node_optimized[i].X.Y = x[3 * i + 1];
                node_optimized[i].X.Z = x[3 * i + 2];
            }
            return node_optimized;
        }
        private void function_fvec(double[] x, double[] fi, object obj) // to reach a point with closest position and orientation
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3D[] temp = new Vector3D[2];
                temp[0] = new Vector3D(0, 0, 0);
                temp[1] = new Vector3D(0, 0, 0);

                for (int j = 0; j < 100; j++)
                {
                    if (i!=j)
                    {
                        temp[0] = temp[0] + transientLinks[i, j].getForces(node_optimized[i], node_optimized[j])[0];
                        temp[1] = temp[1] + transientLinks[i, j].getForces(node_optimized[i], node_optimized[j])[1];
                    }
                }
                node_optimized[i].setForces(temp);
            }

            for (int i = 0; i < 99; i++)
            {
                fi[3 * i] = node_optimized[i].getResidualForce().X;
                fi[3 * i + 1] = node_optimized[i].getResidualForce().Y;
                fi[3 * i + 2] = node_optimized[i].getResidualForce().Z;
            }
            // updating nodes
            for (int i = 0; i < 100; i++)
            {
                node_optimized[i].X.X = x[3 * i];
                node_optimized[i].X.Y = x[3 * i + 1];
                node_optimized[i].X.Z = x[3 * i + 2];
            }
            Console.WriteLine("{0}   ", fi[10]);
        }
    }
}

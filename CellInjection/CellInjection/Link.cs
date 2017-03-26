using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CellInjection
{
    public class Link
    {
        //public double i;
        //public double j;
        private double k = 10;
        private double b = 10;
        private double l0 = 0;
        private double l = 0;
        private double l_old = 0;
        private Node nodei, nodej;
        private Vector3D u;
        public Link(Node nodei, Node nodej)
        {
            this.nodei = nodei;
            this.nodej = nodej;
            l0 = Math.Sqrt(Math.Pow(nodei.X.X - nodej.X.X, 2) + Math.Pow(nodei.X.Y - nodej.X.Y, 2) + Math.Pow(nodei.X.Z - nodej.X.Z, 2));
            l = l0;
            l_old = l0;
        }
        private void updateLength()
        {
            l_old = l;
            l = Math.Sqrt(Math.Pow(nodei.X.X - nodej.X.X, 2) + Math.Pow(nodei.X.Y - nodej.X.Y, 2) + Math.Pow(nodei.X.Z - nodej.X.Z, 2));
        }
        private void updatedirection()
        {
            u = new Vector3D();
            u.X = nodei.X.X - nodej.X.X;
            u.Y = nodei.X.X - nodej.X.Y;
            u.Z = nodei.X.X - nodej.X.Z;
            u = u / u.Length;
        }
        private Vector3D calculateSpringForce()
        {
            return k * (l - l0) / l0 * u;
        }
        private Vector3D calculateDamperForce()
        {
            return b * (l - l_old) / l0 * u;
        }
        public Vector3D[] getForces(Node nodei, Node nodej)
        {
            this.nodei = nodei;
            this.nodej = nodej;
            updateLength();
            updatedirection();
            Vector3D[] forces=new Vector3D[2];
            forces[0] = calculateSpringForce();
            forces[1] = calculateDamperForce();
            return forces;
        }
    }
}

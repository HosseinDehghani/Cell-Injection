using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CellInjection
{
    public class Node
    {
        public Vector3D X;
        private Vector3D[] F;
        public Node()
        {
            X = new Vector3D();
            F = new Vector3D[3];//fk, fb, f_external
            F[0] = new Vector3D(0, 0, 0);
            F[1] = new Vector3D(0, 0, 0);
            F[2] = new Vector3D(0, 0, 0);
        }
        public Vector3D getResidualForce()
        {
            return F[0] + F[1] - F[2];
        }
        public void setForces(Vector3D[] forces)
        {
            this.F[0] = forces[0];
            this.F[1] = forces[1];
        }
        public void setBoundaryCondition(Vector3D force)
        {
            this.F[2] = force;
        }
    }
}

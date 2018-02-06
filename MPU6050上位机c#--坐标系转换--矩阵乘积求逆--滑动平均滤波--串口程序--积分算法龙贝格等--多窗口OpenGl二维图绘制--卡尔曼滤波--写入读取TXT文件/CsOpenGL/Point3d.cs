using System;
using System.Collections.Generic;
using System.Text;

namespace CsOpenGL
{
    class Point3d
    {

        private float x;
        private float y;
        private float z;
        public Point3d()
        {
        }
        public Point3d(float x,float y ,float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X
        {
            set
            {
                x = value;
            }
            get
            {
                return x;
            }
        }
        public float Y
        {
            set
            {
                y = value;
            }
            get
            {
                return y;
            }
        }
        public float Z
        {
            set
            {
                z = value;
            }
            get
            {
                return z;
            }
        }  
    }
}

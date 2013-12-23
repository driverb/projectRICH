using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Entity
{
    public struct Vector
    {
        public float X;
        public float Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Reset()
        {
            X = Y = 0;
        }

        public bool IsZero()
        {
            return X == Y && X == 0;
        }

        public void Normalize()
        {
            if (X == 0 && Y == 0)
            {
            }
            else if (X == 0)
            {
                Y = 1;
            }
            else if (Y == 0)
            {
                X = 1;
            }
            else
            {
                var n = Math.Sqrt(X * X + Y * Y);
                X = (float)(X / n);
                Y = (float)(Y / n);
            }

        }

        public Vector Multiply(float m)
        {
            return new Vector(X * m, Y * m);
        }

        public float Dot(Vector rhs)
        {
            return X * rhs.X + Y * rhs.Y;
        }

        public Vector Multiply(Vector rhs)
        {
            return new Vector(X * rhs.X, Y * rhs.Y);
        }

        public Vector Add(Vector rhs)
        {
            return new Vector(X + rhs.X, Y + rhs.Y);
        }

        public Vector Diff(Vector rhs)
        {
            return new Vector(X - rhs.X, Y - rhs.Y);
        }
    }
}

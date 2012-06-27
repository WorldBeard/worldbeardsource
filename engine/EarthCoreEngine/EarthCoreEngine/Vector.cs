using System;
using System.Runtime.InteropServices;

namespace EarthCoreEngine
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector
    {
        public static Vector Zero = new Vector(0, 0, 0);

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(Point p) : this(p.X, p.Y) { }

        public Vector(double x, double y)
            : this()
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Vector(double x, double y, double z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(LengthSquared);
            }
        }

        public double LengthSquared
        {
            get
            {
                return (X * X + Y * Y + Z * Z);
            }
        }

        public double Angle2D
        {
            get
            {
                if (X == 0 && Y == 0) return 2 * Math.PI;
                if (X == 0 && Y > 0) return Math.PI / 2;
                if (X == 0 && Y < 0) return 3 * Math.PI / 2;
                if (X > 0 && Y == 0) return 2 * Math.PI;
                if (X < 0 && Y == 0) return Math.PI;
                if (X > 0)
                {
                    if (Y > 0) // 1st quad
                    {
                        return Math.Atan(Y / X);
                    }
                    else // 4th quad
                    {
                        return -1 * Math.Atan(-Y / X);
                    }
                }
                else
                {
                    if (Y > 0) // 2nd quad
                    {
                        return Math.PI - Math.Atan(Y / -X);
                    }
                    else // 3rd quad
                    {
                        return Math.PI + Math.Atan(Y / X);
                    }
                }
            }
        }


        //--------------------------
        // Vector equality method and overloads
        //--------------------------
        public bool Equals(Vector v)
        {
            return (X == v.X) && (Y == v.Y) && (Z == v.Z);
        }

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y ^ (int)Z;
        }

        public static bool operator == (Vector v1, Vector v2)
        {
            // If they're the same object or both null, return true
            if (System.Object.ReferenceEquals(v1, v2))
            {
                return true;
            }

            // If one is null, but not both, return false
            if (v1 == null || v2 == null)
            {
                return false;
            }

            return v1.Equals(v2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                return Equals((Vector)obj);
            }
            return base.Equals(obj);
        }

        public static bool operator != (Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }


        //--------------------------
        // Vector addition and subtraction and scalar multiplication methods and overloads
        //--------------------------
        public Vector Add(Vector r)
        {
            return new Vector(X + r.X, Y + r.Y, Z + r.Z);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return v1.Add(v2);
        }

        public Vector Subtract(Vector r)
        {
            return new Vector(X - r.X, Y - r.Y, Z - r.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return v1.Subtract(v2);
        }

        public Vector Multiply(double scalar)
        {
            return new Vector(X * scalar, Y * scalar, Z * scalar);
        }

        public static Vector operator *(Vector v, double scalar)
        {
            return v.Multiply(scalar);
        }

        public static Vector Normalize(Vector v)
        {
            double r = v.Length;
            if (r != 0.0) // prevent division by zero
            {
                return new Vector(v.X / r, v.Y / r, v.Z / r);
            }
            else
            {
                return new Vector(0, 0, 0);
            }
        }


        //--------------------------
        // Dot product and cross product methods and overload
        //--------------------------
        public double DotProduct(Vector v)
        {
            return (v.X * X) + (Y * v.Y) + (Z * v.Z);
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return v1.DotProduct(v2);
        }

        public Vector CrossProduct(Vector v)
        {
            double nx = Y * v.Z - Z * v.Y;
            double ny = Z * v.X - X * v.Z;
            double nz = X * v.Y - Y * v.X;
            return new Vector(nx, ny, nz);
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", X, Y, Z);
        }

    }
}

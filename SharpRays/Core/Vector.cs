namespace SharpRays.Core {
    using System;

    /// <summary>
    ///     Represents a 3D value container. Used both as a 3D vector and color information
    /// </summary>
    internal struct Vector {
        /// <summary>
        ///     The x-coordinate
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        ///     The y-coordinate
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        ///     The z-coordinate
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        ///     Create a 3D vector with 3 specified operands
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector(double x, double y, double z) {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Create a 3D vector with 1 specified operand. All fields have the same value
        /// </summary>
        /// <param name="s"></param>
        public Vector(double s) : this(s, s, s) {}

        /// <summary>
        ///     Gets the square of the length
        /// </summary>
        public double SquaredLength => Dot(this);

        /// <summary>
        ///     Gets the length of the Vector
        /// </summary>
        public double Length => Math.Sqrt(SquaredLength);

        /// <summary>
        ///     Gets a vector with the same direction, but with a length of 1
        /// </summary>
        public Vector Normalized => this / Length;

        /// <summary>
        ///     Caluclate the dot-product with another vector
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double Dot(Vector other) => X * other.X + Y * other.Y + Z * other.Z;

        /// <summary>
        ///     Apply a function to all operands individually
        /// </summary>
        /// <param name="f">The function to apply</param>
        public void ProcessPiecewise(Func<double, double> f) {
            X = f(X);
            Y = f(Y);
            Z = f(Z);
        }

        /// <summary>
        ///     Reflect a <see cref="Vector" /> around a normal.
        /// </summary>
        /// <param name="v">The <see cref="Vector" /> to reflect.</param>
        /// <param name="n">The normal to reflect around.</param>
        /// <returns>The reflected vector.</returns>
        public Vector Reflect(Vector n) {
            return this - 2 * Dot(n) * n;
        }

        public bool Refract(Vector n, double niOverNt, ref Vector refracted) {
            var uv = Normalized;
            var dt = uv.Dot(n);
            var discriminant = 1 - niOverNt * niOverNt * (1 - dt * dt);
            if (discriminant > 0) {
                refracted = niOverNt * (uv - n * dt) - n * Math.Sqrt(discriminant);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Convert to a string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"{X} {Y} {Z}";
        }

        public static Vector operator -(Vector v) {
            return new Vector(-v.X, -v.Y, -v.Z);
        }

        public static Vector operator +(Vector a, Vector b) {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector operator -(Vector a, Vector b) {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector operator *(Vector a, Vector b) {
            return new Vector(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector operator *(Vector v, double s) {
            return new Vector(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vector operator *(double s, Vector v) {
            return v * s;
        }

        public static Vector operator /(Vector v, double s) {
            return v * (1 / s);
        }
    }
}
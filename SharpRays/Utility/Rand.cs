namespace SharpRays.Utility {
    using System;
    using System.Numerics;

    internal static class Rand {
        private static readonly Random Random = new Random();

        public static float Float => (float) Random.NextDouble();

        public static Vector3 RandomInUnitSphere() {
            Vector3 p;
            do {
                p = 2 * new Vector3(Float, Float, Float) - new Vector3(1);
            } while (p.LengthSquared() >= 1);
            return p;
        }

        public static Vector3 RandomInUnitDisk() {
            Vector3 p;
            do {
                p = 2 * new Vector3(Float, Float, Float);
            } while (Vector3.Dot(p, p) >= 1);
            return p;
        }
    }
}

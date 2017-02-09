namespace SharpRays.Utility {
    using System;

    using SharpRays.Core;

    internal static class Rand {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static double Double => Random.NextDouble();

        public static Vector RandomInUnitSphere {
            get {
                Vector p;
                do {
                    p = 2 * new Vector(Double, Double, Double) - new Vector(1);
                } while (p.SquaredLength >= 1);
                return p;
            }
        }
    }
}
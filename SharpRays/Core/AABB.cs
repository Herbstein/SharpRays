namespace SharpRays.Core {
    using System.Numerics;
    using Utility;

    internal class AABB {
        public Vector3 Max;
        public Vector3 Min;

        public AABB(Vector3 a, Vector3 b) {
            Min = a;
            Max = b;
        }

        public bool Hit(Ray r, float tmin, float tmax) {
            for (var a = 0; a < 3; a++) {
                float invD = 1 / r.Direction.Get(a);
                float t0 = (Min.Get(a) - r.Origin.Get(a)) * invD;
                float t1 = (Max.Get(a) - r.Origin.Get(a)) * invD;
                if (invD < 0) {
                    AABB.FastSwap(ref t0, ref t1);
                }

                tmin = t0 > tmin ? t0 : tmin;
                tmax = t1 < tmax ? t1 : tmax;
                if (tmax <= tmin) {
                    return false;
                }
            }

            return true;
        }

        private static float FastMin(float a, float b) {
            return a < b ? a : b;
        }

        private static float FastMax(float a, float b) {
            return a > b ? a : b;
        }

        private static void FastSwap(ref float a, ref float b) {
            float t = a;
            a = b;
            b = t;
        }

        public static AABB SurroundingBox(AABB box0, AABB box1) {
            var small = new Vector3(AABB.FastMin(box0.Min.X, box1.Min.X),
                                    AABB.FastMin(box0.Min.Y, box1.Min.Y),
                                    AABB.FastMin(box0.Min.Z, box1.Min.Z));
            var big = new Vector3(AABB.FastMax(box0.Max.X, box1.Max.X),
                                  AABB.FastMax(box0.Max.Y, box1.Max.Y),
                                  AABB.FastMax(box0.Max.Z, box1.Max.Z));
            return new AABB(small, big);
        }
    }
}

namespace SharpRays.Hitables {
    using SharpRays.Core;

    internal class HitableList : IHitable {
        private readonly IHitable[] hitables;

        public HitableList(IHitable[] list) {
            hitables = list;
        }

        public bool Hit(Ray r, double tmin, double tmax, ref HitRecord rec) {
            var tempRec = new HitRecord();
            var hitAnything = false;
            var closestSoFar = tmax;

            foreach (var t in hitables) {
                if (t.Hit(r, tmin, closestSoFar, ref tempRec)) {
                    hitAnything = true;
                    closestSoFar = tempRec.T;
                    rec = tempRec;
                }
            }

            return hitAnything;
        }
    }
}
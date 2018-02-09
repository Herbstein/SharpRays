namespace SharpRays.Hitables {
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    internal class HitableList : IHitable {
        private readonly IHitable[] hitables;

        public HitableList(IEnumerable<IHitable> list) {
            hitables = list.ToArray();
        }

        public bool Hit(Ray r, float tmin, float tmax, ref HitRecord rec) {
            var tempRec = new HitRecord();
            var hitAnything = false;
            float closestSoFar = tmax;

            foreach (IHitable t in hitables) {
                if (!t.Hit(r, tmin, closestSoFar, ref tempRec)) {
                    continue;
                }

                hitAnything = true;
                closestSoFar = tempRec.T;
                rec = tempRec;
            }

            return hitAnything;
        }

        public bool BoundingBox(float t0, float t1, out AABB box) {
            if (hitables.Length < 1) {
                box = null;
                return false;
            }

            bool firstTrue = hitables[0].BoundingBox(t0, t1, out AABB tempBox);
            if (!firstTrue) {
                box = null;
                return false;
            }

            box = tempBox;

            for (var i = 1; i < hitables.Length; i++) {
                if (hitables[i].BoundingBox(t0, t1, out tempBox)) {
                    box = AABB.SurroundingBox(box, tempBox);
                } else {
                    return false;
                }
            }

            return true;
        }
    }
}

namespace SharpRays.Hitables {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Utility;

	internal class BVHNode : IHitable {
		public AABB Box;
		public IHitable Left;
		public IHitable Right;

		public BVHNode(List<IHitable> hitables, float time0, float time1) {
			var axis = (int) (Rand.Float * 3);
			switch (axis) {
				case 0:
					// sort x
					hitables.Sort(BoxXCompare);
					break;
				case 1:
					// sort y
					hitables.Sort(BoxYCompare);
					break;
				default:
					// sprt z
					hitables.Sort(BoxZCompare);
					break;
			}

			switch (hitables.Count) {
				case 1:
					Left = Right = hitables[0];
					break;
				case 2:
					Left = hitables[0];
					Right = hitables[1];
					break;
				default:
					Left = new BVHNode(hitables.Take(hitables.Count / 2).ToList(), time0, time1);
					Right = new BVHNode(hitables.Skip(hitables.Count / 2).ToList(), time0, time1);
					break;
			}

			if (!Left.BoundingBox(time0, time1, out var boxLeft) || !Right.BoundingBox(time0, time1, out var boxRight)) {
				throw new Exception("No bounding box in bvh_node constructor!");
			}
			Box = AABB.SurroundingBox(boxRight, boxLeft);
		}

		public bool Hit(Ray r, float tmin, float tmax, ref HitRecord rec) {
			if (Box.Hit(r, tmin, tmax)) {
				HitRecord leftRecord = new HitRecord(), rightRecord = new HitRecord();
				var hitLeft = Left.Hit(r, tmin, tmax, ref leftRecord);
				var hitRight = Right.Hit(r, tmin, tmax, ref rightRecord);
				if (hitLeft && hitRight) {
					rec = leftRecord.T < rightRecord.T ? leftRecord : rightRecord;
					return true;
				}

				if (hitLeft) {
					rec = leftRecord;
					return true;
				}

				if (hitRight) {
					rec = rightRecord;
					return true;
				}

				return false;
			}

			return false;
		}

		public bool BoundingBox(float t0, float t1, out AABB box) {
			box = Box;
			return true;
		}

		private static int BoxXCompare(IHitable a, IHitable b) {
			if (!a.BoundingBox(0, 0, out var boxLeft) || !b.BoundingBox(0, 0, out var boxRight)) {
				throw new Exception("no bounding box in BVHNode constructor\n");
			}

			if (boxLeft.Min.X - boxRight.Min.X < 0) {
				return -1;
			}

			return 1;
		}

		private static int BoxYCompare(IHitable a, IHitable b) {
			if (!a.BoundingBox(0, 0, out var boxLeft) || !b.BoundingBox(0, 0, out var boxRight)) {
				throw new Exception("no bounding box in BVHNode constructor\n");
			}

			if (boxLeft.Min.Y - boxRight.Min.Y < 0) {
				return -1;
			}

			return 1;
		}

		private static int BoxZCompare(IHitable a, IHitable b) {
			if (!a.BoundingBox(0, 0, out var boxLeft) || !b.BoundingBox(0, 0, out var boxRight)) {
				throw new Exception("no bounding box in BVHNode constructor\n");
			}

			if (boxLeft.Min.Z - boxRight.Min.Z < 0) {
				return -1;
			}

			return 1;
		}
	}
}

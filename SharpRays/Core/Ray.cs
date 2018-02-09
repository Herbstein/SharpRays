namespace SharpRays.Core {
    using System.Numerics;

    internal struct Ray {
        public readonly Vector3 Origin;
        public readonly Vector3 Direction;
        public readonly float Time;

        public Ray(Vector3 o, Vector3 d, float t = 0.0f) {
            Origin = o;
            Direction = d;
            Time = t;
        }

        public Vector3 PointAtParameter(float t) {
            return Origin + Direction * t;
        }
    }
}

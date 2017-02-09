namespace SharpRays.Core {
    internal struct Ray {
        public Vector Origin;
        public Vector Direction;

        public Ray(Vector o, Vector d) {
            Origin = o;
            Direction = d;
        }

        public Vector PointAtParameter(double t) => Origin + Direction * t;
    }
}
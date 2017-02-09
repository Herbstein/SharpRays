namespace SharpRays.Core {
    internal class Camera {
        private readonly Vector horizontal;
        private readonly Vector lowerLeftCorner;
        private readonly Vector origin;
        private readonly Vector vertical;

        public Camera() {
            horizontal = new Vector(4, 0, 0);
            lowerLeftCorner = new Vector(-2, -1, -1);
            origin = new Vector(0);
            vertical = new Vector(0, 2, 0);
        }

        public Ray GetRay(double u, double v) => new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical);
    }
}
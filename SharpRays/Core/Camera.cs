namespace SharpRays.Core {
    using System;
    using System.Numerics;
    using Utility;

    internal class Camera {
        private readonly Vector3 horizontal;
        private readonly float lensRadius;
        private readonly Vector3 lowerLeftCorner;
        private readonly Vector3 origin;
        private readonly float Time0, Time1;
        private readonly Vector3 u, v, w;
        private readonly Vector3 vertical;

        public Camera(
            Vector3 lookFrom,
            Vector3 lookAt,
            Vector3 upVector3,
            float verticalFOV,
            float aspectRatio,
            float aperture,
            float focusDistance,
            float t0,
            float t1) {
            Time0 = t0;
            Time1 = t1;
            lensRadius = aperture / 2;
            float theta = verticalFOV * (float) Math.PI / 180;
            var halfHeight = (float) Math.Tan(theta / 2);
            float halfWidth = aspectRatio * halfHeight;
            origin = lookFrom;
            w = Vector3.Normalize(lookFrom - lookAt);
            u = Vector3.Normalize(Vector3.Cross(upVector3, w));
            v = Vector3.Cross(w, u);
            lowerLeftCorner = origin -
                              halfWidth * focusDistance * u -
                              halfHeight * focusDistance * v -
                              focusDistance * w;
            horizontal = 2 * halfWidth * focusDistance * u;
            vertical = 2 * halfHeight * focusDistance * v;
        }

        public Ray GetRay(float s, float t) {
            Vector3 rd = lensRadius * Rand.RandomInUnitDisk();
            Vector3 offset = u * rd.X + v * rd.Y;
            float time = Time0 + Rand.Float * (Time1 - Time0);
            return new Ray(origin + offset,
                           lowerLeftCorner + s * horizontal + t * vertical - origin - offset,
                           time);
        }
    }
}

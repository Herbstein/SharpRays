namespace SharpRays.Utility {
    public static class Extentions {
        public static bool IsWithin(this double val, double min, double max) {
            return (val > min) && (val < max);
        }
    }
}
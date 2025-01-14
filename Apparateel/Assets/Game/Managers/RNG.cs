using System;


public static class RNG {
    private static Random _rng = new Random();

    public static int Next(int minValue, int maxValue) {
            return _rng.Next(minValue, maxValue);
    }

    public static void Reseed(int seed) {
            _rng = new Random(seed);
    }

    public static void Reseed() {
            _rng = new Random();
    }
}

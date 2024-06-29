using UnityEngine;

namespace Nemuge.Core {
    public static class Math {
        public static float GetNoise(Vector2 pos, float noiseVal) {
            return Mathf.PerlinNoise(pos.x * noiseVal, pos.y * noiseVal);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apparateel.Utilities {
    public static class Math {
        public static float Map(float value, float fromMin, float fromMax, float toMin, float toMax) {
            return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
        }
    }
}

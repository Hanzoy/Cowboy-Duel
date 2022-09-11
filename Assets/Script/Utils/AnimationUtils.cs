using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Utils
{
    public class AnimationUtils
    {
        public static double EaseOutBack(double x) {
            double c1 = 1.70158;
            double c3 = c1 + 1;

            return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2);
        }
        
        public static double EaseInOutBack(double x) {
            double c1 = 1.70158;
            double c2 = c1 * 1.525;

            return x < 0.5
                ? (Math.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                : (Math.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }
    }
}

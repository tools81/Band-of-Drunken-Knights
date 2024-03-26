using System;

namespace Utility
{
    public static class MathUtil
    {
        public static bool ApproximateEqual(double value1, double value2, double acceptableDifference)
        {
            return Math.Abs(value1 - value2) <= acceptableDifference;
        }
    }
}
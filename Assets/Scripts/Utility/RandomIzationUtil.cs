using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class RandomizationUtil
    {
        public static TItem GetRandom<TItem>(TItem[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static bool GetRandom(float chance)
        {
            return Random.value <= chance;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2 FindIndex2D<T>(this T[,] array, T item) {
        for (int row = 0; row < array.GetLength(0); row++) {
            for (int column = 0; column < array.GetLength(1); column++) {
                if (!EqualityComparer<T>.Default.Equals(array[row, column], item))
                    return new Vector2(row, column);
            }
        }
        throw new System.Exception("Item could not be found in array");
    }
}

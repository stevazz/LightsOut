using System;

namespace LightsOut.Utility
{
    public static class MultiDimensionalArrayExtensions
    {
        public static T[][] ToJaggedArray<T>(this T[,] twoDimentionalArray)
        {
            var jaggedArray = new T[twoDimentionalArray.GetLength(0)][];

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new T[twoDimentionalArray.GetLength(1)];
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    jaggedArray[i][j] = twoDimentionalArray[i, j];
                }
            }

            return jaggedArray;
        }

        public static T[,] ToTwoDimensionalArray<T>(this T[][] jaggedArray)
        {
            var twoDimentionalArray = new T[jaggedArray.Length, jaggedArray[0].Length];

            for (int i = 0; i < twoDimentionalArray.GetLength(0); i++)
            {
                for (int j = 0; j < twoDimentionalArray.GetLength(1); j++)
                {
                    twoDimentionalArray[i, j] = jaggedArray[i][j];
                }
            }

            return twoDimentionalArray;
        }
    }
}

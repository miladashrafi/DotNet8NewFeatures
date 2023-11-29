using System.Collections.ObjectModel;

namespace DotNet8NewFeatures.Models
{
    public class CollectionExpressionModel
    {
        public void NewExpressions()
        {
            // Create an array:
            int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

            // Create a list:
            List<string> b = ["one", "two", "three"];

            // Create a span
            Span<int> c = [1, 2, 3, 4, 5, 6, 7, 8];

            // Create a jagged 2D array:
            int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

            // Create a jagged 2D array from variables:
            int[] row0 = [1, 2, 3];
            int[] row1 = [4, 5, 6];
            int[] row2 = [7, 8, 9];
            int[][] twoDFromVariables = [row0, row1, row2];

            int[] single = [.. row0, .. row1, .. row2];
            foreach (var element in single)
            {
                Console.Write($"{element}, ");
            }
            // output:
            // 1, 2, 3, 4, 5, 6, 7, 8, 9,

            //Range sample from C# 8.0
            var aaa = row0[0..2];
            var ccc = row0[..2];
            var bbb = row0[^2];

        }
        public void OldExpressions()
        {
            // Create an array:
            int[] a = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Create a list:
            List<string> b = new List<string> { "one", "two", "three" };

            // Create a span
            Span<int> c = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Create a jagged 2D array:
            int[][] twoD = new[]
            {
                new[]{1, 2, 3 },
                new[]{4, 5, 6 },
                new[]{7, 8, 9}
            };

            // Create a jagged 2D array from variables:
            int[] row0 = new[] { 1, 2, 3 };
            int[] row1 = new[] { 4, 5, 6 };
            int[] row2 = new[] { 7, 8, 9 };
            int[][] twoDFromVariables = new[]
            {
               row0,
               row1,
               row2
            };
            int[] single = row0.Concat(row1).Concat(row2).ToArray();

            foreach (var element in single)
            {
                Console.Write($"{element}, ");
            }
            // output:
            // 1, 2, 3, 4, 5, 6, 7, 8, 9,
        }


    }
}

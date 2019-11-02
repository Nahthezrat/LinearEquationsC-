using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearEquations
{
    class LinearSystem
    {
        double[,] A;
        double[] B;
        double det;

        LinearSystem(double[,] A, double[] B)
        {
            const int N = 4;
            A = new double[4, 4] { { 10 * N + 1, 4, 2, 2 }, { 4, 8, 0, 2 }, { 2, 0, 9, -4 }, { 2, 2, -4, 12 } };
            B = new double[4] { 2 * N * Math.Sin(N), 5 * (Math.Sin(N) - Math.Cos(N)), 7 * (Math.Cos(N) + Math.Sin(N)), 3 * Math.Sin(N) };
        }

        void Triangle()
        {
            double[,] tMatrix = new double[3, 3] { {2, 1, -1}, {-3, -1, 2}, {-2, 1, 2} };

            for(int i = 1; i < tMatrix.Length; ++i)
            {
                for(int j = 0; j < tMatrix.GetLength(0); ++j)
                {
                    tMatrix[i, j] += -(tMatrix[i, 0] / tMatrix[0, 0]) * tMatrix[0, j];
                    Console.Write(tMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

        }
        /*
        double Determinant(double[,] A, int j)
        {
            Console.WriteLine("Determinant ");
            --j;
            double determinant = 0;
            for (int i = 0; i < A.Length; ++i)
            {
                determinant += Math.Pow(-1, (i + 1) + (j + 1))*A[i,j]* Determinant(j+1);
            }

            return determinant;
        }
        */
    }
}

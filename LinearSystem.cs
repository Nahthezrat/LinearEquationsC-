using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LinearEquations
{
    class LinearSystem
    {
        public double[,] A;
        public double[] B;

        public LinearSystem(double[,] A, double[] B)
        {
            this.A = ((double[,])A.Clone());
            this.B = ((double[])B.Clone());
            //const int N = 4;
            //A = new double[4, 4] { { 10 * N + 1, 4, 2, 2 }, { 4, 8, 0, 2 }, { 2, 0, 9, -4 }, { 2, 2, -4, 12 } };
            //B = new double[4] { 2 * N * Math.Sin(N), 5 * (Math.Sin(N) - Math.Cos(N)), 7 * (Math.Cos(N) + Math.Sin(N)), 3 * Math.Sin(N) };
        }

        private void GaussForwardStroke()
        {

        }

        public void Show()
        {
            for (int i = 0; i < A.GetLength(0); ++i)
            {
                for (int j = 0; j < A.GetLength(1); ++j)
                {
                    Console.Write("{0:f3}\t", A[i, j]);
                }
                Console.Write("| {0:f3}", B[i]);
                Console.WriteLine();
            }
        }

        /*
        // Инициализация массива индексов столбцов
        private int[] InitIndex()
        {
            int[] index = new int[size];
            for (int i = 0; i < index.Length; ++i)
                index[i] = i;
            return index;
        }
        */
        // Приведение к треугольному виду
        public void TriangularForm()
        {
            double coef; // Коэффициент трансформации
            for (int t = 0; t < A.GetLength(0); ++t) //
            {
                for (int i = t + 1; i < A.GetLength(0); ++i)
                {
                    coef = -(A[i, t] / A[t, t]);
                    B[i] += coef * B[t];
                    for (int j = 0; j < A.GetLength(1); ++j)
                    {
                        A[i, j] += coef * A[t, j];
                    }
                }
            }
        }

        static double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        static double[] MatrixVectorProduct(double[,] matrix, double[] vector)
        {
            // result of multiplying an n x m matrix by a m x 1 column vector (yielding an n x 1 column vector)
            int mRows = matrix.Length;
            int mCols = matrix.GetLength(0);
            int vRows = vector.Length;
            if (mCols != vRows)
                throw new Exception("Non-conformable matrix and vector in MatrixVectorProduct");
            double[] result = new double[mRows]; // an n x m matrix times a m x 1 column vector is a n x 1 column vector
            for (int i = 0; i < mRows; ++i)
                for (int j = 0; j < mCols; ++j)
                    result[i] += matrix[i,j] * vector[j];
            return result;
        }

        static double[] MatrixSum(double[] vec1, double[] vec2, int sign = 0)
        {
            double[] result = new double[vec1.Length];
            for(int i = 0; i < vec1.Length; ++i)
            {
                if (sign == 0) { result[i] = vec1[i] + vec2[i]; }
                else { result[i] = vec1[i] - vec2[i]; }
            }
            return result;
        }

        public void MinimalResidualMethod()
        {
            var matrix_a = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1, -1 }, { -3, -1, 2 }, { -2, 1, 2 } });
            var vector_b = Vector<double>.Build.DenseOfArray(new double[] { 8, -11, -3 });

            var xk = Vector<double>.Build.DenseOfArray(new double[] { 1, 1, 1 });
            var rk = Vector<double>.Build.DenseOfArray(new double[] { 1, 1, 1 });
            var AmultRk = Vector<double>.Build.DenseOfArray(new double[] { 1, 1, 1 });
            double theta;


            /* Итерационный процесс */
            for (int k = 0; k < 50; ++k)
            {
                Console.WriteLine();

                // Вычисление невязки r|k|
                rk = matrix_a.Multiply(xk).Subtract(vector_b);

                Console.Write("rk:\t");
                for (int i = 0; i < rk.Count; i++)
                {
                    Console.Write("{0:f3}\t", rk[i]);
                }
                Console.WriteLine();
                

                // Вычисление Тета t|k|
                AmultRk = matrix_a.Multiply(rk);
                theta = (rk.DotProduct(AmultRk)) / (AmultRk.DotProduct(AmultRk));
                Console.Write("theta: {0:f3}\n", theta);

                // Вычисление x|k+1|
                xk = xk.Subtract(rk.Multiply(theta));

                Console.Write("xk:\t");
                for (int i = 0; i < xk.Count; i++)
                {
                    Console.Write("{0:f3}\t", xk[i]);
                }
                Console.WriteLine();

            }

        }


        /*public SteepestDescent()
        {

        }*/

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

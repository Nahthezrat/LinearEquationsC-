using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;

namespace LinearEquations
{
    class LinearSystem
    {
        private Matrix<double> matrix_A;
        private Vector<double> vector_b;
        private int dim;

        public LinearSystem(Matrix<double> matrix_A, Vector<double> vector_b)
        {
            this.matrix_A = matrix_A.Clone();
            this.vector_b = vector_b.Clone();
            this.dim = matrix_A.ColumnCount;
            //A = new double[4, 4] { { 10 * N + 1, 4, 2, 2 }, { 4, 8, 0, 2 }, { 2, 0, 9, -4 }, { 2, 2, -4, 12 } };
            //B = new double[4] { 2 * N * Math.Sin(N), 5 * (Math.Sin(N) - Math.Cos(N)), 7 * (Math.Cos(N) + Math.Sin(N)), 3 * Math.Sin(N) };
        }

        public void Show()
        {
            Console.WriteLine("Linear System: ");
            for (int i = 0; i < dim; ++i)
            {
                for (int j = 0; j < dim; ++j)
                {
                    Console.Write("{0:f3}\t", matrix_A[i, j]);
                }
                Console.Write("| {0:f3}", vector_b[i]);
                Console.WriteLine();
            }
        }

        public void MinimalResidualMethod()
        {
            var rk = Vector<double>.Build.Dense(dim);
            var xk = Vector<double>.Build.Dense(dim);
            var Ark = Vector<double>.Build.Dense(dim);
            double theta;

            // Итерационный процесс
            for (int k = 0; k < 30; k++)
            {
                // r|k| = A*xk - B
                rk = matrix_A.Multiply(xk).Subtract(vector_b);
                Console.Write("rk:\t");
                for (int i = 0; i < dim; i++)
                {
                    Console.Write("{0:f3}\t", rk[i]);
                }
                Console.WriteLine();

                // theta|k| = (rk, Ark) / (Ark, Ark)
                Ark = matrix_A.Multiply(rk);
                theta = (rk.DotProduct(Ark)) / (Ark.DotProduct(Ark));
                Console.Write("theta: {0:f3}\n", theta);

                // x|k+1| = x|k| - r|k|*theta|k|
                xk = xk.Subtract(rk.Multiply(theta));
                Console.Write("xk:\t");
                for (int i = 0; i < dim; i++)
                {
                    Console.Write("{0:f3}\t", xk[i]);
                }
                Console.WriteLine();


                Console.WriteLine();
            }
        }

    }
}

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

        // Метод минимальных невязок / Метод скорейшего спуска
        public Vector<double> MinimalResidualMethod(bool speedly = false, double Eps = 0.00001)
        {
            var rk = Vector<double>.Build.Dense(dim);
            var xk = Vector<double>.Build.Dense(dim);
            var Ark = Vector<double>.Build.Dense(dim);
            double theta;
            int count = 0;

            // Итерационный процесс
            while (Math.Sqrt((matrix_A.Multiply(xk).Subtract(vector_b).DotProduct(matrix_A.Multiply(xk).Subtract(vector_b))) / Math.Sqrt(vector_b.DotProduct(vector_b))) >= Eps)
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
                theta = speedly ? (rk.DotProduct(rk) / Ark.DotProduct(rk)) : (rk.DotProduct(Ark) / Ark.DotProduct(Ark));
                Console.Write("theta: {0:f3}\n", theta);

                // x|k+1| = x|k| - r|k|*theta|k|
                xk = xk.Subtract(rk.Multiply(theta));
                Console.Write("xk:\t");
                for (int i = 0; i < dim; i++)
                {
                    Console.Write("{0:f5}\t", xk[i]);
                }
                Console.WriteLine();

                Console.WriteLine("count: {0}\n", count++);
            }

            return xk;
        }


    }
}

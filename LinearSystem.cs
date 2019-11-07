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
        private Matrix<double> matrix_A_temp;
        private Vector<double> vector_b_temp;
        private int dim;

        public LinearSystem(Matrix<double> matrix_A, Vector<double> vector_b)
        {
            this.matrix_A = matrix_A.Clone();
            this.vector_b = vector_b.Clone();
            this.dim = matrix_A.ColumnCount;
        }

        /// <summary>
        /// Вывод СЛАУ в консоль
        /// </summary>
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

        /// <summary>
        /// Поиск главного элемента в матрице
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private double FindR(int row, int[] index)
        {
            int max_index = row;
            double max = matrix_A_temp[row, index[max_index]];
            double max_abs = Math.Abs(max);

            // Поиск главного (наибольшего) элемента
            for (int cur_index = row + 1; cur_index < dim; ++cur_index)
            {
                double cur = matrix_A_temp[row, index[cur_index]];
                double cur_abs = Math.Abs(cur);
                if (cur_abs > max_abs)
                {
                    max_index = cur_index;
                    max = cur;
                    max_abs = cur_abs;
                }
            }

            // Меняем местами индексы столбцов
            int temp = index[row];
            index[row] = index[max_index];
            index[max_index] = temp;

            return max;
        }

        /// <summary>
        /// Метод Гаусса с выбором главного элемента
        /// </summary>
        /// <param name="Eps"></param>
        /// <returns></returns>
        public Vector<double> GaussMethod(double Eps = 0.00001)
        {
            // Клонирование матриц для операций над ними
            matrix_A_temp = matrix_A.Clone();
            vector_b_temp = vector_b.Clone();
            var vector_x = Vector<double>.Build.Dense(dim);
            var vector_r = Vector<double>.Build.Dense(dim);

            // Инициализация вектора индексов
            int[] index = new int[dim];
            for (int i = 0; i < dim; ++i)
                index[i] = i;

            /* Прямой ход метода Гаусса */
            for (int i = 0; i < dim; ++i)
            {
                // Выбор главного элемента
                double r = FindR(i, index);

                // Преобразование текущей строки матрицы A
                for (int j = 0; j < dim; ++j)
                    matrix_A_temp[i, j] /= r;

                // Преобразование i-го элемента вектора b
                vector_b_temp[i] /= r;

                // Вычитание текущей строки из всех нижерасположенных строк
                for (int k = i + 1; k < dim; ++k)
                {
                    double p = matrix_A_temp[k, index[i]];
                    for (int j = i; j < dim; ++j)
                        matrix_A_temp[k, index[j]] -= matrix_A_temp[i, index[j]] * p;
                    vector_b_temp[k] -= vector_b_temp[i] * p;
                    matrix_A_temp[k, index[i]] = 0.0;
                }
            }

            /* Обратный ход метода Гаусса */
            for (int i = dim - 1; i >= 0; --i)
            {
                // Начальное значение  x
                double x_i = vector_b_temp[i];

                // Корректировка значения x
                for (int j = i + 1; j < dim; ++j)
                    x_i -= vector_x[index[j]] * matrix_A_temp[i, index[j]];
                vector_x[index[i]] = x_i;
            }

            /* Вычисление невязки */
            // r = b - x * A
            vector_r = vector_b.Subtract(matrix_A.Multiply(vector_x));
            // Вывод в консоль
            Console.Write("vector_r:\t");
            for (int i = 0; i < dim; i++)
            {
                Console.Write("{0:f3}\t", vector_r[i]);
            }
            Console.WriteLine();

            return vector_x;
        }

        /// <summary>
        /// Метод минимальных невязок / Метод скорейшего спуска
        /// </summary>
        /// <param name="speedly"></param>
        /// <param name="Eps"></param>
        /// <returns></returns>
        public Vector<double> MinimalResidualMethod(bool speedly = false, double Eps = 0.00001)
        {
            var rk = Vector<double>.Build.Dense(dim); // Невязка
            var xk = Vector<double>.Build.Dense(dim); // Итерационное решение
            var Ark = Vector<double>.Build.Dense(dim); // A*xk
            double theta; // Параметр
            int count = 0; // Счётчик

            /* Итерационный процесс */
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

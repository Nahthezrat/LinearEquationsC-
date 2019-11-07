using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace LinearEquations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //var matrix_A = Matrix<double>.Build.DenseOfArray(new double[,] { {3, 4, -9, 5}, {-15, -12, 50, -16}, {-27, -36, 73, 8}, {9, 12, -10, -16} });
            //var vector_B = Vector<double>.Build.DenseOfArray(new double[] { -14, 44, 142, -76 });
            //var matrix_A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1, -1 }, { -3, -1, 2 }, { -2, 1, 2 } });
            //var vector_B = Vector<double>.Build.DenseOfArray(new double[] { 8, -11, -3 });
            //var system = new LinearSystem(matrix_A, vector_B);
            //system.Show();
            //system.MinimalResidualMethod();

            const int N = 10;
            var matrix_A = Matrix<double>.Build.DenseOfArray(new double[,] { { 10 * N + 1, 4, 2, 2 }, { 4, 8, 0, 2 }, { 2, 0, 9, -4 }, { 2, 2, -4, 12 } });
            var vector_b = Vector<double>.Build.DenseOfArray(new double[] { 2 * N * Math.Sin(N), 5 * (Math.Sin(N) - Math.Cos(N)), 7 * (Math.Cos(N) + Math.Sin(N)), 3 * Math.Sin(N) });
            var vector_X = Vector<double>.Build.Dense(matrix_A.ColumnCount);

            var system = new LinearSystem(matrix_A, vector_b);
            system.Show();

            vector_X = system.MinimalResidualMethod(true);

            Console.Write("vector_X:\t");
            for (int i = 0; i < matrix_A.ColumnCount; i++)
            {
                Console.Write("{0:f5}\t", vector_X[i]);
            }
            Console.WriteLine();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            const int N = 10;
            var matrix_A = Matrix<double>.Build.DenseOfArray(new double[,] { { 10 * N + 1, 4, 2, 2 }, { 4, 8, 0, 2 }, { 2, 0, 9, -4 }, { 2, 2, -4, 12 } });
            var vector_b = Vector<double>.Build.DenseOfArray(new double[] { 2 * N * Math.Sin(N), 5 * (Math.Sin(N) - Math.Cos(N)), 7 * (Math.Cos(N) + Math.Sin(N)), 3 * Math.Sin(N) });
            var vector_X = Vector<double>.Build.Dense(matrix_A.ColumnCount);
            

            var system = new LinearSystem(matrix_A, vector_b);
            system.Show();

            vector_X = system.GaussMethod();

            Console.Write("vector_X:\t");
            for (int i = 0; i < matrix_A.ColumnCount; i++)
            {
                Console.Write("{0:f5}\t", vector_X[i]);
            }
            Console.WriteLine();
        }

    }
}


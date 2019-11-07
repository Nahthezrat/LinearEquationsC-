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

            var matrix_A = Matrix<double>.Build.DenseOfArray(new double[,] { { 2, 1, -1 }, { -3, -1, 2 }, { -2, 1, 2 } });
            var vector_B = Vector<double>.Build.DenseOfArray(new double[] { 8, -11, -3 });

            var system = new LinearSystem(matrix_A, vector_B);
            system.Show();
            system.MinimalResidualMethod();
        }

    }

}

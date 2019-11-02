﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            //int N = 4;
            //double[,] A = new double[4, 4] { { 10 * N + 1, 4, 2, 2 }, { 4, 8, 0, 2 }, { 2, 0, 9, -4 }, { 2, 2, -4, 12 } };
            //double[] B = new double[4] { 2 * N * Math.Sin(N), 5 * (Math.Sin(N) - Math.Cos(N)), 7 * (Math.Cos(N) + Math.Sin(N)), 3 * Math.Sin(N) };

            //double[,] tMatrix = new double[3, 3] { { 2, 1, -1 }, { -3, -1, 2 }, { -2, 1, 2 } };
            double[,] tMatrix = new double[3, 3] { { 1, 2, -1 }, { 2, 3, 1 }, { 1, -1, -1 } };
            Console.WriteLine(tMatrix.Length);

            double coef;
            for (int t = 0; t < tMatrix.GetLength(0); ++t)
            {
                for (int i = t+1; i < tMatrix.GetLength(0); ++i)
                {
                    coef = -(tMatrix[i, t] / tMatrix[t, t]);
                    Console.WriteLine("coef: "+ coef);
                    Console.Write("Line "+i+" : ");
                    for (int j = 0; j < tMatrix.GetLength(1); ++j)
                    {
                        tMatrix[i, j] += coef * tMatrix[t, j];
                        Console.Write(tMatrix[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }

        }

    }
}

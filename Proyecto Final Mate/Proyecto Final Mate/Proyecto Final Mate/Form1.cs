using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Numerics;

namespace Proyecto_Final_Mate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener la función ingresada por el usuario
            string funcion = textBox1.Text;

            // Rango de integración
            double a = -Math.PI;
            double b = Math.PI;

            // Número de términos de la serie de Fourier
            int n = 10; // Puedes ajustar este valor según tus necesidades

            // Calcular coeficientes de la serie de Fourier
            double[] an = new double[n + 1];
            double[] bn = new double[n + 1];

            for (int i = 0; i <= n; i++)
            {
                an[i] = CalcularCoeficienteAn(funcion, i, a, b);
                bn[i] = CalcularCoeficienteBn(funcion, i, a, b);
            }

            // Mostrar los coeficientes a_n y b_n en el ListBox
            listBox1.Items.Clear();
            for (int i = 0; i <= n; i++)
            {
                listBox1.Items.Add($"a{i} = {an[i]}, b{i} = {bn[i]}");
            }
        }

        // Función para calcular el coeficiente a_n de la serie de Fourier
        private double CalcularCoeficienteAn(string funcion, int n, double a, double b)
        {
            Func<double, double> integrando = x => EvaluarFuncion(funcion, x) * Math.Cos(n * x);
            return IntegrarFuncion(integrando, a, b) / Math.PI;
        }

        // Función para calcular el coeficiente b_n de la serie de Fourier
        private double CalcularCoeficienteBn(string funcion, int n, double a, double b)
        {
            Func<double, double> integrando = x => EvaluarFuncion(funcion, x) * Math.Sin(n * x);
            return IntegrarFuncion(integrando, a, b) / Math.PI;
        }

        // Función para evaluar la función ingresada por el usuario
        private double EvaluarFuncion(string funcion, double x)
        {
            // Aquí puedes implementar un parser de funciones o utilizar bibliotecas como Math.NET Symbolics para evaluar la función
            // Por simplicidad, este ejemplo simplemente evalúa sin(x)
            return Math.Sin(x);
        }

        // Función para aproximar la integral numéricamente usando la regla del trapecio
        private double IntegrarFuncion(Func<double, double> funcion, double a, double b)
        {
            int n = 1000; // Puedes ajustar este valor según tus necesidades
            double h = (b - a) / n;
            double suma = 0.5 * (funcion(a) + funcion(b));

            /*for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                suma += funcion(xi);
            }*/

            return suma * h;
        }
    }
}
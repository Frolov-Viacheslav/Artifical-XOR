using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Artifical_XOR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<double[]> neuron = new List<double[]>() {new double[2], new double[3], new double[1] };
        List<double[]> weights = new List <double[]>()  {new double[] { 0.12, 0.74, 0.31, 0.88, 0.71, 0.98 }, new double[] { 0.02, 0.67, 0.42}};
        const double E = 0.7;
        const double A = 0.3;
        double ideal;
        double output;
        double error;
        public void Start()
        {
            error = 0;
            Initialization(0, 0, 0);
            output = Training();
            error += (ideal - output) * (ideal - output);
            Back_Propagation_Method();

            Initialization(0, 1, 1);
            output = Training();
            error += (ideal - output) * (ideal - output);
            Back_Propagation_Method();

            Initialization(1, 0, 1);
            output = Training();
            error += (ideal - output) * (ideal - output);
            Back_Propagation_Method();

            Initialization(1, 1, 0);
            output = Training();
            error += (ideal - output) * (ideal - output);
            error /= 4;
            Back_Propagation_Method();

        }
        public void Initialization(double x1, double x2, double ideal)
        {
            neuron[0][0] = x1;
            neuron[0][1] = x2;
            this.ideal = ideal;
        }
        public double Training()
        {
            neuron[1][0] = Activation(neuron[0][0] * weights[0][0] + neuron[0][1] * weights[0][3]);
            neuron[1][1] = Activation(neuron[0][0] * weights[0][1] + neuron[0][1] * weights[0][4]);
            neuron[1][2] = Activation(neuron[0][0] * weights[0][2] + neuron[0][1] * weights[0][5]);
            neuron[2][0] = Activation(neuron[1][0] * weights[1][0] + neuron[1][1] * weights[1][1] + neuron[1][2] * weights[1][2]);
            return neuron[2][0];
        }

        public double Activation(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }


        double delta_out = 0;
        double delta_h_0 = 0;
        double delta_h_1 = 0;
        double delta_h_2 = 0;
        public void Calculate_neuron_delta()
        {
            delta_out = (ideal - output) * (1 - output) * output;
            delta_h_0 = (1 - neuron[1][0]) * neuron[1][0] * weights[1][0] * delta_out;
            delta_h_1 = (1 - neuron[1][1]) * neuron[1][1] * weights[1][1] * delta_out;
            delta_h_2 = (1 - neuron[1][2]) * neuron[1][2] * weights[1][2] * delta_out;
        }

        double delta_h_w_0 = 0;
        double delta_h_w_1 = 0;
        double delta_h_w_2 = 0;
        public void Calculate_weights_of_hidden_neuron()
        {
            double grad_h_w_0 = delta_out * neuron[1][0];
            double grad_h_w_1 = delta_out * neuron[1][1];
            double grad_h_w_2 = delta_out * neuron[1][2];

            

            delta_h_w_0 = E * grad_h_w_0 + delta_h_w_0 * A;
            delta_h_w_1 = E * grad_h_w_1 + delta_h_w_1 * A;
            delta_h_w_2 = E * grad_h_w_2 + delta_h_w_2 * A;

            weights[1][0] += delta_h_w_0;
            weights[1][1] += delta_h_w_1;
            weights[1][2] += delta_h_w_2;
        }

        double delta_i_w_0 = 0;
        double delta_i_w_1 = 0;
        double delta_i_w_2 = 0;
        double delta_i_w_3 = 0;
        double delta_i_w_4 = 0;
        double delta_i_w_5 = 0;
        public void Calculate_weights_of_input_neuron()
        {
            double grad_i_w_0 = delta_h_0 * neuron[0][0];
            double grad_i_w_1 = delta_h_1 * neuron[0][0];
            double grad_i_w_2 = delta_h_2 * neuron[0][0];
            double grad_i_w_3 = delta_h_0 * neuron[0][1];
            double grad_i_w_4 = delta_h_1 * neuron[0][1];
            double grad_i_w_5 = delta_h_2 * neuron[0][1];

            

            delta_i_w_0 = E * grad_i_w_0 + delta_i_w_0 * A;
            delta_i_w_1 = E * grad_i_w_1 + delta_i_w_1 * A;
            delta_i_w_2 = E * grad_i_w_2 + delta_i_w_2 * A;
            delta_i_w_3 = E * grad_i_w_3 + delta_i_w_3 * A;
            delta_i_w_4 = E * grad_i_w_4 + delta_i_w_4 * A;
            delta_i_w_5 = E * grad_i_w_5 + delta_i_w_5 * A;

            weights[0][0] += delta_i_w_0;
            weights[0][1] += delta_i_w_1;
            weights[0][2] += delta_i_w_2;
            weights[0][3] += delta_i_w_3;
            weights[0][4] += delta_i_w_4;
            weights[0][5] += delta_i_w_5;
        }
       
        public void Back_Propagation_Method()
        {
            Calculate_neuron_delta();
            Calculate_weights_of_hidden_neuron();
            Calculate_weights_of_input_neuron();
        }

        uint epoch = 10000;
        private void B_Start_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < epoch; i++)
            Start();
            TB_Error.Text = (Math.Round(error, 5)).ToString();
            TB_output.Text = (Math.Round(output, 5)).ToString();
            Paint_neuron();

        }

        private void B_Check_Click(object sender, RoutedEventArgs e)
        {
            ideal = Convert.ToUInt16(TB_X1.Text) ^ Convert.ToUInt16(TB_X2.Text);
            neuron[0][0] = Convert.ToUInt16(TB_X1.Text);
            neuron[0][1] = Convert.ToUInt16(TB_X2.Text);
            TB_ideal.Text = ideal.ToString();
            output = Training();
            error = (ideal - output) * (ideal - output);
            TB_Error.Text = (Math.Round(error, 5)).ToString();
            TB_output.Text = (Math.Round(output, 5)).ToString();
            Paint_neuron();

        }
        private void Paint_neuron()
        {
            L_X1.Content = neuron[0][0];
            L_X2.Content = neuron[0][1];
            L_H1.Content = neuron[1][0].ToString("F3");
            L_H2.Content = neuron[1][1].ToString("F3");
            L_H3.Content = neuron[1][2].ToString("F3");
            L_O1.Content = (Math.Round(neuron[2][0], 3)).ToString();
            L_w0.Content = weights[0][0].ToString("F3");
            L_w1.Content = weights[0][1].ToString("F3");
            L_w2.Content = weights[0][2].ToString("F3");
            L_w3.Content = weights[0][3].ToString("F3");
            L_w4.Content = weights[0][4].ToString("F3");
            L_w5.Content = weights[0][5].ToString("F3");
            L_w10.Content = weights[1][0].ToString("F3");
            L_w11.Content = weights[1][1].ToString("F3");
            L_w12.Content = weights[1][2].ToString("F3");
        }

        private void TB_epoch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                epoch = Convert.ToUInt32(TB_epoch.Text);
            }
            catch { }
        }
    }
}

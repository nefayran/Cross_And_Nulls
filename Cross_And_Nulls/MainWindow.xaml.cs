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

namespace Cross_And_Nulls
{
    public static class Program
    {
        public static Random rnd = new Random();
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartEvo_Click(object sender, RoutedEventArgs e)
        {
            Evolution Evo = new Evolution();           
           /* label_winner.Content = Evo.Game(Evo.PersiList[0], Evo.PersiList[1]);
            string CrossMark = "X";
            string NullMark = "O";
            string[] Marks = new string[9];
            for (int i = 0; i < 9; i++)
            {
                if (Evo.X[i] == 1) Marks[i] = NullMark;
                else if (Evo.X[i] == -1) Marks[i] = CrossMark;
            }
            label_1.Content = Marks[0];
            label_2.Content = Marks[1];
            label_3.Content = Marks[2];
            label_4.Content = Marks[3];
            label_5.Content = Marks[4];
            label_6.Content = Marks[5];
            label_7.Content = Marks[6];
            label_8.Content = Marks[7];
            label_9.Content = Marks[8];
            */
        }
    }
}

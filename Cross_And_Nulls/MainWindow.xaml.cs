﻿using System;
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
        Evolution Evo;
        private void StartEvo_Click(object sender, RoutedEventArgs e)
        {
            Evo = new Evolution(91);
            //Рождение первого поколения
            Evo.Born();
            //Проводим селекцию
            while(Evo.n!=1)
            { 
                Evo.Selection();
                Evo.Crossbreeding(true);
            }
            int u = 0;
        }

        private void Step(object sender, RoutedEventArgs e)
        {
            Evo.PersiList[Evo.PersiList.Count - 1].Fraction = -1;
            string[] Marks = new string[9];
            Button label = (Button)sender;
            label.Content = "O";
            label.IsEnabled = false;
            //            
            Marks[0] = Convert.ToString(label_1.Content);
            Marks[1] = Convert.ToString(label_2.Content);
            Marks[2] = Convert.ToString(label_3.Content);
            Marks[3] = Convert.ToString(label_4.Content);
            Marks[4] = Convert.ToString(label_5.Content);
            Marks[5] = Convert.ToString(label_6.Content);
            Marks[6] = Convert.ToString(label_7.Content);
            Marks[7] = Convert.ToString(label_8.Content);
            Marks[8] = Convert.ToString(label_9.Content);
            int[] X = new int[9];
            for (int i = 0; i < 9; i++)
            {
                if (Marks[i] == "X")
                    X[i] = -1;
                else if (Marks[i] == "O") X[i] = 1;
            }
            X = Evo.PersiList[Evo.PersiList.Count-1].GameStep(X);
            //
            for (int i = 0; i < 9; i++)
            {
                if (X[i] == 1) Marks[i] = "O";
                else if (X[i] == -1) Marks[i] = "X";
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
            label_winner.Content = Evo.WinController(X);
        }
    }
}

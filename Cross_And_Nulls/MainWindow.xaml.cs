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
using System.ComponentModel;
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
        Evolution Evo;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private BackgroundWorker backgroundWorker;//Фоновый поток
        bool StepController = true;//Первыми ходят нолики
        int YouFraction = 0;//Твоя фракция
        int iter = 0;
        double Mp = 0;
        int n = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Tick;
            backgroundWorker = new BackgroundWorker();
            iter = Convert.ToInt32(СyclesCount.Text);
            Mp = Convert.ToInt32(MutationPercent.Text) / 100.0;
            n = Convert.ToInt32(StartBorn.Text);//Устойчивый n*2
        }
        //Пусть играют сами с собой
        public void Tick(object sender, EventArgs e)
        {
            //Переводим доску в 0 и -1            
            int[] X = new int[9];
            string[] Marks = new string[9];
            Marks[0] = Convert.ToString(label_1.Content);
            Marks[1] = Convert.ToString(label_2.Content);
            Marks[2] = Convert.ToString(label_3.Content);
            Marks[3] = Convert.ToString(label_4.Content);
            Marks[4] = Convert.ToString(label_5.Content);
            Marks[5] = Convert.ToString(label_6.Content);
            Marks[6] = Convert.ToString(label_7.Content);
            Marks[7] = Convert.ToString(label_8.Content);
            Marks[8] = Convert.ToString(label_9.Content);
            for (int i = 0; i < 9; i++)
            {
                if (Marks[i] == "X")
                    X[i] = -1;
                else if (Marks[i] == "O") X[i] = 1;
                else X[i] = 0;
            }
            //Они ходят
            if (StepController)
            {
                X = Evo.PersiList[0].GameStep(X);
                StepController = !StepController;
            }
            else if(!StepController)
            { 
                X = Evo.PersiList[1].GameStep(X);
                StepController = !StepController;
            }                       
            //Переводим 1 и -1 в доску
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
            if (Evo.WinController(X) != 0)
            {
                label_winner.Content = Evo.WinController(X);
                timer.Stop();
            }
        }                   
        private void StartEvo_Click(object sender, RoutedEventArgs e)
        {
            n = Convert.ToInt32(StartBorn.Text);//Устойчивый n*2
            Evo = new Evolution(3);           
            Evo.Born(n);
            iter = Convert.ToInt32(СyclesCount.Text);
            Mp = Convert.ToInt32(MutationPercent.Text) / 100.0;
            StartEvo.IsEnabled = false;
            Cross.IsEnabled = false;
            Null.IsEnabled = false;
            GoGame.IsEnabled = false;
            Clear_button.IsEnabled = false;
            //Поток
            backgroundWorker.DoWork += new DoWorkEventHandler(bgw_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerAsync();

        }
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<double> K = new List<double>();
            /*for (int i = 0; i < iter; i++)
            {
                Evo.Selection(Evo.n / 2);
                Evo.Crossbreeding(false);
                Evo.Born(n - n/4);
                Evo.Mutation(Mp);
                int prog = i *100 / iter;
                K.Add(Evo.K);
                backgroundWorker.ReportProgress(prog);
            }*/
            for (int i = 0; i < iter; i++)
            {
                Evo.NullsCounters();
                Evo.Crossbreeding(true);
                Evo.Selection(Evo.n / 2);
                Evo.Born(Evo.n/3);
                Evo.Mutation(Mp);
                int prog = i *100 / iter;
                K.Add(Evo.K);
                backgroundWorker.ReportProgress(prog);
            }
            int q = 0;
        }
        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Loader.Value = e.ProgressPercentage;
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Evo.Selection(Evo.PersiList.Count - 2);//Итоговый турнир
            //Включение интерфейса
            Cross.IsEnabled = true;
            Null.IsEnabled = true;
            GoGame.IsEnabled = true;
            Clear_button.IsEnabled = true;
            StartEvo.IsEnabled = true;
        }
        private void Step(object sender, RoutedEventArgs e)
        {
            Evo.PersiList[Evo.PersiList.Count-1].Fraction = -YouFraction;
            string[] Marks = new string[9];
            Button label = (Button)sender;
            if(YouFraction == 1)
                label.Content = "O";
            else label.Content = "X";
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
                else X[i] = 0;
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
        private void Step()
        {
            Evo.PersiList[Evo.PersiList.Count - 1].Fraction = -YouFraction;
            string[] Marks = new string[9];
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
            X = Evo.PersiList[Evo.PersiList.Count - 1].GameStep(X);
            //
            for (int i = 0; i < 9; i++)
            {
                if (X[i] == 1) Marks[i] = "O";
                else if (X[i] == -1) Marks[i] = "X";
                else X[i] = 0;
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
        private void Clear_button_Click(object sender, RoutedEventArgs e)
        {
            label_1.Content = "[  ]";
            label_2.Content = "[  ]";
            label_3.Content = "[  ]";
            label_4.Content = "[  ]";
            label_5.Content = "[  ]";
            label_6.Content = "[  ]";
            label_7.Content = "[  ]";
            label_8.Content = "[  ]";
            label_9.Content = "[  ]";

            label_1.IsEnabled = false;
            label_2.IsEnabled = false;
            label_3.IsEnabled = false;
            label_4.IsEnabled = false;
            label_5.IsEnabled = false;
            label_6.IsEnabled = false;
            label_7.IsEnabled = false;
            label_8.IsEnabled = false;
            label_9.IsEnabled = false;
        }

        private void GoGame_Click(object sender, RoutedEventArgs e)
        {
            Evo.PersiList[0].Fraction = 1;
            Evo.PersiList[1].Fraction = -1;
            timer.Start();
        }

        private void Null_Click(object sender, RoutedEventArgs e)
        {
            YouFraction = 1;
            Step();
            label_1.IsEnabled = true;
            label_2.IsEnabled = true;
            label_3.IsEnabled = true;
            label_4.IsEnabled = true;
            label_5.IsEnabled = true;
            label_6.IsEnabled = true; 
            label_7.IsEnabled = true;
            label_8.IsEnabled = true;
            label_9.IsEnabled = true;
        }

        private void Cross_Click(object sender, RoutedEventArgs e)
        {
            YouFraction = -1;
            label_1.IsEnabled = true;
            label_2.IsEnabled = true;
            label_3.IsEnabled = true;
            label_4.IsEnabled = true;
            label_5.IsEnabled = true;
            label_6.IsEnabled = true;
            label_7.IsEnabled = true;
            label_8.IsEnabled = true;
            label_9.IsEnabled = true;
        }
    }
}

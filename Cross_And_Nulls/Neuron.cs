using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_And_Nulls
{
    public class Neuron
    {
        public double[] inputs;//Вход
        public double[] weights;//Веса
        public double biasWeight;//Смещение веса      
        int n;//Кол-во входов        
        public Neuron(int N)
        {
            n = N;
            weights = new double[n];
            inputs = new double[n];
        }
        //Выход нейрона
        public double outs
        {
            get 
            {
                double s = 0;
                for (int i = 0; i < n; i++)
                    s += weights[i] * inputs[i];
                return Function.F(s);//biasWeight); 
            }
        }
        //Генерация случайных весов
        public void randomizeWeights()
        {
            for (int i = 0; i < n; i++)
            {
                
                weights[i] = 0.5-Program.rnd.NextDouble();
            }
            biasWeight = 0.5 - Program.rnd.NextDouble();
        }
      
    }
    //Класс необходимых функций
    class Function
    {
        //Сигмоида
        public static double F(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }
        //Пороговая функция
        public static double Fp(double x)
        {
            if (x > 0.5)
                return 1.0;
            else return 0;
        }
    }
}

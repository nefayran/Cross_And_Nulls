using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_And_Nulls
{
    /*
     * Класс реализующий систему 
     * Состоит из 3 скрытых нейронов
     * и 1 выходного нейрона
     */
    public class Perseptron
    {       
        Neuron in1;//Скрытый нейрон
        Neuron in2;//Скрытый нейрон
        Neuron in3;//Скрытый нейрон
        Neuron in4;//Скрытый нейрон
        Neuron on;//Выходной нейрон
        public Random rnd = new Random();//Хаос
        public int Fraction = 0;//Сторона
        public Perseptron()
        {
                  
        }
        public void Initialization()
        {
            //Создание нейронов
            in1 = new Neuron(9);
            in2 = new Neuron(9);
            in3 = new Neuron(9);
            in4 = new Neuron(3);
            on = new Neuron(3);
            // Генерируем веса
            in1.randomizeWeights(rnd);
            in2.randomizeWeights(rnd);
            in3.randomizeWeights(rnd);
            on.randomizeWeights(rnd);
        }
        //Функция просчета хода
        public int GameStep(int[] x)
        {
                //Сохраняем исходное состояние
                int[] Save = new int[9];
                Array.Copy(Save, x, 9);
                //Ищем пустые ячейки
                int k = 0;//Кол-во пустых ячеек
                List<int> N = new List<int>();//Номера пустых ячеек          
                for (int i = 0; i < 9; i++)
                {
                    if (x[i] == 0)  
                    {
                        k++;
                        N.Add(i);
                    }
                }
                //Проходим по всем возможным ходам
                double[] steps = new double[9];
                for (int i = 0; i < k; i++)
                {   
                    //Просчитываем ход
                    Save[N[i]] = Fraction;
                    for (int j = 0; j < 9; j++)
                        steps[j] = (double)Save[j];
                    in1.inputs = steps;
                    in2.inputs = steps;
                    in3.inputs = steps;
                    on.inputs = new double[] { in1.outs, in2.outs, in3.outs };  
                    //Считаем результативность хода
                    double rez = on.outs;
                    //Возвращаем исходное состояние
                    Array.Copy(x, Save, 9);
                }
                double step = 1;
                return (int)step;
        }
    }
}

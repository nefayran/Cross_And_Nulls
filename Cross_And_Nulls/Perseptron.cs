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
        public int n;//Кол-во скрытых нейронов
        public Neuron on;//Выходной нейрон
        public int Fraction = 0;//Сторона
        public double WinCounter = 0;//Счетчик побед
        public int game = 0;//Счетчик игр
        public List<Neuron> InvisibleNeurons_1 = new List<Neuron>();//Список скрытых нейронов 1 слоя
        public List<Neuron> InvisibleNeurons_2 = new List<Neuron>();//Список скрытых нейронов 2 слоя
        public List<Neuron> InvisibleNeurons_3 = new List<Neuron>();//Список скрытых нейронов 3 слоя
        public Perseptron(int N)//n-Кол-во скрытых нейронов
        {
            n = N;
            //Создание нейронов
            for (int i = 0; i < n; i++)
            {
                Neuron InvisibleNeuron_1 = new Neuron(9);
                InvisibleNeurons_1.Add(InvisibleNeuron_1);
                Neuron InvisibleNeuron_2 = new Neuron(n);
                InvisibleNeurons_2.Add(InvisibleNeuron_2);
                Neuron InvisibleNeuron_3 = new Neuron(n);
                InvisibleNeurons_3.Add(InvisibleNeuron_3);
            }
            on = new Neuron(n);
        }
        public void Initialization()
        {
            // Генерируем веса
            for (int i = 0; i < n; i++)
            {
                InvisibleNeurons_1[i].randomizeWeights();                
                InvisibleNeurons_2[i].randomizeWeights();
                InvisibleNeurons_3[i].randomizeWeights();
            }
            on.randomizeWeights();
        }
        //Функция просчета хода
        public int[] GameStep(int[] x)
        {
            double[] steps = new double[9];
            int[] y = new int[9];//Выходная доска
            //По умолчанию:
            Array.Copy(x, y, 9);
            double step = -99999;//Значение хода
            //Сохраняем исходное состояние
            int[] Save = new int[9];
            Array.Copy(x, Save, 9);
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
            //Проверка последнего хода 
            if (k == 1)
            { 
                Save[N[0]] = Fraction;
                Array.Copy(Save, y, 9);
                return y;
            }   
            else
            //Проходим по всем возможным ходам            
                for (int i = 0; i < k; i++)
                {   
                    //Просчитываем ход
                    Save[N[i]] = Fraction;
                    for (int j = 0; j < 9; j++)
                        steps[j] = Save[j];
                    for (int u = 0; u < n; u++)
                    {
                        InvisibleNeurons_1[u].inputs = steps;
                        InvisibleNeurons_2[u].inputs[u] = InvisibleNeurons_1[u].outs;
                        InvisibleNeurons_3[u].inputs[u] = InvisibleNeurons_2[u].outs;
                        on.inputs[u] = InvisibleNeurons_3[u].outs;
                    }
                    //Считаем результативность хода и запоминаем доску
                    if (on.outs > step)
                        {
                         step = on.outs;
                         Array.Copy(Save, y, 9);
                        }
                    //Возвращаем исходное состояние
                    Array.Copy(x, Save, 9);
                }
                return y;
        }
    }
}

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
        public Neuron in1,//Скрытый нейрон
        in2,//Скрытый нейрон
        in3;//Скрытый нейрон
        on;//Выходной нейрон
        public int Fraction = 0;//Сторона
        public int WinCounter = 0;//Счетчик побед
        public int game = 0;//Счетчик игр
        public Perseptron()
        {
        }
        public void Initialization()
        {
            //Создание нейронов
            in1 = new Neuron(9);
            in2 = new Neuron(9);
            in3 = new Neuron(9);
            on = new Neuron(3);
            // Генерируем веса
            in1.randomizeWeights();
            in2.randomizeWeights();
            in3.randomizeWeights();
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
                    in1.inputs = steps;
                    in2.inputs = steps;
                    in3.inputs = steps;
                    on.inputs = new double[] { in1.outs, in2.outs, in3.outs };
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

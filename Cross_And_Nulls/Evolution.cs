using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_And_Nulls
{
    public class Evolution
    {
        public int n;//Размер поколения
        public int Neurons;//Кол-во нейронов в персептроне
        public List<Perseptron> PersiList;//Список особей
        public int[] X;//Доска
        public double K;//Показатель эффективности
        public Evolution(int NeuronsNumber)
        {
            Neurons = NeuronsNumber;
            PersiList = new List<Perseptron>();
            X = new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        //Функция обнуления счетчиков
        public void NullsCounters()
        {
            for (int i = 0; i < n; i++)
            {
                PersiList[i].WinCounter = 0;
                PersiList[i].game = 0;
            }
        }
        //Функция рождения поколения
        public void Born(int N)
        {
            for (int i = 0; i < N; i++)
            {
                Perseptron persi = new Perseptron(Neurons);
                persi.Initialization();
                PersiList.Add(persi);
            }
            n += N;
        }
        //Функция убивает определенное кол-во особей, с конца(false) или с начала (true) 
        public void Die(int N,bool start)
        {
            if(start)
            for (int i = 0; i < N; i++)
                PersiList.Remove(PersiList.First());
            else
            for (int i = 0; i < N; i++)
                PersiList.Remove(PersiList.Last());
            n -= N;
        }
        //Функция игры
        public int Game(Perseptron persi_1, Perseptron persi_2, int f1, int f2)
        {
            int winner = 0;
            persi_1.game++;
            persi_2.game++;
            //Выбираем стороны
            persi_1.Fraction = f1;
            persi_2.Fraction = f2;
            if (persi_1.Fraction == -1)
            {
                for (int i = 0; i < 9; i++)
                {
                    X = persi_1.GameStep(X);
                    winner = WinController(X);
                    if (winner != 0)
                        break;
                    X = persi_2.GameStep(X);
                    winner = WinController(X);
                    if (winner != 0)
                        break;
                }
            }
            else if (persi_2.Fraction == -1)
            {
                for (int i = 0; i < 9; i++)
                {
                    X = persi_2.GameStep(X);
                    winner = WinController(X);
                    if (winner != 0)
                        break;
                    X = persi_1.GameStep(X);
                    winner = WinController(X);
                    if (winner != 0)
                        break;
                }
            }
            if (winner == persi_1.Fraction) persi_1.WinCounter++;
            else if (winner == persi_2.Fraction) persi_2.WinCounter++;
            //else if (winner == 0) { persi_1.WinCounter += 0.5; persi_2.WinCounter += 0.5; }
                //Обнуляем фракции
                persi_1.Fraction = 0;
            persi_2.Fraction = 0;
            return winner;
        }
        //Контроллер победителей
        public int WinController(int[] x)
        {
            int WinFraction = 0;
            int[,] y = new int[3,3];
            int u = 0;
            int k1 = 0, k2 = 0;//Кол-во отметок в строке
            int q1 = 0, q2 = 0;//Кол-во отметок в столбце
            int d1 = 0, d2 = 0;//Кол-во отметок в диагонали 1
            //Переводим доску в матричный вид
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    y[i, j] = x[u];
                    u++;
                }
            }

            //--------------------------------------------
            //Проверяем
            for (int i = 0; i < 3; i++)
            {
                k1 = 0;
                q1 = 0;
                k2 = 0;
                q2 = 0;
                for (int j = 0; j < 3; j++)
                {
                    //Проверяем строки
                    if (y[i, j] == 1) k1++;
                    else if (y[i, j] == -1) k2++;
                    if (k1 >= 3) WinFraction = 1;
                    if (k2 >= 3) WinFraction = -1;
                    //Проверяем столбцы
                    if (y[j, i] == 1) q1++;
                    else if (y[j, i] == -1) q2++;
                    if (q1 >= 3) WinFraction = 1;
                    if (q2 >= 3) WinFraction = -1;
                    //Проверяем диагональ 1
                    if (i == j)
                    {
                        if (y[j, i] == 1) d1++;
                        else if (y[i, j] == -1) d2++;
                        if (d1 >= 3) WinFraction = 1;
                        if (d2 >= 3) WinFraction = -1;
                    }
                }
            }
            //Проверяем диагональ 2
            if (y[0, 2] == 1 & y[2, 0] == 1 & y[1, 1] == 1)
                WinFraction = 1;
            if (y[0, 2] == -1 & y[2, 0] == -1 & y[1, 1] == -1)
                WinFraction = -1;
            //---------------------------------------------
            return WinFraction;
        }
        //Функция селекции, после неё остается n - N особей!
        public void Selection(int N)
        {
            //Каждый играет с каждым сначала одной фракцией
            int m = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = m; j < n; j++)
                {
                    //Обнуляем состояние доски
                    X = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    if (i != j)
                        Game(PersiList[i], PersiList[j],-1,1);
                }
                m++;
            }
            //Каждый играет с каждым другой фракцией
            m = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = m; j < n; j++)
                {
                    //Обнуляем состояние доски
                    X = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    if (i != j)
                        Game(PersiList[i], PersiList[j], 1, -1);
                }
                m++;
            }
            m = 0;
            //---------------------------Далее отбор-------------------
            //Сортируем особи по кол-ву побед, от слабого к сильному
            PersiList.Sort(delegate (Perseptron persi_1, Perseptron persi_2)
            { return persi_1.WinCounter.CompareTo(persi_2.WinCounter); });
            //Отсеиваем половину поколения
            for (int i = 0; i < N; i++)
                PersiList.Remove(PersiList.First());
            n -= N;
            //Считаем показатель эффективности
            K = PersiList.Last().WinCounter/ PersiList.Last().game;
        }
        //Функция быстрогоскрещивания, скрещивает всех особей по принципу: 
        //слабый-><-сильный = новый
        //В результате популяция уменьшается в два раза, или увеличивается в 1 раз, (upPopulation = true)
        //Скрещивает сразу нейроны а не веса
        public void FastCrossbreeding(bool upPopulation)
        {
            //Новое поколение
            List<Perseptron> NewPersiList = new List<Perseptron>();
            //Скрещиваем
            for (int i = 0; i < n/2; i++)
            {
                Perseptron persi = new Perseptron(Neurons);
                //Скрещиваем скрытые нейроны
                for (int j = 0; j < Neurons; j++)
                {
                        persi.InvisibleNeurons_1[j] = PersiList[Change(i, n - i - 1)].InvisibleNeurons_1[j];
                        persi.InvisibleNeurons_2[j] = PersiList[Change(i, n - i - 1)].InvisibleNeurons_2[j];
                }
                //Скрещиваем выходные нейроны
                persi.on = PersiList[Change(i, n - i - 1)].on;
                //Добавляем новую особь в список
                NewPersiList.Add(persi);
            }
            //Тут два варианта, либо добавлять особей
            if (upPopulation)
            {
                for (int i = 0; i < n/2; i++)
                    PersiList.Add(NewPersiList[i]);
                n += n/2;
            }
            else
            //либо замещать популяцию
            {
                PersiList.Clear();
                for (int i = 0; i < n/2; i++)
                    PersiList.Add(NewPersiList[i]);
            }
        }
        //Функция скрещивания, скрещивает всех особей по принципу: 
        //слабый-><-сильный = новый
        //В результате популяция уменьшается в два раза, или увеличивается в 1 раз, (upPopulation = true)
        public void Crossbreeding(bool upPopulation)
        {
            //Новое поколение
            List<Perseptron> NewPersiList = new List<Perseptron>();
            //Скрещиваем
            for (int i = 0; i < n/2; i++)
            {
                Perseptron persi = new Perseptron(Neurons);
                //Скрещиваем скрытые нейроны
                for (int j = 0; j < Neurons; j++)
                {
                    for (int l = 0; l < 9; l++)
                        persi.InvisibleNeurons_1[j].weights[l] = PersiList[Change(i, n - i - 1)].InvisibleNeurons_1[j].weights[l];
                    for (int l = 0; l < Neurons; l++)
                    {
                        persi.InvisibleNeurons_2[j].weights[l] = PersiList[Change(i, n - i - 1)].InvisibleNeurons_2[j].weights[l];
                        persi.InvisibleNeurons_3[j].weights[l] = PersiList[Change(i, n - i - 1)].InvisibleNeurons_3[j].weights[l];
                    }

                }
                //Скрещиваем выходные нейроны
                for (int j = 0; j < Neurons; j++)
                    persi.on.weights[j] = PersiList[Change(i, n - i - 1)].on.weights[j];
                //Добавляем новую особь в список
                NewPersiList.Add(persi);
            }
            //Тут два варианта, либо добавлять особей
            if (upPopulation)
            {
                for (int i = 0; i < n/2; i++)
                    PersiList.Add(NewPersiList[i]);
                n += n/2;
            }
            else
            //либо замещать популяцию
            {
                PersiList.Clear();
                for (int i = 0; i < n/2; i++)
                    PersiList.Add(NewPersiList[i]);
                n = n / 2;
            }
        }
        //Функция Мутации
        public void Mutation(double chance)//chance 0 - 1
        {
            double ThisChance = Program.rnd.NextDouble();
            int RndPersi = Program.rnd.Next(0,n);
            if (ThisChance > chance)//Проводим мутацию если шанс достигнут
            {
                for (int m = 0; m < Neurons; m++)
                {
                    for (int j = 0; j < 9; j++)
                        PersiList[RndPersi].InvisibleNeurons_1[m].weights[j] += 0.3 - Program.rnd.NextDouble() * 0.6;
                    for (int j = 0; j < Neurons; j++)
                    {
                        PersiList[RndPersi].InvisibleNeurons_2[m].weights[j] += 0.3 - Program.rnd.NextDouble() * 0.6;
                        PersiList[RndPersi].InvisibleNeurons_3[m].weights[j] += 0.3 - Program.rnd.NextDouble() * 0.6;
                    }
                }
                for (int j = 0; j < Neurons; j++)
                    PersiList[RndPersi].on.weights[j] += 0.3 - Program.rnd.NextDouble() * 0.6;
            }

        }
        //Функция выбирающая случайный ответ из предложенных вариантов
        private int Change(int a, int b)
        {
            int change = Program.rnd.Next(100);
            if (change < 50)
                return a;
            else return b;
        }
    }
}
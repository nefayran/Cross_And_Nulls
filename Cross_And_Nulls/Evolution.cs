﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_And_Nulls
{
    public class Evolution
    {
        int n = 4;//Размер поколения
        public List<Perseptron> PersiList;//Список особей
        public int[] X;//Доска
        public Evolution()
        {
            PersiList = new List<Perseptron>();
            Born();
            X = new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //Каждый играет с каждым
            int m = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = m; j < n; j++)
                {
                    if(i!=j)
                    Game(PersiList[i], PersiList[j]);
                }
                m++;  
            }
            int q = 0;
        }
        //Функция рождения первго поколения
        public void Born()
        {
            for (int i = 0; i < n; i++)
            {
                Perseptron persi = new Perseptron();
                persi.Initialization();
                PersiList.Add(persi);
            }
        }
        //Функция игры
        public int Game(Perseptron persi_1, Perseptron persi_2)
        {
            int winner = 0;
            persi_1.game++;
            persi_2.game++;
            //Выбираем стороны
            persi_1.Fraction = 1;//Нолики
            persi_2.Fraction = -1;//Крестики
            //Обнуляем состояние доски
            X = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 9; i++)
            {
                X = persi_1.GameStep(X);
                winner = WinController(X);
                if (winner != 0)
                    break;
                X = persi_2.GameStep(X);
                winner = WinController(X);
                if (WinController(X) != 0)
                    break;
            }
            if (winner == persi_1.Fraction) persi_1.WinCounter++;
            if (winner == persi_2.Fraction) persi_2.WinCounter++;
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
        //Функция селекции
        public void Selection(Perseptron persi_1, Perseptron persi_2)
        {
            //Program.rnd
            //persi_1.in1.weights[]
        }
    }
}
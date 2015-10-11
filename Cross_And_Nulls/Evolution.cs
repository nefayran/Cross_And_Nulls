using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_And_Nulls
{
    public class Evolution
    {
        int n = 3;//Размер поколения
        public List<Perseptron> PersiList;//Список особей
        public Evolution()
        {
            PersiList = new List<Perseptron>();
            Born();
            Game(PersiList[0], PersiList[1]);
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
        public void Game(Perseptron persi_1, Perseptron persi_2)
        {
            //Выбираем стороны
            persi_1.Fraction = 1;
            persi_2.Fraction = -1;
            //Состояние доски
            int[] X = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int step_1 = persi_1.GameStep(X);
            int step_2 = persi_2.GameStep(X);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wielowatkowePrzeszukanieTablicy
{
    class Program
    {
        /// <summary>
        /// Tab init with 1 mln elements
        /// </summary>
        public static int[] tab;
        /// <summary>
        /// Tutaj watki sprawdzają czy znaleziono wartosc
        /// </summary>
        public static bool findStatus = false;
        /// <summary>
        /// Informacja ktory watek znalazl wartosc
        /// </summary>
        public static string information;

        public static void tabInit()
        {
            tab = new int[1000000];
            Random rand = new Random();

            //losowe wartosci do tabeli miedzy 2 a 99
            for (int i = 0; i < tab.Count(); i++)
            {
                tab[i] = rand.Next(2, 99);
            }

            //ustawienie 1 w losowym miejscu
            int value = rand.Next(0, 999999);
            tab[value] = 1;
        }


        static void Main(string[] args)
        {
            tabInit();

            Thread th1 = new Thread(delegate () {
                for (int i = 0; i < 499999; i++)
                {
                    if (findStatus == true) Thread.CurrentThread.Abort();
                    if (tab[i] == 1 && findStatus == false)
                    {
                        findStatus = true;
                        information = "Watek TH1 znalazl wartosc na pozycji: " + i;
                    }
                }

            });
            Thread th2 = new Thread(delegate () {
                for (int i = 499999; i < 1000000; i++)
                {
                    if (findStatus == true) Thread.CurrentThread.Abort();
                    if (tab[i] == 1 && findStatus == false)
                    {
                        findStatus = true;
                        information = "Watek TH2 znalazl wartosc na pozycji: " + i;
                    }
                }

            });

            th1.Start();
            th2.Start();

            while (findStatus == false)
            {
                Thread.Sleep(500);
            }

            Console.WriteLine(information);
            Console.ReadKey();

        }
    }
}
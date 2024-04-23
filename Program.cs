using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _2048 game = new _2048(10);
            game.PlaceNew();
            game.PlaceNew();
            

            game.Display();
            
            while (true)
            {
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        game.Left();
                        game.PlaceNew();
                        break;


                    case ConsoleKey.RightArrow:
                        game.Right();
                        game.PlaceNew();
                        break;


                    case ConsoleKey.UpArrow:
                        game.Up();
                        game.PlaceNew();
                        break;


                    case ConsoleKey.DownArrow:
                        game.Down();
                        game.PlaceNew();
                        break;
                }
                Console.Clear();
                game.Display();

                if(false&&game.MaxNum() == 2048)
                {
                    Console.WriteLine("Vyhral jsi");
                    break;
                }
                if(game.MinNum() != 0)
                {
                    Console.WriteLine("Prohral jsi");
                    break;
                }

            }
            Thread.Sleep(10000);
            Console.ReadKey();
        }
    }


    class _2048
    {
        int[,] pole = new int[4,4];

        public _2048(int len)
        {
            pole = new int[len, len];
            init();
        }
        public _2048() => init();

        private void init()
        {
            for (int x = 0; x < pole.GetLength(0); x++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    pole[x, y] = 0;
                }
            }
        }



        private Random random = new Random();

        public void Display()
        {

            int maxLen = Convert.ToString(MaxNum()).Length;

            for (int x = 0; x < pole.GetLength(0); x++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    int number = pole[x, y];
                    Console.ForegroundColor = getColorFromInt(number);
                    Console.Write(number.ToString().PadRight(maxLen)+" ");
                }
                Console.WriteLine();
            }
        }

        private ConsoleColor getColorFromInt(int cislo)
        {
            if (cislo == 0)
                return ConsoleColor.White;

            int i = 0;
            while(cislo != 2)
            {
                cislo /= 2;
                i++;
            }
            i++;
            return (ConsoleColor)i;
        }

        public int MinNum()
        {
            int minNum = int.MaxValue;
            for (int x = 0; x < pole.GetLength(0); x++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    if (minNum > pole[x,y])
                    {
                        minNum = pole[x,y];
                    }
                }
            }
            return minNum;
        }

        public int MaxNum()
        {
            int maxNum = -1;
            for (int x = 0; x < pole.GetLength(0); x++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {

                    if (pole[x, y] > maxNum)
                        maxNum = pole[x, y];
                }
            }
            return maxNum;
        }
        public void PlaceNew()
        {
            while (true)
            {
                int X = random.Next(pole.GetLength(0));
                int Y = random.Next(pole.GetLength(1));
                if (pole[X, Y] == 0)
                {
                    pole[X, Y] = 2;
                    break;
                }
            }
        }

        private void move(int y, int x)
        {
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int ix = 0; ix < pole.GetLength(0); ix++)
                {
                    for (int iy = 0; iy < pole.GetLength(1); iy++)
                    {
                        if (ix + x < pole.GetLength(0) && iy + y < pole.GetLength(1) && ix + x >= 0 && iy + y >= 0)
                        {
                            if (pole[ix + x, iy + y] == 0)
                            {
                                pole[ix + x, iy + y] = pole[ix, iy];
                                pole[ix, iy] = 0;
                            }
                            else
                            {
                                if (pole[ix + x,iy+y] == pole[ix,iy])
                                {
                                    pole[ix+x, iy+y] *= 2;
                                    pole[ix, iy] = 0;
                                }
                            }
                            
                        }
                    }
                }
            }



        }


        public void Left() => move(-1,0);
        public void Right() => move(+1,0);
        public void Up() => move(0,-1);
        public void Down() => move(0,+1);
        
    }
}

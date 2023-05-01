using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCopy
{
    class player
    {
        public int HeroX;
        public int HeroY;
        public int score;
        public int Herohealth;
        public int enemyX;
        public int enemyY;
        public int Enemyhealth;

        public void printHeroHealth(player heroCo)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(75, 10);
            Console.WriteLine("Hero Health: " + heroCo.Herohealth + " ");

            Console.SetCursorPosition(75, 12);
            Console.WriteLine("Mr Fighter Health: " + heroCo.Enemyhealth + " ");

            Console.SetCursorPosition(75, 8);
            Console.WriteLine("Score: " + heroCo.score + " ");
        }
        public void gameoverCollsion(player heroCo)
        {

            if (heroCo.Enemyhealth < 0)
            {
                for (int i = -2; i < 3; i++)
                {
                    if (heroCo.HeroX + 3 == heroCo.enemyX - 1 && heroCo.HeroY == heroCo.enemyY + i)
                    {
                        heroCo.Herohealth = 0;
                    }
                }

                if (heroCo.HeroX + 3 == heroCo.enemyX - 1 && heroCo.HeroY == heroCo.enemyY)
                {
                    heroCo.Herohealth = 0;
                }

                for (int i = -3; i < 8; i++)
                {
                    if (heroCo.HeroX == heroCo.enemyX + i && heroCo.HeroY - 1 == heroCo.enemyY + 2)
                    {
                        heroCo.Herohealth = 0;
                    }
                }

                for (int i = -3; i < 8; i++)
                {
                    if (heroCo.HeroX == heroCo.enemyX + i && heroCo.HeroY + 2 == heroCo.enemyY - 1)
                    {
                        heroCo.Herohealth = 0;
                    }
                }
            }
        }

    }
}

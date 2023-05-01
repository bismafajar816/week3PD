using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using gameCopy;
using EZInput;

namespace Task02
{
    class Program
    {
        static char[,] Maze = new char[36, 72];

        static char i = (char)2;
        static char k = (char)19;
        static char[,] hero = new char[3, 4] {
    { ' ', i, ' ',  ' ' },
    { '<', '#', '-',  '>' },
    { ' ', k, ' ' , ' ' }};
        static char[,] HeroLeft = new char[3, 4] {
    { ' ', i, ' ',  ' ' },
    { '<', '-', '#',  '>' },
    { ' ', k, ' ' , ' ' }};
        static char h = (char)127;
        static char m = (char)193;
        static char[,] Enemy = new char[3, 5] {
                                             { ' ',  ' ', h, h, ' ' },
                                             { '<',  '-', m, m, '>' },
                                             { ' ',  ' ', '!', '!', ' ' }
                                             };





        static string Direction = "right";


        static int[] HerobulletX = new int[100];
        static int[] HerobulletY = new int[100];
        static char[] HerobulletDirection = new char[100];
        static int HerobulletCount = 0;


        static string enemydirection = "down";


        static int score = 0; // score

        static int check = 0; // flags for game
        static bool game = true;

        static void Main(string[] args)
        {
            player heroCO = new player();
            heroCO.HeroX = 13;
            heroCO.HeroY = 20;
            heroCO.score = 0;
            heroCO.Herohealth = 100;
            heroCO.enemyX = 18;
            heroCO.enemyY = 22;
            heroCO.Enemyhealth = 50;
            LoadMaze(Maze);


            Header();
            int opt = Menu();
            while (opt < 3)
            {
                Console.Clear();
                if (opt == 1)
                {


                    PrintMaze(Maze);
                    PrintEnemy(Enemy, heroCO);
                    printHero(hero, heroCO, Maze);

                    while (game)
                    {

                       heroCO.printHeroHealth(heroCO);
                        EnemyMotion(Maze, Enemy, heroCO, ref enemydirection);
                        gameover(heroCO, ref game);
                        heroCO.gameoverCollsion(heroCO);
                        MotionOfHero(Maze, hero, HeroLeft, heroCO, ref Direction, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount, ref check, ref game);
                        movebullet(Maze, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount);
                        collision(heroCO, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount);
                        Thread.Sleep(40);
                        if (heroCO.Herohealth <= 0)
                            Console.Clear();
                                
                    }

                }
                if (opt == 2)
                {
                    Console.Clear();
                    Header();
                    int choice = Option2();
                    while (choice < 3)
                    {
                        if (choice == 1)
                        {
                            Console.Clear();
                            Header();
                            keys();
                            choice = Option2();
                        }
                        if (choice == 2)
                        {
                            Console.Clear();
                            Header();
                            instructions();
                            choice = Option2();
                        }
                        if (choice > 2 || choice == 0)
                        {
                            Console.Clear();
                            Header();
                            //* choice = Option2();*//*
                        }

                    }
                    opt = Menu();

                }

            }


            Console.ReadKey();
        }
        static void Header()
        {
            Console.WriteLine("     /$$      /$$                                      /$$$$$$  ");
            Console.WriteLine("   | $$$    /$$$                                     /$$__  $$                  ");
            Console.WriteLine("   | $$$$  /$$$$  /$$$$$$  /$$$$$$$$  /$$$$$$       | $$  \\__/  /$$$$$$  /$$$$$$  /$$$$$$$$  /$$$$$$  ");
            Console.WriteLine("   | $$ $$/$$ $$ |____  $$|____ /$$/ /$$__  $$      | $$       /$$__  $$|____  $$|____ /$$/ /$$__  $$ ");
            Console.WriteLine("   | $$  $$$| $$  /$$$$$$$   /$$$$/ | $$$$$$$$      | $$      | $$  \\__/ /$$$$$$$   /$$$$/ | $$$$$$$$ ");
            Console.WriteLine("   | $$\\  $ | $$ /$$__  $$  /$$__/  | $$_____/      | $$    $$| $$      /$$__  $$  /$$__/  | $$_____/ ");
            Console.WriteLine("   | $$ \\/  | $$|  $$$$$$$ /$$$$$$$$|  $$$$$$$      |  $$$$$$/| $$     |  $$$$$$$ /$$$$$$$$|  $$$$$$$ ");
            Console.WriteLine("   |__/     |__/ \\_______/|________/ \\_______/       \\______/ |__/      \\_______/|________/ \\_______/ ");
        }
        static int Menu()
        {
            int option;
            Console.WriteLine("\n\n\n\nMenu");
            Console.WriteLine("___________________________________");
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Option");
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nYour Choice: ");
            option = int.Parse(Console.ReadLine());
            return option;
        }
        static int Option2()
        {
            int choice;
            Console.WriteLine("\n\n\n_________________________________");
            Console.WriteLine("1. Keys");
            Console.WriteLine("2. Instructions");
            Console.WriteLine("3. Exit");
            Console.WriteLine("\nYour Choice: ");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
        static void keys()
        {
            Console.WriteLine("Keys\t\t\t\t Functions");
            Console.WriteLine("Up arrow\t\t\t Move character upward");
            Console.WriteLine("Down arrow\t\t\t Move character downward");
            Console.WriteLine("Left arrow\t\t\t Move character leftward");
            Console.WriteLine("Right arrow\t\t\t Move character rightward");
            Console.WriteLine("Space Bar\t\t\t  Hero Bullets");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void instructions()
        {
            Console.WriteLine("You can press arrow keys for movement and space key for shooting");
            Console.WriteLine("Your score will increase by eating food pallets.");
            Console.WriteLine("Your health will decrease by colliding with enemy or by walls.");
            Console.WriteLine("Be careful while shooting the direction of your bullets will be in the direction of printing the player.");
            Console.WriteLine("It is a game of seconds try to keep away from walls and enemy . if wall is right next to you and you are pressing arrow key in direction of wall then your health will decrease continuously.");
            Console.WriteLine("If wall or enemy are exactly next to you then your health will decrease.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        static void PrintMaze(char[,] Maze)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 70; j++)
                {
                    Console.Write(Maze[i, j]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void EraseMaze()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 70; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void moveheroDown(char[,] Maze, char[,] hero, char[,] HeroLeft, player heroCo, ref string Direction)
        {
            char next = Maze[heroCo.HeroY + 3, heroCo.HeroX];
            char next1 = Maze[heroCo.HeroY + 3, heroCo.HeroX + 1];
            char next2 = Maze[heroCo.HeroY + 3, heroCo.HeroX + 2];
            char next3 = Maze[heroCo.HeroY + 3, heroCo.HeroX + 3];
            if (next == ' ' && next1 == ' ' && next2 == ' ' && next3 == ' ')
            {
                eraseHero(heroCo);
                heroCo.HeroY++;
                if (Direction == "right")
                {
                    printHero(hero, heroCo, Maze);
                }
                else
                {
                    printHeroLeft(HeroLeft, heroCo);
                }
            }

            else if (next == '.' || next1 == '.' || next2 == '.' || next3 == '.')
            {
                heroCo.score = heroCo.score + 1;
                eraseHero(heroCo);
                heroCo.HeroY++;
                if (Direction == "right")
                {
                    printHero(hero, heroCo, Maze);
                }
                else
                {
                    printHeroLeft(HeroLeft, heroCo);
                }
            }
            else if (next == '#' || next1 == '#' || next2 == '#' || next3 == '#')
            {
                heroCo.Herohealth--;
            }
        }

        static void moveheroUp(char[,] Maze, char[,] hero, char[,] HeroLeft, player heroCo, ref string Direction)
        {
            char next = Maze[heroCo.HeroY - 1, heroCo.HeroX];
            char next1 = Maze[heroCo.HeroY - 1, heroCo.HeroX + 1];
            char next2 = Maze[heroCo.HeroY - 1, heroCo.HeroX + 2];
            char next3 = Maze[heroCo.HeroY - 1, heroCo.HeroX + 3];
            if (next == ' ' && next1 == ' ' && next2 == ' ' && next3 == ' ')
            {
                eraseHero(heroCo);
                heroCo.HeroY--;
                if (Direction == "right")
                {
                    printHero(hero, heroCo, Maze);
                }
                else
                {
                    printHeroLeft(HeroLeft, heroCo);
                }
            }

            else if (next == '.' || next1 == '.' || next2 == '.' || next3 == '.')
            {
                heroCo.score = heroCo.score + 1;

                eraseHero(heroCo);
                heroCo.HeroY--;
                if (Direction == "right")
                {
                    printHero(hero, heroCo, Maze);
                }
                else
                {
                    printHeroLeft(HeroLeft, heroCo);
                }
            }

            else if (next == '#' || next1 == '#' || next2 == '#' || next3 == '#')
            {
                heroCo.Herohealth--;
            }

        }

        static void moveheroLeft(char[,] Maze, char[,] hero, char[,] HeroLeft, player heroCo, ref string Direction)
        {
            Direction = "left";
            char next = Maze[heroCo.HeroY + 4, heroCo.HeroX + 1];
            /*   char next = Maze[heroCo.HeroY, heroCo.HeroX - 1];*/
            char next1 = Maze[heroCo.HeroY - 1, heroCo.HeroX - 1];
            char next2 = Maze[heroCo.HeroY + 2, heroCo.HeroX - 1];
            if (next == ' ' && next1 == ' ' && next2 == ' ')
            {
                eraseHero(heroCo);
                heroCo.HeroX--;
                printHeroLeft(HeroLeft, heroCo);
            }
            if (next == '.' || next1 == '.' || next2 == '.')
            {
                heroCo.score = heroCo.score + 1;
                eraseHero(heroCo);
                heroCo.HeroX--;
                printHeroLeft(HeroLeft, heroCo);
            }
            if (next == '#' || next1 == '#' || next2 == '#')
            {
                heroCo.Herohealth--;
            }
        }

        static void moveheroRight(char[,] Maze, char[,] hero, char[,] heroLeft, player heroCo, ref string Direction)
        {
            Direction = "right";
            char next = Maze[heroCo.HeroY + 1, heroCo.HeroX + 4];
            char next1 = Maze[heroCo.HeroY + 1, heroCo.HeroX + 4];
            char next2 = Maze[heroCo.HeroY + 2, heroCo.HeroX + 4];
            char next3 = Maze[heroCo.HeroY + 1, heroCo.HeroX + 1];
            if (next == ' ' && next1 == ' ' && next2 == ' ' && next3 == ' ')
            {
                eraseHero(heroCo);
                heroCo.HeroX++;
                printHero(hero, heroCo, Maze);
            }
            if (next == '.' || next1 == '.' || next2 == '.' || next3 == '.')
            {
                heroCo.score = heroCo.score + 1;
                eraseHero(heroCo);
                heroCo.HeroX++;
                printHero(hero, heroCo, Maze);
            }
            if (next == '#' || next1 == '#' || next2 == '#' || next3 == '#')
            {
                heroCo.Herohealth--;
            }


        }

        static void MotionOfHero(char[,] Maze, char[,] hero, char[,] HeroLeft, player heroCo, ref string Direction, int[] HerobulletX, int[] HerobulletY, char[] HerobulletDirection, ref int HerobulletCount, ref int check, ref bool game)
        {
            if (heroCo.Enemyhealth > 0)
            {


                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    moveheroLeft(Maze, hero, HeroLeft, heroCo, ref Direction);
                }

                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    moveheroRight(Maze, hero, HeroLeft, heroCo, ref Direction);
                }
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {

                    moveheroUp(Maze, hero, HeroLeft, heroCo, ref Direction);

                }
                if (Keyboard.IsKeyPressed(Key.DownArrow))
                {
                    moveheroDown(Maze, hero, HeroLeft, heroCo, ref Direction);
                }
                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    createBullet(Maze, heroCo, ref Direction, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount);
                }
            }
            else
            {
                Console.Clear();
            }
        }

        static void printHero(char[,] hero, player heroCo, char[,] Maze)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int x = heroCo.HeroX;
            int y = heroCo.HeroY;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 4; j++)
                {
                    Maze[i, j] = hero[i, j];
                    Console.Write(Maze[i, j]);
                }
                Console.WriteLine();
                y++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void eraseHero(player heroCo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int x = heroCo.HeroX;
            int y = heroCo.HeroY;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
                y++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void printHeroLeft(char[,] heroLeft, player heroCo)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int x = heroCo.HeroX;
            int y = heroCo.HeroY;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(heroLeft[i, j]);
                }
                Console.WriteLine();
                y++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintEnemy(char[,] Enemy, player heroCo)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            int x = heroCo.enemyX;
            int y = heroCo.enemyY;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(Enemy[i, j]);
                }
                Console.WriteLine();
                y++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void EraseEnemy(player heroCo)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            int x = heroCo.enemyX;
            int y = heroCo.enemyY;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
                y++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void createBullet(char[,] Maze, player heroCo, ref string Direction, int[] HerobulletX, int[] HerobulletY, char[] HerobulletDirection, ref int HerobulletCount)
        {
            if (Direction == "right")
            {

                char next = Maze[heroCo.HeroY + 1, heroCo.HeroX + 4];
                if (next == ' ')
                {
                    HerobulletX[HerobulletCount] = heroCo.HeroX + 4;
                    HerobulletY[HerobulletCount] = heroCo.HeroY + 1;
                    HerobulletDirection[HerobulletCount] = 'R';
                    Console.SetCursorPosition(heroCo.HeroX + 4, heroCo.HeroY + 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*");
                    HerobulletCount++;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }

            if (Direction == "left")
            {

                char next = Maze[heroCo.HeroY + 1, heroCo.HeroX - 1];
                if (next == ' ')
                {
                    HerobulletX[HerobulletCount] = heroCo.HeroX - 1;
                    HerobulletY[HerobulletCount] = heroCo.HeroY + 1;
                    HerobulletDirection[HerobulletCount] = 'L';
                    Console.SetCursorPosition(heroCo.HeroX - 1, heroCo.HeroY + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*");
                    HerobulletCount++;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
        }
        static void movebullet(char[,] Maze, int[] HerobulletX, int[] HerobulletY, char[] HerobulletDirection, ref int HerobulletCount)
        {
            for (int x = 0; x < HerobulletCount; x++)
            {
                if (HerobulletDirection[x] == 'R')
                {
                    char next = Maze[HerobulletY[x], HerobulletX[x] + 1];
                    if (next != ' ')
                    {
                        eraseBullet(HerobulletX[x], HerobulletY[x]);
                        deleteBullet(x, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount);
                    }
                    else
                    {
                        eraseBullet(HerobulletX[x], HerobulletY[x]);
                        HerobulletX[x]++;
                        printBullet(HerobulletX[x], HerobulletY[x]);
                    }
                }

                if (HerobulletDirection[x] == 'L')
                {
                    char next = Maze[HerobulletY[x], HerobulletX[x] - 1];
                    if (next != ' ')
                    {
                        eraseBullet(HerobulletX[x], HerobulletY[x]);
                        deleteBullet(x, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount);
                    }
                    else
                    {
                        eraseBullet(HerobulletX[x], HerobulletY[x]);
                        HerobulletX[x]--;
                        printBullet(HerobulletX[x], HerobulletY[x]);
                    }
                }
            }
        }

        static void deleteBullet(int index, int[] HerobulletX, int[] HerobulletY, char[] HerobulletDirection, ref int HerobulletCount)
        {
            int x = index;
            while (x < HerobulletCount)
            {
                HerobulletX[x] = HerobulletX[x + 1];
                HerobulletY[x] = HerobulletY[x + 1];
                HerobulletDirection[x] = HerobulletDirection[x + 1];
                x++;
            }
            HerobulletCount--;
        }

        static void printBullet(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("*");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void eraseBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(" ");
        }
        static void EnemyMotion(char[,] Maze, char[,] Enemy, player heroCo, ref string enemydirection)
        {
            if (heroCo.Enemyhealth > 0)
            {
                if (enemydirection == "up")
                {
                    char next = Maze[heroCo.enemyY - 1, heroCo.enemyX];
                    if (next == ' ')
                    {
                        EraseEnemy(heroCo);
                        heroCo.enemyY--;
                        PrintEnemy(Enemy, heroCo);
                    }
                    if (next == '#')
                    {
                        enemydirection = "down";
                    }
                }
                if (enemydirection == "down")
                {
                    char next = Maze[heroCo.enemyY + 3, heroCo.enemyX];
                    if (next == ' ')
                    {
                        EraseEnemy(heroCo);
                        heroCo.enemyY++;
                        PrintEnemy(Enemy, heroCo);
                    }
                    if (next == '#')
                    {
                        enemydirection = "up";
                    }
                }
            }
            if (heroCo.Enemyhealth == 0)
            {
                EraseEnemy(heroCo);
                heroCo.Enemyhealth = -1;
                heroCo.enemyX = 0;
                heroCo.enemyY = 0;
                Console.SetCursorPosition(75, 12);
                Console.WriteLine(" Mr - Fighter Died ");
            }
        }
        static void gameover(player heroCo, ref bool game)
        {
            if (heroCo.Herohealth <= 0)
            {
                game = false;
                Console.Clear();
                eraseHero(heroCo);
                EraseEnemy(heroCo);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(27, 19);
                Console.WriteLine("GAME OVER");
                EraseMaze();
                EraseEnemy(heroCo);
                eraseHero(heroCo);
                Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


      
    

        static void LoadMaze(char[,] Maze)
        {
            StreamReader file = new StreamReader("maze.txt");
            string line;
            int row = 0;
            while ((line = file.ReadLine()) != null)
            {
                for (int x = 0; x < 70; x++)
                {
                    Maze[row, x] = line[x];
                }
                row++;
                if (row > 36)
                {
                    break;
                }
            }

            file.Close();
        }

        static void collision(player heroCo, int[] HerobulletX, int[] HerobulletY, char[] HerobulletDirection, ref int HerobulletCount)
        {
            for (int x = 0; x < HerobulletCount; x++)
            {
                if (heroCo.Enemyhealth > 0)
                {
                    if (HerobulletX[x] + 1 == heroCo.enemyX && (HerobulletY[x] == heroCo.enemyY || HerobulletY[x] == heroCo.enemyY + 1 || HerobulletY[x] == heroCo.enemyY + 2))
                    {
                        eraseBullet(HerobulletX[x], HerobulletY[x]);
                        heroCo.Enemyhealth = heroCo.Enemyhealth - 1;
                        heroCo.score++;
                        deleteBullet(x, HerobulletX, HerobulletY, HerobulletDirection, ref HerobulletCount);
                    }
                }
            }
        }
    }
}

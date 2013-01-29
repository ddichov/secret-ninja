using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

class GameFollingRocks
{

    static int playerPosition = 0;
    static int life = 5;
    static string rocks = "^@*&+%$#!.;";
    static int[] rocksPositinsX = new int[40];
    static int[] rocksRepeat = new int[40];
    static int[] rocksColor = new int[40];
    static char[] symbolType = new char[40];
    static Random randomGenerator = new Random();
    static int score = 0;

    static void RemoveScrollBars()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
    }

    static void PrintAtPosition(int x, int y, int color, int repeat, char symbol)
    {
        Console.SetCursorPosition(x, y);
        switch (color)
        {
            case 1: Console.ForegroundColor = ConsoleColor.Blue; break;
            case 2: Console.ForegroundColor = ConsoleColor.Green; break;
            case 3: Console.ForegroundColor = ConsoleColor.Red; break;
            case 4: Console.ForegroundColor = ConsoleColor.White; break;
            case 5: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
            case 6: Console.ForegroundColor = ConsoleColor.Cyan; break;
            default: Console.ForegroundColor = ConsoleColor.Yellow; break;
        }
        Console.Write(symbol);
        if (repeat > 75 && repeat < 95)
        {
            Console.Write(symbol);
        }
        if (repeat > 95)
        {
            Console.Write("{0}{0}", symbol);
        }
    }

    static void DrawDwarf()
    {
        Console.SetCursorPosition(playerPosition, Console.WindowHeight - 1);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("(O)");
    }

    static void MovePlayerRight()
    {
        if (playerPosition < (Console.WindowWidth - 4))
        {
            playerPosition++;
        }
    }

    static void MovePlayerLeft()
    {
        if (playerPosition > 0)
        {
            playerPosition--;
        }
    }

    static void PrintResult()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (score > 0)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, 0);
            Console.Write("Life {0}  Score: {1}", life, score);
        }
        else
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, 0);
            Console.Write("Life {0}  Score: {1}", life, 0);
        }
    }

    static void MoveRows()
    {
        //change coordinates y;
        for (int i = Console.WindowHeight - 1; i >= 1; i--)
        {
            rocksPositinsX[i] = rocksPositinsX[i - 1];
            symbolType[i] = symbolType[i - 1];
            rocksRepeat[i] = rocksRepeat[i - 1];
            rocksColor[i] = rocksColor[i - 1];

            PrintAtPosition(rocksPositinsX[i], i, rocksColor[i], rocksRepeat[i], symbolType[i]);
        }
    }
    static void DrawFirstRow()
    {
        int randomNumber = randomGenerator.Next(1, Console.WindowWidth);
        int randomChar = randomGenerator.Next(0, 11);
        int randomRepeat = randomGenerator.Next(1, 101);
        int randomColor = randomGenerator.Next(1, 7);
        char symbol = rocks[randomChar];
        rocksPositinsX[0] = randomNumber;
        symbolType[0] = symbol;
        rocksRepeat[0] = randomRepeat;
        rocksColor[0] = randomColor;
        // TO DO: insert second symbol on the same row ?
        PrintAtPosition(randomNumber, 1, randomColor, randomRepeat, symbol);
    }

    static void SetInitialData()
    {
        for (int i = 0; i < Console.WindowHeight; i++)
        {
            rocksPositinsX[i] = 0;
            symbolType[i] = ' ';
            rocksRepeat[i] = 0;
            rocksColor[i] = 0;
            score = -Console.WindowHeight + 1;
            life = 5;
            playerPosition = Console.WindowWidth / 2 - 1;
        }
    }

    static void ReduceLife()
    {
        if (life > 0)
        {
            life--;
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Game Over!");
            Console.WriteLine("Your Score is:{0}", score);
            Console.WriteLine("Press Enter to play again!");
            Thread.Sleep(200);
            bool newGame = false;
            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Enter)
                        newGame = true;
                    Thread.Sleep(100);
                }
            }
            while (newGame==false);
            //Console.ReadKey();
            SetInitialData();
        }
    }

    static void CollisionCheck()
    {
        if ((rocksRepeat[Console.WindowHeight - 1] < 75) && (rocksPositinsX[Console.WindowHeight - 1] >= playerPosition
            && rocksPositinsX[Console.WindowHeight - 1] <= playerPosition + 2))
        {
            ReduceLife();
        }
        if ((rocksRepeat[Console.WindowHeight - 1] > 75 && rocksRepeat[Console.WindowHeight - 1] < 95)
            && (rocksPositinsX[Console.WindowHeight - 1] >= playerPosition - 1 && rocksPositinsX[Console.WindowHeight - 1] <= playerPosition + 2))
        {
            ReduceLife();
        }
        if ((rocksRepeat[Console.WindowHeight - 1] > 95) && (rocksPositinsX[Console.WindowHeight - 1] >= playerPosition - 2
            && rocksPositinsX[Console.WindowHeight - 1] <= playerPosition + 2))
        {
            ReduceLife();
        }
    }

    static void Main()
    {
        RemoveScrollBars();
        SetInitialData();

        while (true)
        {

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    MovePlayerLeft();
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    MovePlayerRight();
                }
            }
            MoveRows();
            CollisionCheck();
            score++;
            DrawFirstRow();
            DrawDwarf();
            PrintResult();
            Thread.Sleep(150);
            Console.Clear();
            //Console.In.ReadToEnd();
            //Console.Out.Flush();

        }
    }
}


using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int width = 30;
        int height = 20;
        int snakeX = width / 2;
        int snakeY = height / 2;
        int foodX = 0;
        int foodY = 0;
        int score = 0;
        Random rand = new Random();
        char direction = 'r';
        List<Tuple<int, int>> snake = new List<Tuple<int, int>>();
        snake.Add(new Tuple<int, int>(snakeX, snakeY));

        Console.CursorVisible = false;
        Console.WindowHeight = height + 2;
        Console.WindowWidth = width + 2;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;

        PlaceFood(rand, width, height, snake, out foodX, out foodY);

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = 'u';
                        break;
                    case ConsoleKey.DownArrow:
                        direction = 'd';
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = 'l';
                        break;
                    case ConsoleKey.RightArrow:
                        direction = 'r';
                        break;
                }
            }

            int newX = snake[0].Item1;
            int newY = snake[0].Item2;

            switch (direction)
            {
                case 'u':
                    newY--;
                    break;
                case 'd':
                    newY++;
                    break;
                case 'l':
                    newX--;
                    break;
                case 'r':
                    newX++;
                    break;
            }

            if (newX < 0 || newX >= width || newY < 0 || newY >= height || snake.Contains(new Tuple<int, int>(newX, newY)))
            {
                Console.Clear();
                Console.WriteLine("Perdeu! Pontuação: " + score);
                Console.ReadKey();
                return;
            }

            snake.Insert(0, new Tuple<int, int>(newX, newY));

            if (newX == foodX && newY == foodY)
            {
                score++;
                PlaceFood(rand, width, height, snake, out foodX, out foodY);
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            Console.Clear();
            DrawBoard(width, height, snake, foodX, foodY);
            Console.WriteLine("Pontuação: " + score);

            Thread.Sleep(100);
        }
    }

    static void DrawBoard(int width, int height, List<Tuple<int, int>> snake, int foodX, int foodY)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    Console.Write("#");
                }
                else if (x == foodX && y == foodY)
                {
                    Console.Write("@");
                }
                else if (snake.Contains(new Tuple<int, int>(x, y)))
                {
                    Console.Write("O");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    static void PlaceFood(Random rand, int width, int height, List<Tuple<int, int>> snake, out int foodX, out int foodY)
    {
        foodX = rand.Next(1, width - 1);
        foodY = rand.Next(1, height - 1);

        while (snake.Contains(new Tuple<int, int>(foodX, foodY)))
        {
            foodX = rand.Next(1, width - 1);
            foodY = rand.Next(1, height - 1);
        }
    }
}
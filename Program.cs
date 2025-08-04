using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            Random randomNum = new Random();
            int score = 0;
            int gameSpeed = 100;
            int foodCounter = 0;
            bool isGameOver = false;

            // Initialize the snake
            List<int> snakeXPosition = new List<int>() { 10, 9, 8 };
            List<int> snakeYPosition = new List<int>() { 10, 10, 10 };
            int snakeLength = snakeXPosition.Count;

            // Initialize the food
            int foodXPosition = randomNum.Next(0, screenWidth);
            int foodYPosition = randomNum.Next(0, screenHeight);

            // Set initial movement direction
            ConsoleKey direction = ConsoleKey.RightArrow;

            // Hide the cursor
            Console.CursorVisible = false;

            while (!isGameOver)
            {
                // Check if a key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    direction = key.Key;
                }

                // Move the snake
                MoveSnake(snakeXPosition, snakeYPosition, direction);

                // Check if the snake hits the wall or itself
                if (snakeXPosition[0] == 0 || snakeXPosition[0] == screenWidth - 1 ||
                    snakeYPosition[0] == 0 || snakeYPosition[0] == screenHeight - 1 ||
                    snakeXPosition.GetRange(1, snakeLength - 1).Contains(snakeXPosition[0]) &&
                    snakeYPosition.GetRange(1, snakeLength - 1).Contains(snakeYPosition[0]))
                {
                    isGameOver = true;
                }

                // Check if the snake eats the food
                if (snakeXPosition[0] == foodXPosition && snakeYPosition[0] == foodYPosition)
                {
                    // Increase the score
                    score += 10;

                    // Generate new food position
                    foodXPosition = randomNum.Next(0, screenWidth);
                    foodYPosition = randomNum.Next(0, screenHeight);

                    // Increase the snake length
                    snakeXPosition.Add(0);
                    snakeYPosition.Add(0);
                    snakeLength++;

                    // Increase the game speed
                    if (gameSpeed > 10)
                    {
                        gameSpeed -= 10;
                    }

                    // Increase the food counter
                    foodCounter++;
                }

                // Clear the console
                Console.Clear();

                // Draw the game border
                DrawBorder(screenWidth, screenHeight);

                // Draw the snake
                DrawSnake(snakeXPosition, snakeYPosition);

                // Draw the food
                DrawFood(foodXPosition, foodYPosition);

                // Display the score and game speed
                DisplayScore(score);
                DisplayGameSpeed(gameSpeed);

                // Slow down the game
                Thread.Sleep(gameSpeed);
            }

            // Show game over screen
            Console.Clear();
            Console.SetCursorPosition(screenWidth / 2 - 5, screenHeight / 2);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(screenWidth / 2 - 8, screenHeight / 2 + 1);
            Console.WriteLine("Your score: " + score);
            Console.SetCursorPosition(screenWidth / 2 - 12, screenHeight / 2 + 2);
            Console.WriteLine("Press any key to exit");

            // Wait for a key press to exit the game
            Console.ReadKey();
        }

        static void MoveSnake(List<int> snakeXPosition, List<int> snakeYPosition, ConsoleKey direction)
        {
            // Move the snake based on the direction
            for (int i = snakeXPosition.Count - 1; i > 0; i--)
            {
                snakeXPosition[i] = snakeXPosition[i - 1];
                snakeYPosition[i] = snakeYPosition[i - 1];
            }

            switch (direction)
            {
                case ConsoleKey.LeftArrow:
                    snakeXPosition[0]--;
                    break;
                case ConsoleKey.RightArrow:
                    snakeXPosition[0]++;
                    break;
                case ConsoleKey.UpArrow:
                    snakeYPosition[0]--;
                    break;
                case ConsoleKey.DownArrow:
                    snakeYPosition[0]++;
                    break;
            }
        }

        static void DrawBorder(int screenWidth, int screenHeight)
        {
            // Draw the top border
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
            }

            // Draw the bottom border
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("#");
            }

            // Draw the left border
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
            }

            // Draw the right border
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("#");
            }
        }

        static void DrawSnake(List<int> snakeXPosition, List<int> snakeYPosition)
        {
            // Draw the snake body
            for (int i = 0; i < snakeXPosition.Count; i++)
            {
                Console.SetCursorPosition(snakeXPosition[i], snakeYPosition[i]);
                if (i == 0)
                {
                    Console.Write("@");
                }
                else
                {
                    Console.Write("*");
                }
            }
        }

        static void DrawFood(int foodXPosition, int foodYPosition)
        {
            // Draw the food
            Console.SetCursorPosition(foodXPosition, foodYPosition);
            Console.Write("F");
        }

        static void DisplayScore(int score)
        {
            // Display the score
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write("Score: " + score);
        }

        static void DisplayGameSpeed(int gameSpeed)
        {
            // Display the game speed
            Console.SetCursorPosition(Console.WindowWidth - 15, Console.WindowHeight - 1);
            Console.Write("Speed: " + gameSpeed);
        }
    }
}
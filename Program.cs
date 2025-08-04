using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get initial console window dimensions
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            bool playAgain = true; // Control variable for game loop
            do
            {
                // Show instructions and wait for player to start
                ShowStartMessage(screenWidth, screenHeight);

                // Clear console to start the game fresh
                Console.Clear();

                // Set fixed window size for gameplay
                Console.WindowHeight = 24;
                Console.WindowWidth = 48;
                screenWidth = Console.WindowWidth;
                screenHeight = Console.WindowHeight;
                Random randomNum = new Random();

                // Initialize game state variables
                int score = 0; // Player's score
                int gameSpeed = 100; // Initial game speed (lower is faster)
                int foodCounter = 0; // Tracks number of foods eaten
                bool isGameOver = false; // Game over flag

                // Initialize the snakes starting position (horizontal line)
                List<int> snakeXPosition = new List<int>() { 10, 9, 8 };
                List<int> snakeYPosition = new List<int>() { 10, 10, 10 };
                int snakeLength = snakeXPosition.Count;

                // Generate first food position inside the screen bounds
                int foodXPosition = randomNum.Next(1, screenWidth - 1);
                int foodYPosition = randomNum.Next(1, screenHeight - 3);

                // Generate first food position inside the screen bounds
                ConsoleKey direction = ConsoleKey.RightArrow;

                // Hide cursor for cleaner game display
                Console.CursorVisible = false;

                // Main game loop
                while (!isGameOver)
                {
                    // Handle user input for direction control
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            // Prevent reversing direction from Right to Left
                            case ConsoleKey.LeftArrow:
                            case ConsoleKey.A:
                                if (direction != ConsoleKey.RightArrow)
                                    direction = ConsoleKey.LeftArrow;
                                break;
                            // Prevent reversing direction from Left to Right
                            case ConsoleKey.RightArrow:
                            case ConsoleKey.D:
                                if (direction != ConsoleKey.LeftArrow)
                                    direction = ConsoleKey.RightArrow;
                                break;
                            // Prevent reversing direction from Down to Up
                            case ConsoleKey.UpArrow:
                            case ConsoleKey.W:
                                if (direction != ConsoleKey.DownArrow)
                                    direction = ConsoleKey.UpArrow;
                                break;
                            // Prevent reversing direction from Up to Down
                            case ConsoleKey.DownArrow:
                            case ConsoleKey.S:
                                if (direction != ConsoleKey.UpArrow)
                                    direction = ConsoleKey.DownArrow;
                                break;
                        }
                    }

                    // Move snake segments forward
                    MoveSnake(snakeXPosition, snakeYPosition, direction);

                    // Check for collision with walls
                    bool hitWall = snakeXPosition[0] <= 0 || snakeXPosition[0] >= screenWidth - 1 ||
                                   snakeYPosition[0] <= 0 || snakeYPosition[0] >= screenHeight - 1;

                    // Check for collision with the snakes own body
                    bool hitSelf = false;
                    for (int i = 1; i < snakeXPosition.Count; i++)
                    {
                        if (snakeXPosition[0] == snakeXPosition[i] && snakeYPosition[0] == snakeYPosition[i])
                        {
                            hitSelf = true;
                            break;
                        }
                    }

                    // If collision detected, end the game
                    if (hitWall || hitSelf)
                    {
                        isGameOver = true;
                    }

                    // Check if snake has eaten the food
                    if (snakeXPosition[0] == foodXPosition && snakeYPosition[0] == foodYPosition)
                    {
                        // Increase the score
                        score += 10;

                        // Generate new food position ensuring it's not on the snake
                        bool isOnSnake;
                        do
                        {
                            foodXPosition = randomNum.Next(1, screenWidth - 1);
                            foodYPosition = randomNum.Next(1, screenHeight - 1);
                            isOnSnake = false;
                            // Check against snake parts
                            for (int i = 0; i < snakeXPosition.Count; i++)
                            {
                                if (snakeXPosition[i] == foodXPosition && snakeYPosition[i] == foodYPosition)
                                {
                                    isOnSnake = true; // Reposition if on snake
                                    break;
                                }
                            }
                        } while (isOnSnake);

                        // Add new segment to snake's tail at dummy position (will be moved in next tick)
                        snakeXPosition.Add(0);
                        snakeYPosition.Add(0);
                        snakeLength++;

                        // Increase game speed gradually, making the game more challenging
                        if (gameSpeed > 10)
                        {
                            gameSpeed -= 10; // Decrease sleep time, increasing speed
                        }

                        // Increment the count of foods eaten
                        foodCounter++;
                    }

                    // Clear console to prepare for new frame rendering
                    Console.Clear();

                    // Draw game boundary walls
                    DrawBorder(screenWidth, screenHeight);

                    // Draw the snake on the console
                    DrawSnake(snakeXPosition, snakeYPosition);

                    // Draw the food item inside the border
                    DrawFood(foodXPosition, foodYPosition);

                    // Display current score and game speed at the bottom
                    DisplayStatus(score, gameSpeed, screenWidth, screenHeight);

                    // Control game speed with thread sleep
                    Thread.Sleep(gameSpeed);
                }

                // When game over, clear the console to show final messages
                Console.Clear();

                // Draw border again for visual clarity on game over screen
                DrawBorder(screenWidth, screenHeight);

                // Prepare game over and final score messages
                string gameOverMsg = "      Game Over      ";
                string finalScoreMsg = $"Final Score: {score}";
                string restartMsg = "Press 'Space' to Exit or any key to Restart";

                // Helper method to center text horizontally
                int CalculateCenteredPosition(int width, int messageLength)
                {
                    int pos = (width - messageLength) / 2;
                    if (pos < 0) pos = 0; // Ensure position isn't negative
                    return pos;
                }

                // Calculate starting positions for the centered messages
                int gameOverLeft = CalculateCenteredPosition(screenWidth, gameOverMsg.Length);
                int finalScoreLeft = CalculateCenteredPosition(screenWidth, finalScoreMsg.Length);
                int restartLeft = CalculateCenteredPosition(screenWidth, restartMsg.Length);

                int gameOverTop = screenHeight / 2 - 2;
                int finalScoreTop = gameOverTop + 2;
                int restartTop = finalScoreTop + 2;

                // Display "Game Over" message centered
                Console.SetCursorPosition(gameOverLeft, gameOverTop);
                Console.WriteLine(gameOverMsg);

                // Display final score below "Game Over"
                Console.SetCursorPosition(finalScoreLeft, finalScoreTop);
                Console.WriteLine(finalScoreMsg);

                // Prompt user to restart or exit
                Console.SetCursorPosition(restartLeft, restartTop);
                Console.WriteLine(restartMsg);

                // Wait for user key press
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    // Exit the game loop and terminate
                    playAgain = false;
                }
                else
                {
                    // Restart the game
                    playAgain = true;
                }
            } while (playAgain);
        }

        // Method to display initial instructions and wait for user input to start game
        static void ShowStartMessage(int screenWidth, int screenHeight)
        {
            string[] lines = {
                "Welcome to the C# Snake Game!",
                "",
                "Please be aware that the game has flashing lights for the console menu, please close this game immediately ",
                "if you are sensitive to flashing lights, or may have any form of epilepsy.",
                "",
                "Use Arrow Keys or W/A/S/D to move the snake up, down, left, or right.",
                "Eat the 'F's that appear on the map to grow your snake and avoid colliding with walls or yourself.",
                "Your score increases by 10 for each food aten.",
                "The game gets faster and more challenging the more you eat.",
                "",
                "Press any key to begin the game..."
            };

            // Clear console for the start message
            Console.Clear();

            // Calculate vertical start position to center message block
            int startY = (screenHeight - lines.Length) / 2;
            if (startY < 0) startY = 0; // ensure non-negative

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Calculate starting X position for centering each line
                int startX = (screenWidth - line.Length) / 2;
                if (startX < 0) startX = 0;

                // Truncate if line exceeds buffer width
                if (startX + line.Length > Console.BufferWidth)
                {
                    line = line.Substring(0, Console.BufferWidth - startX);
                }

                // Set cursor position and write line if within bounds
                if (startY + i >= 0 && startY + i < Console.BufferHeight)
                {
                    Console.SetCursorPosition(startX, startY + i);
                    Console.WriteLine(line);
                }
            }

            // Wait for any key press to start the game
            Console.ReadKey(true);
        }

        // Moves the snake segments forward in the current direction
        static void MoveSnake(List<int> snakeXPosition, List<int> snakeYPosition, ConsoleKey direction)
        {
            // Move each segment to follow the previous one
            for (int i = snakeXPosition.Count - 1; i > 0; i--)
            {
                snakeXPosition[i] = snakeXPosition[i - 1];
                snakeYPosition[i] = snakeYPosition[i - 1];
            }

            // Update the head position based on the current direction
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

        // Draws the rectangular border around the game area
        static void DrawBorder(int screenWidth, int screenHeight)
        {
            int borderHeight = screenHeight - 2; // Reserve bottom row(s) for info or spacing

            // Draw top border
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
            }

            // Draw bottom border at designated height
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, borderHeight);
                Console.Write("#");
            }

            // Draw left border
            for (int i = 0; i < borderHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
            }

            // Draw right border
            for (int i = 0; i < borderHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("#");
            }
        }

        // Renders the snake on the console
        static void DrawSnake(List<int> snakeXPosition, List<int> snakeYPosition)
        {
            // Loop through each segment to draw it
            for (int i = 0; i < snakeXPosition.Count; i++)
            {
                Console.SetCursorPosition(snakeXPosition[i], snakeYPosition[i]);
                if (i == 0)
                {
                    // Draw snake head with '@'
                    Console.Write("@");
                }
                else
                {
                    // Draw snake body segment with '*'
                    Console.Write("*");
                }
            }
        }

        // Draws the food item at specified position
        static void DrawFood(int foodXPosition, int foodYPosition)
        {
            Console.SetCursorPosition(foodXPosition, foodYPosition);
            Console.Write("F");
        }

        // Displays current game status at the bottom of the window
        static void DisplayStatus(int score, int gameSpeed, int screenWidth, int screenHeight)
        {
            int linePosition = screenHeight - 1; // Use bottom line for status
            if (linePosition >= Console.BufferHeight)
                linePosition = Console.BufferHeight - 1; // Safety check

            // Prepare status texts
            string scoreText = $"Score: {score}";
            string speedText = $"Speed: {gameSpeed}";

            // Calculate amount of spacing to align texts
            int spaceBetween = screenWidth - (scoreText.Length + speedText.Length);

            // Prevent overlapping in small windows
            if (spaceBetween < 1)
            {
                spaceBetween = 1;
            }

            // Generate spacing string
            string spacing = new string(' ', spaceBetween);

            // Concatenate status line
            string lineContent = scoreText + spacing + speedText;

            // Set cursor and display
            Console.SetCursorPosition(0, linePosition);
            Console.Write(lineContent);
        }
    }
}

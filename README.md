# Snake Game in C#

# Overview:

This project is a command-line version of the classic Snake game, developed in C#. The game is designed to provide an interactive and fun experience where the player controls a snake's movement to consume food while avoiding collisions with the walls or itself. As the snake consumes food, it grows in length and the game speed increases, adding to the challenge and making it fun to replay. This simple yet engaging game was designed for casual game enjoyers, or anyone who would be interested in understanding and utilizing console-based applications, threading, and basic game mechanics.

# Features:
- Responsive controls allowing the user to use either Arrow Keys or W/A/S/D for snake movement
- Dynamic game area with collision detection for walls and the snake's body
- Score tracking and incremental difficulty adjustments that occur based on snake growth/how much points the player gets
- Randomized food placement, while ensuring it's not generated on the snake's body or outside/on the boarder wall of the game
- Start message explaining the goal of the game, and key notes/details and Game over screen displaying the final score and restart/exit options

## Technologies Used:
- C#
- .NET Framework
- Standard C# libraries for console applications and threading (System, System.Collections.Generic, System.Threading)

# Usage Instructions:

# File Pathway Tree/ File Directory:

\Snake-Game-C-Sharp\
      | --- Program.cs\
      | --- Snake-Game-C-Sharp.csproj\
      | --- Snake-Game-C-Sharp.sln

## Installation & Setup:
1. Clone this repository to your local machine. Ensure that C# 12 is installed inside of a runnable IDE (Such as Visual Studio Community 2022).
2. Ensure that you have a .NET environment set up and downloaded, IE (.NET version 8.0 or later).
3. After that there are no additional downloads/libraries required; just run the “Program.cs” file directly.

## Running the Game:
1. Navigate to the project directory in your terminal/IDE.
2. Run the project using your C# development environment by running the “Program.cs” file inside the IDE and the project directory.
3. Follow the in-game instructions displayed via the start message. (Summarized below):
-	Use the Arrow Keys or W/A/S/D to navigate the snake.
-	Avoid crashing into walls or the snake's body.
-	Consume the 'F' food icons to grow your snake, increase your score, and ramp up the speed of the snake.
4. Upon the game ending by the snake crashing into a wall or itself, choose whether to restart and continue playing or exit the game by following the on-screen prompts.

## How It Works:
-	The snake moves continuously, responding to directional input from the player.
-	Collision detection ensures the game ends if the snake hits any walls or its own body.
-	Randomly generated food appears within the game boundaries, which the snake must consume to grow and to increase the score.
-	The game speed increases slightly with each piece of food consumed, raising the difficulty.
-	At the end of each game session, the final score is displayed, enabling replayability with options to restart and continue playing or exit the game.

# Contributing to the Codebase:
Contributions are encouraged and welcome! You can fork the repository to implement enhancements, fix bugs, or add new features. After implementing your changes, submit a pull request for review. Your contributions are appreciated and help make the game better and more enjoyable for everyone!

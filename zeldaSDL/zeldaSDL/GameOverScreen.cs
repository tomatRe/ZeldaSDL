using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class GameOverScreen : Screen
{

    Image imageW;

    bool exit;

    public GameOverScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/gameOverScreen.png", 1024, 720);

        Console.WriteLine("Game Over Screen Created");
        exit = false;
    }

    public void Run()
    {
        while (exit != true)
        {
            hardware.ClearScreen();
            hardware.DrawImage(imageW);
            hardware.UpdateScreen();

            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }
    }

    public void DrawText()
    {
        //to do
    }
}


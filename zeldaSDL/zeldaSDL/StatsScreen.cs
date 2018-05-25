using System;
using System.Threading;

class StatsScreen : Screen
{
    Image imageW;

    bool exit;

    public StatsScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/statsScreen.png", 1024, 720);

        Console.WriteLine("Stats Screen Created");
        exit = false;
    }

    public void Run()
    {
        while (exit != true)
        {
            hardware.ClearScreen();
            hardware.DrawImage(imageW);
            hardware.UpdateScreen();
            Thread.Sleep(50);

            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }
    }
}
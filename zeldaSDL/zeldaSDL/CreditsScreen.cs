using System;
using System.Threading;

class CreditsScreen : Screen
{
    Image imageW;

    bool exit;

    public CreditsScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/creditsScreen.png", 1024, 720);

        Console.WriteLine("Credits Screen Created");
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


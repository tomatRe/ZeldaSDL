using System;
using System.Threading;

class HelpScreen : Screen
{

    Image imageW;

    bool exit;

    public HelpScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/helpScreen.png", 1024, 720);

        Console.WriteLine("Help Screen Created");
        exit = false;
    }

    public void Run()
    {
        while (exit != true)
        {
            hardware.ClearScreen();
            hardware.DrawImage(imageW);
            hardware.UpdateScreen();

            Thread.Sleep(25);
            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }
    }
}


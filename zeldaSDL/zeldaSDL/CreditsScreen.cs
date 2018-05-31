using System;
using System.Threading;

class CreditsScreen : Screen
{
    Image imageW;
    Image imageW_es;

    byte languaje;
    bool exit;

    public CreditsScreen(Hardware hardware, byte languaje) : base(hardware)
    {
        imageW = new Image("sprites/creditsScreen.png", 1024, 720);
        imageW_es = new Image("sprites/creditsScreen_es.png", 1024, 720);

        this.languaje = languaje;

        Console.WriteLine("Credits Screen Created");
        exit = false;
    }

    public void Run()
    {
        while (exit != true)
        {
            hardware.ClearScreen();

            if(languaje == 0)
                hardware.DrawImage(imageW);
            if(languaje == 1)
                hardware.DrawImage(imageW_es);

            hardware.UpdateScreen();
            Thread.Sleep(50);

            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }
    }
}


/*
 * 0.01, 17-05-2018 Almudena López: Method show and added new atribute image
 * 
 */

using System;
using System.Threading;


class WelcomeScreen : Screen
{
    Image imageW;

    bool exit;

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/welcomeScreen.png", 1024, 720);
        Thread.Sleep(5);
        Console.WriteLine("Welcome Screen Created");
        exit = false;
    }

    public void Run()
    {

        while (exit != true)
        {
            hardware.DrawImage(imageW);
            hardware.UpdateScreen();

            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }
        
    }

    public bool GetExit()
    {
        return exit;
    }
}


using System;
using System.Threading;


class WelcomeScreen : Screen
{

    bool exit;

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        Thread.Sleep(5);
        Console.WriteLine("Welcome Screen Created");
    }

    public bool GetExit()
    {
        return exit;
    }
}


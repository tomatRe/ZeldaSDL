/*
 * v0.01 Angel Rebollo, 18/05/2018, implemented the actual menu
 */

using System.Threading;
using Tao.Sdl;

class MenuScreen : Screen
{
    public int option { get; set; }
    private bool exit;
    private int newRun;
    private int stats;
    private int credits;
    private int quit;
    private short[] yPositionsArray = { 365, 420, 470, 550 };
    private short position = 0;
    Image imagen;
    Image cursor;

    public MenuScreen(Hardware hardware) : base(hardware)
    {
        imagen = new Image("sprites/MenuScreen.png", 1024, 720);
        cursor = new Image("sprites/SelectArrow.png",78,78);
        cursor.X = 200;
        cursor.Y = 365;
        exit = false;
        newRun = 0;
        stats = 0;
        credits = 0;
        quit = 0;
    }

    public void Run()
    {
        
        while (!exit)
        {
            hardware.ticks = hardware.ticks + 1;
            hardware.ClearScreen();
            WaitForNextFrame();
            hardware.DrawImage(imagen);
            hardware.DrawImage(cursor);
            hardware.UpdateScreen();

            CheckInput();
        }

        switch (option)
        {
            default:
                break;
        }
    }

    public void CheckInput()
    {
        System.Console.WriteLine("CheckInput() call");
        System.Console.WriteLine("Ticks: " + hardware.ticks);
        if (hardware.IsKeyPressed(Hardware.KEY_ESC))
            exit = true;
        
        if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
        {
            position++;

            if (position > 3)
                position = 0;
            if (position < 0)
                position = 3;

            cursor.Y = yPositionsArray[position];
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_UP))
        {
            position--;

            if (position > 3)
                position = 0;
            if (position < 0)
                position = 3;

            cursor.Y = yPositionsArray[position];
        }
    }

    public void WaitForNextFrame()
    {
        Thread.Sleep(25);
    }

}
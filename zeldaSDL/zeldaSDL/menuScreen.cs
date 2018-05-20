/*
 * v0.01 Angel Rebollo, 18/05/2018, implemented the actual menu
 */

using System.Threading;
using Tao.Sdl;

class MenuScreen : Screen
{
    public int option { get; set; }
    private bool exit;
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
    }

    public void Run()
    {

        WaitForNextFrame(25);

        while (!exit)
        {
            hardware.ClearScreen();
            WaitForNextFrame(40);
            hardware.DrawImage(imagen);
            hardware.DrawImage(cursor);
            hardware.UpdateScreen();

            CheckInput();
        }
    }

    public void CheckInput()
    {
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

        if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
        {

            option = position;

            switch (option)
            {
                case 0:
                    System.Console.WriteLine("New run Selected");
                    exit = true;
                    break;
                case 1:
                    System.Console.WriteLine("Stats selected");
                    exit = true;
                    break;
                case 2:
                    System.Console.WriteLine("Credits selected");
                    exit = true;
                    break;

                case 3:
                    System.Console.WriteLine("Exit Selected");
                    exit = true;
                    break;

                default:
                    System.Console.WriteLine("Error on menu option");
                    break;
            }
        }
    }

    public void WaitForNextFrame(int delay)
    {
        Thread.Sleep(delay);
    }

}
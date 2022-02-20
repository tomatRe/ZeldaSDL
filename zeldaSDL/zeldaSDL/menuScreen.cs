/*
 * v0.01 Angel Rebollo, 18/05/2018, implemented the actual menu
 */

using System.Threading;
using Tao.Sdl;

class MenuScreen : Screen
{
    public int option { get; set; }
    public bool exit;
    private short[] yPositionsArray = { 365, 420, 470, 550 };
    private short position = 0;

    byte languaje;
    Image imagen;
    Image image_es;
    Image cursor;

    public MenuScreen(Hardware hardware, byte languaje) : base(hardware)
    {
        imagen = new Image("sprites/MenuScreen.png", 1024, 720);
        image_es = new Image("sprites/MenuScreen_es.png", 1024, 720);
        cursor = new Image("sprites/SelectArrow.png",78,78);
        cursor.X = 200;
        cursor.Y = 365;
        this.languaje = languaje;
        exit = false;
    }

    public void Run()
    {

        WaitForNextFrame(100);

        //main loop
        while (!exit)
        {
            hardware.ClearScreen();
            WaitForNextFrame(60);

            if(languaje == 0)
                hardware.DrawImage(imagen);
            if (languaje == 1)
                hardware.DrawImage(image_es);

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
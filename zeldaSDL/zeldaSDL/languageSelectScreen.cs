
using System.Threading;

class LanguageSelectScreen : Screen
{
    public byte languaje { get; set; }

    Image imageW;
    Image cursor;

    private short[] yPositionsArray = { 230, 375 };
    private short position = 0;

    bool exit;

    public LanguageSelectScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/LanguajeSelectScreen.png", 1024, 720);
        cursor = new Image("sprites/SelectArrow_w.png", 78, 78);
        cursor.X = 640;
        cursor.Y = 230;
        exit = false;
    }

    public void Run()
    {

        WaitForNextFrame(25);

        //main loop
        while (!exit)
        {
            hardware.ClearScreen();
            WaitForNextFrame(40);
            hardware.DrawImage(imageW);
            hardware.DrawImage(cursor);
            hardware.UpdateScreen();

            CheckInput();
        }

        if (position == 0)
            System.Console.WriteLine("English Selected");
        else
            System.Console.WriteLine("Spanish Selected");

        languaje = (byte)position;
    }

    public void CheckInput()
    {
        if (hardware.IsKeyPressed(Hardware.KEY_ESC))
            exit = true;

        if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
        {
            position++;

            if (position > 1)
                position = 0;
            if (position < 0)
                position = 1;

            cursor.Y = yPositionsArray[position];
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_UP))
        {
            position--;

            if (position > 1)
                position = 0;
            if (position < 0)
                position = 1;

            cursor.Y = yPositionsArray[position];

        }

        if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
            exit = true;
    }

    public void WaitForNextFrame(int delay)
    {
        Thread.Sleep(delay);
    }
}


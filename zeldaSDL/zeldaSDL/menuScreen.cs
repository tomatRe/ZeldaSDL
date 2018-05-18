/*
 * v0.01 Angel Rebollo, 18/05/2018, implemented the actual menu
 */

class MenuScreen : Screen
{
    public int option { get; set; }
    private bool exit;
    private int newRun;
    private int stats;
    private int credits;
    private int quit;
    Image imagen;
    Image cursor;

    public MenuScreen(Hardware hardware) : base(hardware)
    {
        imagen = new Image("sprites/MenuScreen.png", 1024, 720);
        cursor = new Image("sprites/SelectArrow.png",626,626);
        cursor.X = 0;
        cursor.Y = 600;
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
            hardware.DrawImage(cursor);
            hardware.DrawImage(imagen);
            hardware.UpdateScreen();

            if (hardware.IsKeyPressed(Hardware.KEY_CTRL))
                exit = true;
        }

        switch (option)
        {
            default:
                break;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MenuScreen : Screen
{
    public int option { get; set; }
    private bool exit;
    private int newRun;
    private int stats;
    private int credits;
    private int quit;
    Image imagen;

    public MenuScreen(Hardware hardware) : base(hardware)
    {
        imagen = new Image("sprites/MenuScreen.png", 1024, 720);
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
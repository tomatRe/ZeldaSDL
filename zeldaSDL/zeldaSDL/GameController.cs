using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;


class GameController
{
    public void Start()
    {
        Hardware hardware = new Hardware(1024, 720, 24, false);
        WelcomeScreen welcome = new WelcomeScreen(hardware);
        GameScreen game = new GameScreen(hardware);
        CreditsScreen credits = new CreditsScreen(hardware);
        Player p = Player.GetPlayer();

        welcome.Show();
        game.Run();
        Thread.Sleep(500);

        /*do
        {
            hardware.ClearScreen();
            welcome.Show();
            if (!welcome.GetExit())
            {
                hardware.ClearScreen();
                game.Show();
            }
        } while (!welcome.GetExit());*/

        credits.Show();
    }
}


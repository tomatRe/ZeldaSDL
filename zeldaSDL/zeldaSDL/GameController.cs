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
        MenuScreen menu = new MenuScreen(hardware);
        GameScreen game = new GameScreen(hardware);
        StatsScreen stats = new StatsScreen(hardware);
        CreditsScreen credits = new CreditsScreen(hardware);
        Player p = Player.GetPlayer();

        welcome.Run();
        menu.Run();
        game.Run();

        credits.Show();
    }
}


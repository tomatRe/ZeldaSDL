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
        HelpScreen help = new HelpScreen(hardware);
        StatsScreen stats = new StatsScreen(hardware);
        CreditsScreen credits = new CreditsScreen(hardware);
        GameOverScreen end = new GameOverScreen(hardware);

        Player p = Player.GetPlayer();

        welcome.Run();
        menu.Run();

        switch (menu.option)
        {
            case 0:
                help.Run();
                game.Run();
                end.Run();
                break;
            case 1:
                stats.Run();
                break;
            case 2:
                credits.Run();
                break;

            default:
                break;
        }
    }
}


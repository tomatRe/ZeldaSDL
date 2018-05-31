using System;
using System.Threading;

class GameController
{
    public void Start()
    {
        Hardware hardware = new Hardware(1024, 720, 24, false);
        WelcomeScreen welcome = new WelcomeScreen(hardware);
        LanguageSelectScreen languaje = new LanguageSelectScreen(hardware);
        
        welcome.Run();
        languaje.Run();
        MenuScreen menu = new MenuScreen(hardware, languaje.languaje);

        do
        {
            GameScreen game = new GameScreen(hardware);
            HelpScreen help = new HelpScreen(hardware, languaje.languaje);
            StatsScreen stats = new StatsScreen(hardware, languaje.languaje);
            CreditsScreen credits = new CreditsScreen(hardware, languaje.languaje);

            Player p = Player.GetPlayer();
            DateTime begin;
            DateTime timeEnd;

            hardware.ClearScreen();
            menu.exit = false;
            menu.option = -1;
            Console.WriteLine("Menu");
            menu.Run();

            switch (menu.option)
            {
                case 0:
                    help.Run();
                    begin = DateTime.Now;
                    game.Run();
                    timeEnd = DateTime.Now;
                    TimeSpan time = timeEnd - begin;
                    GameOverScreen end = new GameOverScreen(hardware, time, languaje.languaje);
                    end.Run();
                    break;
                case 1:
                    stats.Run();
                    break;
                case 2:
                    credits.Run();
                    break;
            }
            Thread.Sleep(50);

        } while (menu.option != 3 ||
        hardware.IsKeyPressed(Hardware.KEY_ESC));
    }
}


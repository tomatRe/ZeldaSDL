using System;
using System.IO;
using Tao.Sdl;


class GameOverScreen : Screen
{

    Image imageW;
    TimeSpan time;
    Font font;
    IntPtr levelReached, timeSpent, score;
    bool exit;

    public GameOverScreen(Hardware hardware, TimeSpan time) : base(hardware)
    {
        font = new Font("fonts/lato.ttf", 20);
        imageW = new Image("sprites/gameOverScreen.png", 1024, 720);

        Console.WriteLine("Game Over Screen Created");
        this.time = time;
        exit = false;
    }

    public void Run()
    {
        Console.WriteLine(time.ToString());

        while (exit != true)
        {
            hardware.ClearScreen();
            hardware.DrawImage(imageW);
            DrawText();
            hardware.UpdateScreen();

            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }

        WriteStats("100",time.ToString(),"1");
    }

    public void DrawText()
    {
        Sdl.SDL_Color red = new Sdl.SDL_Color(255, 0, 0);
        timeSpent = SdlTtf.TTF_RenderText_Solid
            (font.GetFontType(), time.ToString(), red);
        levelReached = SdlTtf.TTF_RenderText_Solid
            (font.GetFontType(), "1", red);
        score = SdlTtf.TTF_RenderText_Solid
            (font.GetFontType(), "1000", red);

        hardware.WriteText
            (timeSpent, 340, 486);
        hardware.WriteText
            (levelReached, 340, 513);
        hardware.WriteText
            (score, 340, 586);
    }

    public void WriteStats(string score,
        string time, string levelReached)
    {
        if (!File.Exists("StatsFile.st"))
        {
            Console.WriteLine
                ("The Stats file does not exist or has errors," +
                " Creating a new one...");
            File.Create("StatsFile.st");
        }
        else
        {
            try
            {
                StreamWriter data = File.AppendText("StatsFile.st");
                string line = score + "-" + time + "-" + levelReached;
                data.WriteLine(line);
                data.Close();
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("PATH TOO LONG");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("INPUT/OUTPUT ERROR: " + ioEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}


using System;
using System.Threading;
using System.IO;
using Tao.Sdl;

class StatsScreen : Screen
{
    Image imageW;
    Image imageW_es;
    Font font;
    IntPtr score, time, levelReached;

    byte languaje;
    short line = 300;
    bool exit;

    public StatsScreen(Hardware hardware, byte languaje) : base(hardware)
    {
        imageW = new Image("sprites/statsScreen.png", 1024, 720);
        imageW_es = new Image("sprites/statsScreen_es.png", 1024, 720);
        font = new Font("fonts/lato.ttf", 20);

        this.languaje = languaje;

        Console.WriteLine("Stats Screen Created");
        exit = false;
    }

    public void Run()
    {

        hardware.ClearScreen();
        if(languaje == 0)
            hardware.DrawImage(imageW);
        if(languaje == 1)
            hardware.DrawImage(imageW_es);
        ReadStats();
        hardware.UpdateScreen();

        while (exit != true)
        {
            Thread.Sleep(50);
            if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
                exit = true;
        }
    }

    public void ReadStats()
    {
        if (!File.Exists("StatsFile.st"))
            EmptyStats();
        else
        {

            try
            {
                StreamReader data = File.OpenText("StatsFile.st");
                string line = "";

                do
                {
                    line = data.ReadLine();
                    string[] dataArray = line.Split('-');

                    DrawStats(dataArray);

                } while (line != null);                

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
        line = 300;
    }

    public void DrawStats(string[] dataArray)
    {
        Sdl.SDL_Color red = new Sdl.SDL_Color(0, 255, 0);

        score = SdlTtf.TTF_RenderText_Solid
            (font.GetFontType(), dataArray[0].ToString(), red);
        time = SdlTtf.TTF_RenderText_Solid
            (font.GetFontType(), dataArray[1].ToString(), red);
        levelReached = SdlTtf.TTF_RenderText_Solid
            (font.GetFontType(), dataArray[2].ToString(), red);

        hardware.WriteText
            (score, 150, line);
        hardware.WriteText
            (time, 360, line);
        hardware.WriteText
            (levelReached, 570, line);
        line += 20;
    }

    private void EmptyStats()
    {
        Console.WriteLine
                ("The Stats file does not exist or has errors," +
                " Creating a new one...");

        File.Create("StatsFile.st");
    }
}
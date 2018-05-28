using System;
using System.Threading;
using System.IO;

class StatsScreen : Screen
{
    Image imageW;

    bool exit;

    public StatsScreen(Hardware hardware) : base(hardware)
    {
        imageW = new Image("sprites/statsScreen.png", 1024, 720);

        Console.WriteLine("Stats Screen Created");
        exit = false;
    }

    public void Run()
    {
        while (exit != true)
        {
            hardware.ClearScreen();
            hardware.DrawImage(imageW);
            hardware.UpdateScreen();
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
    }

    public void WriteStats(string username, string score,
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

                string line = username + "-" + score +
                    "-" + time + "-" + levelReached;

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

    public void DrawStats(string[] dataArray)
    {
        //To do
    }

    private void EmptyStats()
    {
        //to do
    }
}
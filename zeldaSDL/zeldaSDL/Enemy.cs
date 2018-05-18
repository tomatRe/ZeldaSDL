using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Enemy : Sprite
{
    Random rnd = new Random();
    const int FRAMES = 10;

    public Enemy()
    {
        x = 50;
        y = 60;
        xSpeed = 1;
        ySpeed = 1;
        direction = UP;

        Console.WriteLine("Enemy spawned");
    }

    public void Move()
    {
        if (rnd.Next(1, 10) % 2 == 0)
        {
            for (int i = 0; i < FRAMES; i++)
            {
                //is inside the window boundaries?

                if (x <= 1024 && y <= 720 && x >= 0 && y >= 0)
                {
                    if (i % 2 == 0)
                        x += xSpeed;
                    else if (i % 3 == 0)
                        x -= xSpeed;
                    else
                        x += xSpeed;
                }
                else
                {
                    if (x < 0)
                        x = 0;
                    else if (y < 0)
                        y = 0;
                    else if (x < 1024)
                        x = 1024;
                    else if (y < 720)
                        y = 720;
                }

            }
        }
        else
        {

        }

    }
}


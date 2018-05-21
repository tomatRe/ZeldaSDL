using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Enemy : Sprite
{
    Random rnd = new Random();
    const int FRAMES = 10;
    const short DROPRATE = 1;
    public int hearts = 3;
    public bool isAttacking;

    public Enemy(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
        xSpeed = 1;
        ySpeed = 1;
        SpriteX = 0;
        SpriteY = 0;
        direction = UP;

        Console.WriteLine("Enemy spawned");
    }

    public void Move()
    {
        if (hearts >= 0)//is alive
        {
            if (rnd.Next(1, 100)  <= 5)//can move?
            {
                Console.WriteLine("Enemy at: {0} {1}", X, Y);

                for (int i = 0; i < FRAMES; i++)
                {
                    //is inside the window boundaries?

                    if (X <= 1024 && Y <= 720 && X >= 0 && Y >= 0)
                    {
                        if (i % 2 == 0)
                            X += xSpeed;
                        else if (i % 3 == 0)
                            X -= xSpeed;
                        else
                            X += xSpeed;
                    }
                    else
                    {
                        if (X < 0 || X >1024)
                            xSpeed *= -1;
                        else if (Y < 0 || Y > 720)
                            ySpeed *= -1;
                    }

                }
            }
        }

        else
        {
            Console.WriteLine("Enemy hearts: ", hearts);
            Die();
        }
    }

    public void Die()
    {
        //play animation (To do)

        //Despawn (To do)

        //Drop

        if (rnd.Next(0, 25) <= DROPRATE)
        {
            Console.WriteLine
                ("Dropped heart at: {0} {1}",X,Y);

            Heart h = new Heart(X,Y);

        }
    }
}


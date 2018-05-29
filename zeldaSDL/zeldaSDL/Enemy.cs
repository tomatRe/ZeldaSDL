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
    public int hearts = 2;
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
                //Console.WriteLine("Enemy at: {0} {1}", X, Y);

                for (int i = 0; i < FRAMES; i++)
                {
                    //is inside the window boundaries?

                    if (X <= 1024 - SPRITE_WIDTH &&
                        Y <= 720 - SPRITE_WIDTH && X >= 0 && Y >= 0)
                    {
                        //Random movements
                        if (i % 2 == 0)
                            X += xSpeed;
                        else if (i % 3 == 0)
                            X -= xSpeed;
                        else if (i % 5 == 0)
                            xSpeed *= -1;
                        else if (i == 10)
                            if (rnd.Next(1, 2) == 2)
                                xSpeed *= -1;
                            else
                                ySpeed *= -1;
                        else
                            Y += ySpeed;
                    }
                    else
                    {
                        if (X < 10 || X > 1020)
                        {
                            xSpeed *= -1;
                            X += xSpeed;
                        }
                            
                        else if (Y < 10 || Y > 7)
                        {
                            ySpeed *= -1;
                            Y += ySpeed;
                        }
                            
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

        SpriteX = 100;

        //Drop

        if (rnd.Next(0, 25) <= DROPRATE)
        {
            Console.WriteLine
                ("Dropped heart at: {0} {1}",X,Y);

            Heart h = new Heart(X, Y);
        }
    }
}


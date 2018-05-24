using System;
using System.Threading;

class Bomb : Sprite
{
    const short explosionRadius = 20;
    const short damage = 2;
    DateTime begin = DateTime.Now;
    int cooldown = 45;//in seconds
    bool canDropBomb = true;

    public Bomb(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
    }

    //Countdown
    private void Update()
    {
        //to do
    }

    public void Explosion(Player p, Enemy e)
    {
        //explosion delay
        //To do
        
        if (begin.Second - cooldown <= 0)
        {
            canDropBomb = true;
            cooldown = 45;
        }

        //Collision with enemies or player

        if (canDropBomb)
        {
            if ((p.X >= X - explosionRadius && p.X <= X + explosionRadius) &&
                (p.Y >= Y - explosionRadius && p.Y <= Y + explosionRadius))
            {
                    p.hearts -= damage; //Extra damage for special move
            }

            if ((e.X >= X - explosionRadius && e.X <= X + explosionRadius) &&
                (e.Y >= Y - explosionRadius && e.Y <= Y + explosionRadius))
            {
                e.hearts -= damage;
            }

            System.Console.WriteLine("EXPLOSION AT : {0} {1}", X, Y);
        }
        
        
    }
}


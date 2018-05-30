using System;
using System.Threading;

class Bomb : Sprite
{

    const short explosionRadius = 20;
    const short damage = 2;
    DateTime begin;
    DateTime end;
    int cooldown = 45;//in seconds
    bool canDropBomb = true;

    public Bomb(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
        begin = DateTime.Now;
    }

    //This really does nothing, but here it is...
    struct Bomba
    {
        private bool exploded;
        private bool onRange;
    }

    //Countdown
    public void Update(Player p, Enemy e)
    {
        if (p.CanFireSpecial())
        {
            canDropBomb = true;
            cooldown = 45;
            begin = DateTime.Now;
        }
        else
            canDropBomb = false;

        if (canDropBomb)
        {
            end = new DateTime(0,0,0,0,0,5);
            if (begin.Second - end.Second <= 0)
                Explosion(p, e);
        }

        Console.WriteLine(begin - end);
    }

    private void Explosion(Player p, Enemy e)
    {

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

            Console.WriteLine("EXPLOSION AT : {0} {1}", X, Y);
        }
        
        
    }
}


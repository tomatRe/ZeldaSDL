
class Door : Sprite
{
    public bool locked { get; set; }
    short oldX, oldY;

    public Door(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
        locked = true;
    }

    public void collision(Player p)
    {
        oldX = p.X;
        oldY = p.Y;
        if (locked)
        {
            //colide
            if ((p.X >= X - width && p.X <= X + width) &&
                (p.Y >= Y - height && p.Y <= Y + height))
            {
                p.X = oldX;
                p.Y = oldY;
            }
            
        }
        else
        {
            if (p.hasKey)
            {
                locked = false;
                //Set sprite to unlocked door (to do)

            }
        }
    }
}


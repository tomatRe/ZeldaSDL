
class Door : Sprite
{
    public bool locked { get; set; }

    public Door(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
        locked = false;
    }

    public void collision()
    {
        if (locked)
        {
            //to do
        }
    }
}


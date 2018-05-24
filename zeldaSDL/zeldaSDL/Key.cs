
class Key : Sprite
{
    public Key(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public bool PickUp(short playerX, short playerY)
    {
        //is the player touching the key?
        if (playerX <= X + 17 || playerX >= X - 17 &&
            playerY <= Y + 17 || playerY >= Y - 17)
            return true;
        else
            return false;
    }
}


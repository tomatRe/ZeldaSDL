//Angel Rebollo Berná, 21/05/2018, class created


class Heart : Sprite
{
    public Heart(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public bool PickUp(short playerX, short playerY)
    {
        //is the player touching the heart?
        if (playerX <= X + 17 || playerX >= X - 17 &&
            playerY <= Y + 17 || playerY >= Y - 17)
            return true;
        else
            return false;
    }
}


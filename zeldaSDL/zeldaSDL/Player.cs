
class Player : Sprite
{
    private const int x_offset = 48;
    private const int sprite_height = 64;

    public int hearts = 3;
    public bool isAttacking;
    public int cooldown = 20;

    private static Player player_instance;

    public static Player GetPlayer()
    {
        if (player_instance == null)
        {
            player_instance = new Player();
        }
        return player_instance;
    }

    public void Update()
    {
        NextFrame();
    }

    public Player()
    {
        X = 500;
        Y = 90;
        xSpeed = 1;
        ySpeed = 1;
        direction = UP;
        isAttacking = false;
        visible = true;
        containsSequence = true;

    }

    public void MoveUp()
    {
        if (!isAttacking)
        {
            MoveTo(X, (short)(Y - ySpeed));
            direction = UP;
            SpriteY = 0;
            SpriteX += x_offset;
            if (SpriteX >= x_offset * 12)
                SpriteX = 0;
            NextFrame();
        }
        
    }

    public void Movedown()
    {
        if (!isAttacking)
        {
            MoveTo(X, (short)(Y + ySpeed));
            direction = DOWN;

            SpriteY = sprite_height * 2;
            SpriteX += x_offset;
            if (SpriteX >= x_offset * 12)
                SpriteX = 0;

            NextFrame();
        }
        
    }

    public void MoveLeft()
    {
        if (!isAttacking)
        {
            MoveTo((short)(X - xSpeed), Y);
            direction = LEFT;

            SpriteY = sprite_height * 3;
            SpriteX += x_offset;
            if (SpriteX >= x_offset * 12)
                SpriteX = 0;

            NextFrame();
        }
        
    }

    public void MoveRight()
    {
        MoveTo((short)(X + xSpeed), Y);
        direction = RIGHT;

        SpriteY = sprite_height;
        SpriteX += x_offset;
        if (SpriteX >= x_offset * 12)
            SpriteX = 0;

        NextFrame();
    }

    public void FireMain()
    {
        if (cooldown <= 0)
        {
            isAttacking = true;
            //Temporally (sprites are incomming)
            SpriteY = sprite_height;
            SpriteX += x_offset;
            if (SpriteX >= x_offset * 12)
                SpriteX = 0;
            isAttacking = false;

            cooldown = 20;
        }
        
    }

    public void FireSpecial()
    {

    }

    public void Roll()
    {

    }

    private void ChangeSprite()
    {
        
    }
}
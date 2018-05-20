
class Player : Sprite
{
    private const int x_offset = 50;
    private const int sprite_height = 60;

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

        SpriteX = 0;
        SpriteY = 300;

        isAttacking = false;
        visible = true;
        containsSequence = true;

    }

    public void MoveUp()
    {
        if (!isAttacking)
        {
            MoveTo(X, (short)(Y - ySpeed));

            //--animation--
            direction = UP;
            SpriteY = 300;
            SpriteX += x_offset;
            if (SpriteX >= 600)
                SpriteX = 0;
            NextFrame();
        }
        
    }

    public void Movedown()
    {
        if (!isAttacking)
        {
            MoveTo(X, (short)(Y + ySpeed));

            //--animation--
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

            //--animation--
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

        //--animation--
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
            //Temporally (real sprites are incomming)
            SpriteY = sprite_height;
            SpriteX += x_offset;
            if (SpriteX >= x_offset * 12)
                SpriteX = 0;
            isAttacking = false;

            //Cooldown system so the player dont spam the attack button
            cooldown = 20;
        }
        
    }

    public void FireSpecial()
    {
        //To do
    }

    public void Roll()
    {
        //To do
    }

    private void ChangeSprite()
    {
    }
}
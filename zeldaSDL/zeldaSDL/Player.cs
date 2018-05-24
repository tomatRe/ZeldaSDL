
class Player : Sprite
{
    private const int x_offset = 55;
    private const int sprite_height = 60;
    private const int frameSpeed = 3;
    private int frames = 0;

    public int hearts = 3;
    public bool isAttacking;
    public int cooldown = 20;
    public int bombCooldown = 400;

    public bool hasKey = false;

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

            if (frames % frameSpeed == 0)//Slowing animation
            {
                SpriteX += x_offset;
                if (SpriteX >= 600)
                    SpriteX = 0;
                NextFrame();
                frames = 0;
            }
                
            frames++;
        }
        
    }

    public void Movedown()
    {
        if (!isAttacking)
        {
            MoveTo(X, (short)(Y + ySpeed));

            //--animation--
            direction = DOWN;
            SpriteY = 445;

            if (frames% frameSpeed == 0)//Slowing animation
            {
                SpriteX += x_offset;
                if (SpriteX >= 600)
                    SpriteX = 0;

                NextFrame();
                frames = 0;
            }
                
            frames++;
        }
        
    }

    public void MoveLeft()
    {
        if (!isAttacking)
        {
            MoveTo((short)(X - xSpeed), Y);

            //--animation--
            direction = LEFT;
            SpriteY = 520;

            if (frames % frameSpeed == 0)//Slowing animation
            {
                SpriteX += x_offset;
                if (SpriteX >= 600)
                    SpriteX = 0;

                NextFrame();
                frames = 0;
            }
                
            frames++;
        }
        
    }

    public void MoveRight()
    {
        MoveTo((short)(X + xSpeed), Y);

        //--animation--
        direction = RIGHT;
        SpriteY = 375;

        if (frames % frameSpeed == 0)//Slowing animation
        {
            SpriteX += x_offset;
            if (SpriteX >= 600)
                SpriteX = 0;

            NextFrame();
            frames = 0;
        }

        frames++;
    }

    public void FireMain()
    {
        if (cooldown <= 0)
        {
            isAttacking = true;

            SpriteX = 10;

            switch (direction)
            {
                case UP:
                    SpriteY = 147;
                    break;
                case DOWN:
                    SpriteY = 0;
                    break;
                case LEFT:
                    SpriteY = 65;
                    break;
                case RIGHT:
                    SpriteY = 200;
                    break;
            }

            SpriteX = 0;

            while (SpriteX <= 600)
            {
                frames++;
                if (frames % frameSpeed == 0)
                    SpriteX += x_offset;
            }
            
            if (SpriteX >= 600)
                SpriteX = 0;

            isAttacking = false;

            //Cooldown system so the player dont spam the attack button
            //The cooldown is reduced on the main loop of the gameScreen
            cooldown = 20;
        }

    }

    public bool CanFireSpecial()
    {
        if (bombCooldown <= 0)
        {
            bombCooldown = 400;
            return true;
        }
            
        else return false;
    }

    public void Roll()
    {
        //To do
    }

    private void ChangeSprite()
    {
    }
}
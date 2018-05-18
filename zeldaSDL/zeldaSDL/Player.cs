using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Player : Sprite
{
    private const int x_offset = 48;
    private const int sprite_height = 64;
    int[] framesX_up = { };
    private enum PLAYER_DIR{
        FACING_UP,
        FACING_DOWN,
        FACING_LEFT,
        FACING_RIGHT
    }

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
        visible = true;
        containsSequence = true;

    }

    public void MoveUp()
    {
        MoveTo(X, (short)(Y - ySpeed));
        direction = UP;
        SpriteY = 0;
        SpriteX += x_offset;
        if (SpriteX >= x_offset * 12)
            SpriteX = 0;
        NextFrame();
    }

    public void Movedown()
    {
        MoveTo(X, (short)(Y + ySpeed));
        direction = DOWN;

        SpriteY = sprite_height * 2;
        SpriteX += x_offset;
        if (SpriteX >= x_offset * 12)
            SpriteX = 0;

        NextFrame();
    }

    public void MoveLeft()
    {
        MoveTo((short)(X - xSpeed), Y);
        direction = LEFT;

        SpriteY = sprite_height * 3;
        SpriteX += x_offset;
        if (SpriteX >= x_offset * 12)
            SpriteX = 0;

        NextFrame();
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
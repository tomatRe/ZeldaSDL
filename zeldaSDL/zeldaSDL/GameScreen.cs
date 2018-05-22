using System;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    Level level;
    Player character;
    Enemy enemy;

    public GameScreen(Hardware hardware) : base(hardware)
    {
    }

    public void Run()
    {
        int keyPressed;

        level = new Level("level1");
        character = Player.GetPlayer();
        enemy = new Enemy(50,600);

        do
        {
            

            // 1. Draw everything

            hardware.ClearScreen();
            DrawLevel();
            DrawPlayerHUD();
            hardware.UpdateScreen();

            // 2. Move character from keyboard input

            keyPressed = hardware.KeyPressed();
            CheckInput();

            // 3. Move enemies and objects

            enemy.Move();
            MoveElements();

            // 4. Check collisions and update game state

            CheckCollisions();
            pollEvents();

            // 5. Pause game
            Thread.Sleep(15);


        } while (keyPressed != Hardware.KEY_ESC);
    }

    private void CheckInput()
    {
        if (hardware.IsKeyPressed(Hardware.KEY_LEFT))
        {
            character.MoveLeft();

            if (hardware.IsKeyPressed(Hardware.KEY_UP))
            {
                character.MoveUp();
            }
            else if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
            {
                character.Movedown();
            }

        }
        else if (hardware.IsKeyPressed(Hardware.KEY_RIGHT))
        {
            character.MoveRight();

            if (hardware.IsKeyPressed(Hardware.KEY_UP))
            {
                character.MoveUp();
            }
            else if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
            {
                character.Movedown();
            }
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_UP))
        {
            character.MoveUp();
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
        {
            character.Movedown();
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
        {
            character.FireMain();
        }
    }

    public void MoveElements()
    {
        //To Do
    }

    public void CheckCollisions()
    {
        //To Do enviromental collisions


        //Heart pickup

        /*if (Heart.PickUp(character.X, character.Y))
            character.hearts++;*/

        //Damage collisions

        if (enemy.X < character.Y + 5 && enemy.Y < character.X + 5)
        {
            if (character.isAttacking)
            {
                Console.WriteLine("Hit!");
                enemy.hearts--;
            }
        }
        if (character.cooldown >= 0)
        {
            character.cooldown--;
            Console.WriteLine(character.cooldown);
        }
        
    }

    public void DrawPlayerHUD()
    {
        const short startX = 1000;
        short x = startX;

        //you can have as many hearts as you can grab
        //so the hud draws all that the players have
        for (int i = 0; i < character.hearts; i++)
        {
            hardware.DrawSprite(Sprite.heart, x, 25, 0, 0, 16, 16);
            x -= 20;
        }
        x = startX;
    }

    public void DrawLevel()
    {
        /*hardware.DrawSprite(level.Floor,
          0, 0, level.XMap, level.YMap, GameController.SCREEN_WIDTH,
          GameController.SCREEN_HEIGHT);

        foreach (Wall wall in level.Walls)
            hardware.DrawSprite(Sprite.SpriteSheet,
            (short)(wall.X - level.XMap),
            (short)(wall.Y - level.YMap),
            wall.SpriteX, wall.SpriteY,
            Sprite.SPRITE_WIDTH,
            Sprite.SPRITE_HEIGHT);

        foreach (Door d in level.Doors)
            hardware.DrawSprite(Sprite.SpriteSheet,
            (short)(d.X - level.XMap),
            (short)(d.Y - level.YMap),
            d.SpriteX, d.SpriteY,
            Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);

        hardware.DrawSprite(Sprite.SpriteSheet,
            (short)(level.Key.X - level.XMap),
            (short)(level.Key.Y - level.YMap),
            level.Key.SpriteX, level.Key.SpriteY,
            Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
        */

        hardware.DrawSprite(Sprite.ockorok,
            (short)(enemy.X- level.XMap),
            (short)(enemy.Y - level.YMap),
            enemy.SpriteX, enemy.SpriteY, 68, 63);

        hardware.DrawSprite(Sprite.link,
            (short)(character.X - level.XMap),
            (short)(character.Y - level.YMap),
            character.SpriteX, character.SpriteY, 55, 60);
    }

    public static void PauseTillNextFrame(int sleeptime)
    {
        Thread.Sleep(sleeptime);
    }

    private void pollEvents()
    {
        Sdl.SDL_PumpEvents();
    }
}


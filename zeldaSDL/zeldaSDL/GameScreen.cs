using System;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    short oldX;
    short oldY;
    short oldXMap;
    short oldYMap;

    int numLevel = 1;
    bool exit = false;
    Level level;
    Player character;
    Enemy enemy;
    Key k;
    Bomb b;

    public GameScreen(Hardware hardware) : base(hardware)
    {
        Console.WriteLine("Game screen created");
    }

    public void Run()
    {
        int keyPressed;

        level = new Level("level"+numLevel+".map");
        character = Player.GetPlayer();
        k = new Key(60, 60);
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
            oldX = character.X;
            oldY = character.Y;
            oldXMap = level.XMap;
            oldYMap = level.YMap;
            CheckInput();

            // 3. Move enemies and objects

            enemy.Move();
            MoveElements();

            // 4. Check collisions and update game state

            CheckCollisions();
            pollEvents();

            // 5. Check lives

            CheckLives();

            // 6. Pause game
            Thread.Sleep(15);


        } while (!exit);
    }

    private void CheckInput()
    {
        if (hardware.IsKeyPressed(Hardware.KEY_LEFT))
        {
            if (character.X == 1024 / 2 && level.XMap > 0)
                level.XMap--;
            else if (character.X > 0)
                character.MoveLeft();

            if (hardware.IsKeyPressed(Hardware.KEY_UP))
            {
                character.MoveUp();
                if (character.Y == 720 / 2 && level.YMap > 0)
                    level.YMap--;
                /*else if (character.Y > 0)
                    character.MoveUp();*/
            }
            else if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
            {
                if (character.Y == 720 / 2 &&
                    level.YMap < level.Height - 720)
                    level.YMap++;
                //else if (character.Y < 720 - Sprite.SPRITE_HEIGHT)
                character.Movedown();
            }

        }
        else if (hardware.IsKeyPressed(Hardware.KEY_RIGHT))
        {
            if (character.X == 1024 / 2 &&
                    level.XMap < level.Width - 1024)
                level.XMap++;
            else if (character.X < 1024 - Sprite.SPRITE_WIDTH)
                character.MoveRight();

            if (hardware.IsKeyPressed(Hardware.KEY_UP))
            {
                if (character.Y == 720 / 2 && level.YMap > 0)
                    level.YMap--;
                else if (character.Y > 0)
                    character.MoveUp();
            }
            else if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
            {
                if (character.Y == 720 / 2 &&
                    level.YMap < level.Height - 720)
                    level.YMap++;
                else if (character.Y < 720 - Sprite.SPRITE_HEIGHT)
                    character.Movedown();
            }
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_UP))
        {
            
            if (character.Y == 720 / 2 && level.YMap > 0)
                level.YMap--;
            else if (character.Y > 0)
                character.MoveUp();
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_DOWN))
        {
            if (character.Y == 720 / 2 &&
                    level.YMap < level.Height - 720)
                level.YMap++;
            else if (character.Y < 720 - Sprite.SPRITE_HEIGHT)
                character.Movedown();
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_SPACE))
        {
            character.FireMain();
        }
        else if (hardware.IsKeyPressed(Hardware.KEY_CTRL))
        {
            if (character.CanFireSpecial())
            {
                b = new Bomb(character.X, character.Y);
            }

        }
        else if (hardware.IsKeyPressed(Hardware.KEY_ESC))
            exit = true;


        
    }

    public void MoveElements()
    {
        //To Do
    }

    public void CheckCollisions()
    {
        //To Do enviromental collisions

        if (character.CollidesWith(level.Walls))
        {
            character.X = oldX;
            character.Y = oldY;
            level.XMap = oldXMap;
            level.YMap = oldYMap;
        }

        if (character.CollidesWith(level.SideWall))
        {
            character.X = oldX;
            character.Y = oldY;
            level.XMap = oldXMap;
            level.YMap = oldYMap;
        }

        /*if (character.CollidesWith(level.floorTiles))
        {
            character.X = oldX;
            character.Y = oldY;
            level.XMap = oldXMap;
            level.YMap = oldYMap;
        }*/

        //Heart pickup

        /*if (Heart.PickUp(character.X, character.Y))
            character.hearts++;*/

        //key pickup

        if ((character.X >= k.X - 30 && character.X <= k.X + 30) &&
                (character.Y >= k.Y - 30 && character.Y <= k.Y + 30))
            character.hasKey = true;

        //Damage collisions

        if ((character.X >= enemy.X - 30 && character.X <= enemy.X + 30) &&
                (character.Y >= enemy.Y - 30 && character.Y <= enemy.Y + 30))
            if (character.isAttacking)
                switch (character.direction)
                {
                    case 0://up
                        if (enemy.Y >= character.Y && enemy.Y <= character.Y + 20)
                        {
                            Console.WriteLine("Hit!");
                            enemy.hearts--;
                        }
                        break;
                    case 1://down
                        if (enemy.Y <= character.Y && enemy.Y <= character.Y - 20)
                        {
                            Console.WriteLine("Hit!");
                            enemy.hearts--;
                        }
                        break;
                    case 2://left
                        if (enemy.X <= character.X && enemy.X >= character.X - 20)
                        {
                            Console.WriteLine("Hit!");
                            enemy.hearts--;
                        }
                        break;
                    case 3://right
                        if (enemy.X >= character.X && enemy.X <= character.X + 20)
                        {
                            Console.WriteLine("Hit!");
                            enemy.hearts--;
                        }
                        break;

                }
        if (character.cooldown >= 0)
            character.cooldown--;

        if (character.bombCooldown >= 0)
        {
            character.bombCooldown--;
            if (character.bombCooldown%100 == 0)
                Console.WriteLine(character.bombCooldown);
        }

        if (b != null)
            b.Update(character, enemy);

    }

    public void CheckLives()
    {
        if (character.hearts <= 0)
        {
            exit = true;
        }

        if (enemy.hearts < 0)
        {
            //enemy = null;
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
        hardware.DrawSprite(Sprite.grass,
        (short)(level.XMap * -1), (short)(level.YMap * -1),
        0, 0, 1024, 720);

        if (!character.hasKey)
            hardware.DrawSprite(Sprite.key,
                (short)(k.X - level.XMap),
                (short)(k.Y - level.YMap),
                0, 0, 25, 25);

        foreach (Wall wall in level.Walls)
            hardware.DrawSprite(Sprite.wall,
            (short)(wall.X - level.XMap),
            (short)(wall.Y - level.YMap),
            0, 0, 46, 43);

        hardware.DrawSprite(Sprite.link,
            (short)(character.X - level.XMap),
            (short)(character.Y - level.YMap),
            character.SpriteX, character.SpriteY, 55, 60);

        foreach (Door d in level.Doors)
            hardware.DrawSprite(Sprite.objects,
            (short)(d.X - level.XMap),
            (short)(d.Y - level.YMap),
            d.SpriteX, d.SpriteY,
            Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);

        foreach (Floor roof in level.Roof)
            hardware.DrawSprite(Sprite.floor,
            (short)(roof.X - level.XMap),
            (short)(roof.Y - level.YMap),
            0, 0, 42, 43);

        foreach (SideWall wall in level.SideWall)
            hardware.DrawSprite(Sprite.sideWall,
            (short)(wall.X - level.XMap),
            (short)(wall.Y - level.YMap),
            0, 0, 46, 43);

        if (enemy.hearts > 0)
            hardware.DrawSprite(Sprite.ockorok,
            (short)(enemy.X - level.XMap),
            (short)(enemy.Y - level.YMap),
            enemy.SpriteX, enemy.SpriteY, 68, 63);
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


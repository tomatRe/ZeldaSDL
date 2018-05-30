using System;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    bool exit = false;
    Map map;
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

        map = new Map();
        character = Player.GetPlayer();

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
        try
        {
            foreach (Floor floor in map.floorTiles)
                hardware.DrawSprite(Sprite.objects,
                (short)(floor.X - map.XMap),
                (short)(floor.Y - map.YMap),
                223, 463,
                48, 46);

            foreach (Wall wall in map.Walls)
                hardware.DrawSprite(Sprite.objects,
                (short)(wall.X - map.XMap),
                (short)(wall.Y - map.YMap),
                351, 50,
                48, 46);

            foreach (Door d in map.Doors)
                hardware.DrawSprite(Sprite.objects,
                (short)(d.X - map.XMap),
                (short)(d.Y - map.YMap),
                d.SpriteX, d.SpriteY,
                Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);

            if (enemy.hearts > 0)
                hardware.DrawSprite(Sprite.ockorok,
                (short)(enemy.X - map.XMap),
                (short)(enemy.Y - map.YMap),
                enemy.SpriteX, enemy.SpriteY, 68, 63);

            if (!character.hasKey)
                hardware.DrawSprite(Sprite.key,
                    (short)(k.X - map.XMap),
                    (short)(k.Y - map.YMap),
                    0, 0, 25, 25);

            hardware.DrawSprite(Sprite.link,
                (short)(character.X - map.XMap),
                (short)(character.Y - map.YMap),
                character.SpriteX, character.SpriteY, 55, 60);
        }
        catch(Exception ex)
        {
            Console.WriteLine("FAILED LOADING LEVEL : "+ ex);
        }
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


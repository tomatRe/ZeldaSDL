﻿using System;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    bool exit = false;
    Level level;
    Player character;
    Enemy enemy;
    Key k;

    public GameScreen(Hardware hardware) : base(hardware)
    {
        Console.WriteLine("Game screen created");
    }

    public void Run()
    {
        int keyPressed;

        level = new Level("level1.map");
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
                Bomb b = new Bomb(character.X, character.Y);
                b.Explosion(character, enemy);
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

        if ((character.X >= k.X - 15 && character.X <= k.X + 15) &&
                (character.Y >= k.Y - 15 && character.Y <= k.Y + 15))
            character.hasKey = true;

        //Damage collisions

        if ((character.X >= enemy.X - 30 && character.X <= enemy.X + 30) &&
                (character.Y >= enemy.Y - 30 && character.Y <= enemy.Y + 30))
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
        if (character.bombCooldown >= 0)
        {
            character.bombCooldown--;
            if (character.bombCooldown%100 == 0)
                Console.WriteLine(character.bombCooldown);

        }

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

        if (enemy.hearts > 0)
            hardware.DrawSprite(Sprite.ockorok,
            (short)(enemy.X - level.XMap),
            (short)(enemy.Y - level.YMap),
            enemy.SpriteX, enemy.SpriteY, 68, 63);

        if (!character.hasKey)
            hardware.DrawSprite(Sprite.objects,
                (short)(k.X - level.XMap),
                (short)(k.Y - level.YMap),
                419, 21, 7, 16);

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


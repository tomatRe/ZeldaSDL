﻿using System;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    Player character;
    Enemy enemy;
    Heart testHeart;

    public GameScreen(Hardware hardware) : base(hardware)
    {
    }

    public void Run()
    {
        int keyPressed;

        character = Player.GetPlayer();
        enemy = new Enemy(50,600);
        testHeart = new Heart(512,200);

        do
        {
            pollEvents();
            keyPressed = hardware.KeyPressed();

            // 1. Draw everything
            hardware.ClearScreen();

            hardware.DrawSprite(Sprite.link, character.X, character.Y,
                character.SpriteX, character.SpriteY, 55, 60);

            hardware.DrawSprite(Sprite.ockorok, enemy.X, enemy.Y,
                enemy.SpriteX, enemy.SpriteY, 68, 63);

            hardware.DrawSprite(Sprite.heart, 512, 200, 0, 0, 16, 16);

            DrawPlayerHUD();
            hardware.UpdateScreen();

            // 2. Move character from keyboard input

            CheckInput();

            // 3. Move enemies and objects

            enemy.Move();

            // 4. Check collisions and update game state

            CheckCollisions();

            // 5. Pause game
            Thread.Sleep(5);


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

    public static void PauseTillNextFrame(int sleeptime)
    {
        Thread.Sleep(sleeptime);
    }

    private void pollEvents()
    {
        Sdl.SDL_PumpEvents();
    }
}


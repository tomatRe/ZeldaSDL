using System;
using System.Threading;
using Tao.Sdl;

class GameScreen : Screen
{
    Player character;
    Enemy enemy;

    public GameScreen(Hardware hardware) : base(hardware)
    {
    }

    public void Run()
    {
        int keyPressed;

        character = Player.GetPlayer();
        enemy = new Enemy();

        do
        {
            pollEvents();
            keyPressed = hardware.KeyPressed();

            // 1. Draw everything
            hardware.ClearScreen();
            hardware.DrawSprite(Sprite.SpriteSheet, character.X, character.Y,
                character.SpriteX, character.SpriteY, Sprite.SPRITE_WIDTH, Sprite.SPRITE_HEIGHT);
            hardware.UpdateScreen();

            // 2. Move character from keyboard input
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

            // 3. Move enemies and objects

            // 4. Check collisions and update game state

            CheckCollisions();

            // 5. Pause game
            Thread.Sleep(10);

        } while (keyPressed != Hardware.KEY_ESC);
    }

    public void MoveElements()
    {
        //To Do
    }

    public void CheckCollisions()
    {
        //To Do enviromental collisions

        if (enemy.X < character.Y + 5 && enemy.Y < character.X + 5)
        {
            if (character.isAttacking)
            {
                enemy.hearts--;
            }
        }
        if (character.cooldown >= 0)
        {
            character.cooldown--;
            Console.WriteLine(character.cooldown);
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


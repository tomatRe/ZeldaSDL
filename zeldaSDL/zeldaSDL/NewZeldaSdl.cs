using System;

/*
 * v0.01 Angel Rebollo Berná, 14/05/2018, Created all clases and soft logic.
 * v0.02 Angel Rebollo Berná, 15/05/2018, Developed the player class, but not his animations.
 * v0.03 Angel Rebollo Berná, 16/05/2018, Developed random enemy movement, and changed the game class squeleton.
 */

class NewZeldaSdl
{
    static void Main(string[] args)
    {
        GameController game = new GameController();

        game.Start();
    }
}


using System;
using System.IO;
using System.Collections.Generic; 

class Level : Map
{

    public Level(string levelName)
    {
        Walls = new List<Wall>();
        Doors = new List<Door>();
        Keys = new List<Key>();
        EnemySpawns = new List<EnemySpawn>();
        playerSpawns = new List<PlayerSpawn>();
        floorTiles = new List<Floor>();
        XMap = 0;
        YMap = 0;

        if (!File.Exists(levelName))
        {
            Console.WriteLine
                ("The level {0} does not exist or has errors",levelName);
        }
        else
        {

            try
            {
                StreamReader data = File.OpenText(levelName);
                string line = "";
                short numLines = 0;

                do
                {
                    line = data.ReadLine();
                    if (line == null)
                        Console.WriteLine("Level load success");
                    else
                    {
                        numLines++;

                        Width = (short)(line.Length * Sprite.SPRITE_WIDTH);
                        Height = (short)(numLines * Sprite.SPRITE_HEIGHT);

                        for (int i = 0; i < line.Length; i++)
                        {

                            switch (line[i])
                            {
                                case 'w'://walls(boundaries)
                                    AddWall(new Wall(
                                        (short)
                                        (i * Sprite.SPRITE_WIDTH),
                                        (short)
                                        (numLines * Sprite.SPRITE_HEIGHT)));
                                    break;

                                case 'X'://Origin of the level (also a wall)
                                    XMap = (short)
                                        (i * Sprite.SPRITE_WIDTH);
                                    YMap = (short)
                                        (numLines * Sprite.SPRITE_HEIGHT);

                                    AddWall(new Wall(
                                        (short)
                                        (i * Sprite.SPRITE_WIDTH),
                                        (short)
                                        (numLines * Sprite.SPRITE_HEIGHT)));
                                    break;

                                case 'x'://Origin of the level(without wall)
                                    XMap = (short)
                                        (i * Sprite.SPRITE_WIDTH);
                                    YMap = (short)
                                        (numLines * Sprite.SPRITE_HEIGHT);
                                    break;

                                case 'd'://Add door
                                    AddDoor(new Door(
                                        (short)
                                        (i * Sprite.SPRITE_WIDTH),
                                        (short)
                                        (numLines * Sprite.SPRITE_HEIGHT)));
                                    break;

                                case 'k'://Add key
                                    AddKey(new Key(
                                        (short)
                                        (i * Sprite.SPRITE_WIDTH),
                                        (short)
                                        (numLines * Sprite.SPRITE_HEIGHT)));
                                    break;

                                case 'e'://Add EnemySpawner
                                    AddEnemy(new EnemySpawn(
                                        (short)i, numLines));
                                    break;

                                case 'p'://Add PlayerSpawn
                                    AddPlayer(new PlayerSpawn(
                                        (short)i, numLines));
                                    break;

                                default://Add floor
                                    DrawGrass(new Floor(
                                        (short)
                                        (i * Sprite.SPRITE_WIDTH),
                                        (short)
                                        (numLines * Sprite.SPRITE_HEIGHT)));
                                    break;
                            }
                        }
                    }

                } while (line != null);
                data.Close();
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("PATH TOO LONG");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("INPUT/OUTPUT ERROR: " + ioEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }

    private void AddWall(Wall w)
    {
        Walls.Add(w);
    }

    private void AddDoor(Door d)
    {
        Doors.Add(d);
    }

    private void AddKey(Key k)
    {
        Keys.Add(k);
    }

    private void AddEnemy(EnemySpawn e)
    {
        EnemySpawns.Add(e);
    }

    private void AddPlayer(PlayerSpawn p)
    {
        playerSpawns.Add(p);
    }

    private void DrawGrass(Floor f)
    {
        floorTiles.Add(f);
    }
}
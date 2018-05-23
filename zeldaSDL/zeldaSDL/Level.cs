using System;
using System.IO;
using System.Collections.Generic; 

class Level : Map
{
    //----item variables----
    public int xKeyPosition { get; set; }
    public int yKeyPosition { get; set; }

    public int xDoorPosition { get; set; }
    public int yDoorPosition { get; set; }
    //------------------------

    //----level variables----
    public short Width { get; set; }
    public short Height { get; set; }
    public short XMap { get; set; }
    public short YMap { get; set; }

    public List<Sprite> Walls { get; }
    public List<Sprite> Doors { get; }
    public List<Key> Keys { get; }
    public List<EnemySpawn> EnemySpawns { get; }
    public List<PlayerSpawn> playerSpawns { get; }
    //------------------------

    public Level(string levelName)
    {
        XMap = 0;
        YMap = 0;

        if (!File.Exists(levelName))
        {
            Console.WriteLine
                ("The level {0} does not exist or has errors",levelName);
        }
        else
        {
            string[] data = File.ReadAllLines(levelName);

            if (data.Length > 0)
            {
                Width = (short)(data[0].Length * Sprite.SPRITE_WIDTH);
                Height = (short)(data.Length * Sprite.SPRITE_HEIGHT);

                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {

                        switch(data[i][j])
                        {
                            case 'w'://walls(boundaries)
                                AddWall(new Wall(
                                    (short)(j * Sprite.SPRITE_WIDTH),
                                    (short)(i * Sprite.SPRITE_HEIGHT)));
                                break;

                            case 'X'://Origin of the level (also a wall)
                                XMap = (short)(j * Sprite.SPRITE_WIDTH);
                                YMap = (short)(i * Sprite.SPRITE_HEIGHT);

                                AddWall(new Wall(
                                    (short)(j * Sprite.SPRITE_WIDTH),
                                    (short)(i * Sprite.SPRITE_HEIGHT)));
                                break;

                            case 'x'://Origin of the level(without wall)
                                XMap = (short)(j * Sprite.SPRITE_WIDTH);
                                YMap = (short)(i * Sprite.SPRITE_HEIGHT);
                                break;

                            case 'd'://Add door
                                AddDoor(new Door(
                                    (short)(j * Sprite.SPRITE_WIDTH),
                                    (short)(i * Sprite.SPRITE_HEIGHT)));
                                break;

                            case 'k'://Add key
                                AddKey(new Key(
                                    (short)(j * Sprite.SPRITE_WIDTH),
                                    (short)(i * Sprite.SPRITE_HEIGHT)));
                                break;

                            case 'e'://Add EnemySpawner
                                AddEnemy(new EnemySpawn((short)j,
                                    (short)i));
                                break;

                            case 'p'://Add PlayerSpawn
                                AddPlayer(new PlayerSpawn((short)j,
                                    (short)i));
                                break;

                            default:
                                DrawGrass();
                                break;
                        }
                    }
                }
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

    private void DrawGrass()
    {
        //to do
    }
}
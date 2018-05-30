using System.Collections.Generic;

class Map
{
    Level currentLevel { get; set; }

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

    public List<Wall> Walls { get; set; }
    public List<Door> Doors { get; set; }
    public List<Key> Keys { get; set; }
    public List<EnemySpawn> EnemySpawns { get; set; }
    public List<PlayerSpawn> playerSpawns { get; set; }
    public List<Floor> floorTiles { get; set; }
    //------------------------

    public Map()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        int levelNumber = 0;
        levelNumber++;
        if (levelNumber > 10 || levelNumber < 1)
            levelNumber = 1;
        currentLevel = new Level("level"+ levelNumber + ".map");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    //------------------------

    public Level(string levelname)
    {
        XMap = 0;
        YMap = 0;
    }
}
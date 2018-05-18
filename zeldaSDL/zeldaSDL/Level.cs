using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Level : Map
{
    public int xKeyPosition { get; set; }
    public int yKeyPosition { get; set; }

    public int xDoorPosition { get; set; }
    public int yDoorPosition { get; set; }

    public Level(Hardware hardware) : base(hardware)
    {
    }

    //To Do
}
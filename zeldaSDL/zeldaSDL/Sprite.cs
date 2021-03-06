﻿using System;
using System.Collections.Generic;
using System.Text;


class Sprite
{

    //------Movement variables-----------
    protected short startX { get; set; }
    protected short startY { get; set; }

    public short width { get; set; }
    public short height { get; set; }
    protected short xSpeed { get; set; }
    protected short ySpeed { get; set; }
    protected bool visible { get; set; }
    //------Movement variables-----------

    //------Animation variables-----------
    protected Image[][] sequence;
    protected bool containsSequence;
    protected int currentFrame;

    public byte direction { get; set; }
    public const byte UP = 0;
    public const byte DOWN = 1;
    public const byte LEFT = 2;
    public const byte RIGHT = 3;

    public short frame = 0;
    //------Animation variables-----------

    public static Image link = new Image
        ("sprites/linkSpriteSheet.png", 576, 512);
    public static Image ockorok = new Image
        ("sprites/oktorok.png", 136, 280);
    public static Image heart = new Image
        ("sprites/heart.png", 16, 16);
    public static Image objects = new Image
        ("sprites/Overworld.png", 640,576);
    public static Image key = new Image
        ("sprites/key.png", 25,25);
    public static Image wall = new Image
        ("sprites/wall.png", 46, 43);
    public static Image sideWall = new Image
        ("sprites/sideWall.png", 46, 43);
    public static Image grass = new Image
        ("sprites/grass.png", 3073, 2160);
    public static Image floor = new Image
        ("sprites/floor.png", 42, 43);

    private int currentFrameTick;
    private int updatesPerFrame;
    public const short SPRITE_WIDTH = 42;
    public const short SPRITE_HEIGHT = 43;

    public short X { get; set; }
    public short Y { get; set; }
    public short SpriteX { get; set; }
    public short SpriteY { get; set; }

    public void MoveTo(short x, short y)
    {
        X = x;
        Y = y;
    }

    public void NextFrame()
    {
        if (!containsSequence)
            return;

        currentFrameTick++;
        if (currentFrameTick < updatesPerFrame)
            return;

        currentFrameTick = 0;
        currentFrame++;
        /*if (currentFrame >= sequence[direction].Length)
            currentFrame = 0;*/
    }


    public void LoadSequence(byte direction, string[] names)
    {
        int amountOfFrames = names.Length;
        sequence[direction] = new Image[amountOfFrames];
        for (int i = 0; i < amountOfFrames; i++)
            sequence[direction][i] = new Image(names[i]);
        containsSequence = true;
        currentFrame = 0;
    }

    public bool CollidesWith(Wall sp)
    {
        return (X + Sprite.SPRITE_WIDTH > sp.X && X < sp.X + Sprite.SPRITE_WIDTH &&
                Y + Sprite.SPRITE_HEIGHT > sp.Y-15 && Y < sp.Y + Sprite.SPRITE_HEIGHT/2);
    }

    public bool CollidesWith(List<Wall> sprites)
    {
        foreach (Wall sp in sprites)
            if (this.CollidesWith(sp))
                return true;
        return false;
    }

    public bool CollidesWith(SideWall sp)
    {
        return (X + Sprite.SPRITE_WIDTH > sp.X && X < sp.X + Sprite.SPRITE_WIDTH &&
                Y + Sprite.SPRITE_HEIGHT > sp.Y && Y < sp.Y + Sprite.SPRITE_HEIGHT);
    }

    public bool CollidesWith(List<SideWall> sprites)
    {
        foreach (SideWall sp in sprites)
            if (this.CollidesWith(sp))
                return true;
        return false;
    }

    public bool CollidesWith(Floor sp)
    {
        return (X + Sprite.SPRITE_WIDTH > sp.X && X < sp.X + Sprite.SPRITE_WIDTH &&
                Y + Sprite.SPRITE_HEIGHT > sp.Y && Y < sp.Y + Sprite.SPRITE_HEIGHT);
    }

    public bool CollidesWith(List<Floor> sprites)
    {
        foreach (Floor sp in sprites)
            if (this.CollidesWith(sp))
                return true;
        return false;
    }

    public void LoadSequence(string[] names)
    {
        LoadSequence(RIGHT, names);
    }
}


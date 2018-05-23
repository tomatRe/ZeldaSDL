/*
 * 0.01, 17-05-2018 Almudena López: Created class 
 *      based on Saboteur and Gauntlet
 * 
 */

using Tao.Sdl;
using System;

class Font
{
    IntPtr fontType;

    public Font(string fileName, int fontSize)
    {
        fontType = SdlTtf.TTF_OpenFont(fileName, fontSize);
        if (fontType == IntPtr.Zero)
        {
            Console.WriteLine("Font type not found");
        }
    }

    public IntPtr GetFontType()
    {
        return fontType;
    }
}


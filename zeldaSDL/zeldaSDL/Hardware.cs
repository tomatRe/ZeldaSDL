using System;
using Tao.Sdl;

class Hardware
{
    //--------------KEYBOARD---------------------
    public const int KEY_ESC = Sdl.SDLK_ESCAPE;
    public const int KEY_CTRL = Sdl.SDLK_LCTRL;
    public const int KEY_UP = Sdl.SDLK_UP;
    public const int KEY_DOWN = Sdl.SDLK_DOWN;
    public const int KEY_LEFT = Sdl.SDLK_LEFT;
    public const int KEY_RIGHT = Sdl.SDLK_RIGHT;
    public const int KEY_SPACE = Sdl.SDLK_SPACE;
    public const int KEY_H = Sdl.SDLK_h;
    //--------------KEYBOARD---------------------

    //--------------CONTROLLER-------------------
    static bool isThereJoystick;
    static IntPtr joystick;

    public const int CONT_STRT = Sdl.SDLK_ESCAPE;
    public const int CONT_B = Sdl.SDLK_LCTRL;
    public const int CONT_A = Sdl.SDLK_SPACE;
    public const int CONT_UP = Sdl.SDLK_UP;
    public const int CONT_DOWN = Sdl.SDLK_DOWN;
    public const int CONT_LEFT = Sdl.SDLK_LEFT;
    public const int CONT_RIGHT = Sdl.SDLK_RIGHT;
    //--------------CONTROLLER-------------------

    short screenWidth;
    short screenHeight;
    short colorDepth;
    IntPtr screen;

    public Hardware(short width, short height, short depth, bool fullScreen)
    {
        screenWidth = width;
        screenHeight = height;
        colorDepth = depth;

        int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
        if (fullScreen)
            flags = flags | Sdl.SDL_FULLSCREEN;

        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        screen = Sdl.SDL_SetVideoMode(screenWidth, screenHeight, colorDepth, flags);
        Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_SetClipRect(screen, ref rect);

        Console.WriteLine("Created hardware");
    }

    //----------CONTROLLER SUPPORT-----------

    // Joystick methods

    /** JoystickPressed: returns TRUE if
        *  a certain button in the joystick/gamepad
        *  has been pressed
        */
    public static bool JoystickPressed(int boton)
    {
        if (!isThereJoystick)
            return false;

        if (Sdl.SDL_JoystickGetButton(joystick, boton) > 0)
            return true;
        else
            return false;
    }

    /** JoystickMoved: returns TRUE if
        *  the joystick/gamepad has been moved
        *  up to the limit in any direction
        *  Then, int returns the corresponding
        *  X (1=right, -1=left)
        *  and Y (1=down, -1=up)
        */
    public static bool JoystickMoved(out int posX, out int posY)
    {
        posX = 0; posY = 0;
        if (!isThereJoystick)
            return false;

        posX = Sdl.SDL_JoystickGetAxis(joystick, 0);  // Leo valores (hasta 32768)
        posY = Sdl.SDL_JoystickGetAxis(joystick, 1);
        // Normalizo valores
        if (posX == -32768) posX = -1;  // Normalizo, a -1, +1 o 0
        else if (posX == 32767) posX = 1;
        else posX = 0;
        if (posY == -32768) posY = -1;
        else if (posY == 32767) posY = 1;
        else posY = 0;

        if ((posX != 0) || (posY != 0))
            return true;
        else
            return false;
    }


    /** JoystickMovedRight: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely to the right
        */
    public static bool JoystickMovedRight()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posX == 1))
            return true;
        else
            return false;
    }

    /** JoystickMovedLeft: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely to the left
        */
    public static bool JoystickMovedLeft()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posX == -1))
            return true;
        else
            return false;
    }


    /** JoystickMovedUp: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely upwards
        */
    public static bool JoystickMovedUp()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posY == -1))
            return true;
        else
            return false;
    }


    /** JoystickMovedDown: returns TRUE if
        *  the joystick/gamepad has been moved
        *  completely downwards
        */
    public static bool JoystickMovedDown()
    {
        if (!isThereJoystick)
            return false;

        int posX = 0, posY = 0;
        if (JoystickMoved(out posX, out posY) && (posY == 1))
            return true;
        else
            return false;
    }
    //----------CONTROLLER SUPPORT-----------

    internal static void FatalError(string error)
    {
        Console.WriteLine(error);
    }

    public void DrawImage(Image img)
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, img.ImageWidth,
            img.ImageHeight);
        Sdl.SDL_Rect target = new Sdl.SDL_Rect(img.X, img.Y,
            img.ImageWidth, img.ImageHeight);
        Sdl.SDL_BlitSurface(img.ImagePtr, ref source, screen, ref target);
    }

    // Draws a sprite from a sprite sheet in the specified X and Y position of the screen
    // The sprite to be drawn is determined by the x and y 
    //      coordinates within the image, and the width and height to be cropped
    public void DrawSprite
        (Image image, short xScreen, short yScreen,
        short x, short y, short width, short height)
    {
        Sdl.SDL_Rect src = new Sdl.SDL_Rect(x, y, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(xScreen, yScreen, width, height);
        Sdl.SDL_BlitSurface(image.ImagePtr, ref src, screen, ref dest);
    }

    // Update screen
    public void UpdateScreen()
    {
        Sdl.SDL_Flip(screen);
    }

    // Detects if the user presses a key and returns the code of the key pressed
    public int KeyPressed()
    {
        
        int pressed = -1;

        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event keyEvent;
        if (Sdl.SDL_PollEvent(out keyEvent) == 1)
        {
            if (keyEvent.type == Sdl.SDL_KEYDOWN)
            {
                pressed = keyEvent.key.keysym.sym;
            }
        }

        if (pressed != -1)
            Console.WriteLine("Key input: " + pressed);
        
        return pressed;
    }

    // Checks if a given key is now being pressed
    public bool IsKeyPressed(int key)
    {

        bool pressed = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event evt;
        Sdl.SDL_PollEvent(out evt);
        int numKeys;
        byte[] keys = Sdl.SDL_GetKeyState(out numKeys);

        if (keys[key] == 1)
            pressed = true;
            
        return pressed;
    }

    // Clears the screen
    public void ClearScreen()
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, screenWidth, screenHeight);
        Sdl.SDL_FillRect(screen, ref source, 0);
    }

    ~Hardware()
    {
        Sdl.SDL_Quit();
    }
}


using System;
using System.Collections.Generic;
using System.Text;


class Screen
{
    protected Hardware hardware;

    public Screen(Hardware hardware)
    {
        this.hardware = hardware;
    }

    public virtual void Show()
    {
    }
}


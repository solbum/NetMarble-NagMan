using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public PlayerMovement player;

    private void Update()
    {
        
    }

    public float GetXValue()
    {
        return this.Direction.x;
    }
}
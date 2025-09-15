using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float cooldown;
    private float timeRemaining;

    public Timer(float cooldown)
    {
        this.cooldown = cooldown;
        this.timeRemaining = 0;
    }

    public void Update(float delta)
    {
        if (timeRemaining > 0f)
            timeRemaining -= delta;
    }

    public bool isReady => timeRemaining <= 0f;

    public void Reset()
    {
        timeRemaining = cooldown;
    }
}

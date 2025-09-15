using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperScript
{
    public static Bounds GetBounds()
    {
        Bounds bounds = GameObject.FindGameObjectWithTag("Ground").GetComponent<SpriteRenderer>().bounds;
        return bounds;
    }
}

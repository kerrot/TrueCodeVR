﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpableObject : ActObjectBase
{
    private GameObject player;

    private Vector3 tmpLocation;
    private float YOffset;

    private void Awake()
    {
        GameSystem system = GameObject.FindObjectOfType<GameSystem>();
        if (system)
        {
            player = system.GetChara();
        }

        YOffset = player.transform.position.y;
    }

    public override void Act()
    {
        if (Warpper.CanWarp)
        {
            tmpLocation = RayCastBase.Hit.point;
            tmpLocation.y += YOffset;
            player.transform.position = tmpLocation;
        }
    }
}
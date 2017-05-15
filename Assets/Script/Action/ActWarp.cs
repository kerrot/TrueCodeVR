using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActWarp : ActBase
{
    public override void Action(ActionParam param)
    {
        Warpper w = GameObject.FindObjectOfType<Warpper>();
        if (w)
        {
            w.Warp();
            return;
        }

        Debug.Log("ActWarp Param Error: " + param);
    }
}

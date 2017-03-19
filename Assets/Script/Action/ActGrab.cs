using UnityEngine;
using System.Collections;

public class ActGrab : ActBase
{
    public override void Action(ActionParam param)
    {
        Graber g = GameObject.FindObjectOfType<Graber>();
        if (g)
        {
            g.Grab(param.target);
        }
    }
}

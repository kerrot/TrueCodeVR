using UnityEngine;
using System.Collections;

public class ActGrab : ActBase
{
    public override void Action(ActionParam param)
    {
        Graber g = param.target.GetComponent<Graber>();
        if (g)
        {
            g.Grab(param.self);
            return;
        }

        Debug.Log("ActGrab Param Error: " + param);
    }
}

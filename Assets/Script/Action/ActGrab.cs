using UnityEngine;
using System.Collections;

public class ActGrab : ActBase
{
    public override void Action(ActionParam param)
    {
        Graber g = param.self.GetComponent<Graber>();
        if (g)
        {
            g.Grab(param.target);
            return;
        }

        Debug.Log("ActGrab Param Error: " + param);
    }
}

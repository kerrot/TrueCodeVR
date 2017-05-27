using UnityEngine;
using System.Collections;

public class ActRelease : ActBase
{
    public override void Action(ActionParam param)
    {
        Graber g = param.target.GetComponent<Graber>();
        if (g)
        {
            g.Release();
            return;
        }

        Debug.Log("ActRelease Param Error: " + param);
    }
}

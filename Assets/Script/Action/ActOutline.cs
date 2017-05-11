using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActOutline : ActBase
{
    public override void Action(ActionParam param)
    {
        Ouline_AvgNormal outline = param.target.GetComponent<Ouline_AvgNormal>();
        if (outline != null)
        {
            outline.enabled = param.param == "true";
            return;
        }

        Debug.Log("ActOutline Param Error: " + param);
    }
}

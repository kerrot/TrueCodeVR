using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActVisible : ActBase
{
    public override void Action(ActionParam param)
    {
        param.target.SetActive(param.param == "true");
    }
}

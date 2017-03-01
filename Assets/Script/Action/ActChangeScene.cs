using UnityEngine;
using System.Collections;

public class ActChangeScene : ActBase
{
    public override void Action(ActionParam param)
    {
        SceneControler s = GameObject.FindObjectOfType<SceneControler>();
        if (s)
        {
            s.LoadScene(param.str);
        }
    }
}

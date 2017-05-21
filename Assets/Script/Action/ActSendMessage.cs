using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSendMessage : ActBase
{
    public override void Action(ActionParam param)
    {
        MessageReceiver mr = param.target.GetComponent<MessageReceiver>();
        if (mr != null)
        {
            mr.ReceiveMassage(param.param);
            return;
        }

        Debug.Log("ActSendMessage Param Error: " + param);
    }
}

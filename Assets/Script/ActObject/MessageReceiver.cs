using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageReceiver : InteractableObject
{
    public abstract void ReceiveMassage(string msg);
}

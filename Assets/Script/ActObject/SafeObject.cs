using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SafeObject : MessageReceiver
{
    [SerializeField]
    private string password;
    [SerializeField]
    private string clear;
    [SerializeField]
    private List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();

    private bool opened = false;

    private string currentInput;
    public string Current { get { return currentInput; } }

    [SerializeField]
    private GameObject target;

    public override void ReceiveMassage(string msg)
    {
        if (opened)
        {
            return;
        }

        if (msg == clear)
        {
            currentInput = "";
        }

        currentInput += msg;

        if (currentInput == password)
        {
            opened = true;
            Act(actions, target);
        }
    }
}

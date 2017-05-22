using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class LockObject : MessageReceiver
{
    [SerializeField]
    private string password;
    [SerializeField]
    private string clear;
    [SerializeField]
    private List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();
	[SerializeField]
	private TextMesh display;
	[SerializeField]
    private GameObject target;

    private bool opened = false;

    private string currentInput;
    public string Current { get { return currentInput; } }


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
		else
		{
			currentInput += msg;	
		}
		UpdateDisplay();

        if (currentInput == password)
        {
            opened = true;
            Act(actions, target);
        }
    }

	void UpdateDisplay()
	{
		if (display)
		{
			display.text = "";
			for (int i = 0; i < currentInput.Length; ++i)
			{
				display.text += "*";
			}
		}
	}
}

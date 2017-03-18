using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

public class InteractableObject : ActObjectBase
{
    [SerializeField]
    protected List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();

    protected GameObject target;

    public override void Act()
    {
        actions.ForEach(a =>
        {
            ActBase act = ActBase.GetAction(a.type);
            if (act != null)
            {
                a.target = (a.target) ? a.target : target;
                if (a.delay > 0)
                {
                    Observable.Timer(TimeSpan.FromSeconds(a.delay)).Subscribe(_ => act.Action(a)).AddTo(this);
                }
                else
                {
                    act.Action(a);
                }
            }
        });
    }
}

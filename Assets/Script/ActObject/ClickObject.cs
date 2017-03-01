using UniRx;
using UniRx.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour {

    [SerializeField]
    protected List<ClickParam> actions = new List<ClickParam>();

    void Start ()
    {
        this.OnMouseUpAsObservable().Subscribe(_ => Action());
	}

    [Serializable]
    public struct ClickParam
    {
        public float param;
        public string str;
        public ActBase.ActionType type;
    }


    protected ActBase.ActionParam param = new ActBase.ActionParam();

    protected void Action()
    {
        param.self = gameObject;
        actions.ForEach(a =>
        {
            ActBase act = ActBase.GetAction(a.type);
            if (act != null)
            {
                param.value = a.param;
                param.str = a.str;
                act.Action(param);
            }
        });
    }
}

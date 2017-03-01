using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ColliderObject : MonoBehaviour
{
    [SerializeField]
    protected List<ColliderActionUnit> actions = new List<ColliderActionUnit>();

    public enum ActionTarget
    {
        SELF,
        TARGET,
    }

    [Serializable]
    public struct ColliderActionUnit
    {
        public float param;
        public ActionTarget target;
        public ActBase.ActionType type;
    }

    protected ActBase.ActionParam param = new ActBase.ActionParam();

    protected void Action(GameObject target)
    {
        param.self = gameObject;
        actions.ForEach(a =>
        {
            ActBase act = ActBase.GetAction(a.type);
            if (act != null)
            {
                param.value = a.param;
                param.obj = (a.target == ActionTarget.SELF) ? gameObject : target;
                act.Action(param);
            }
        });
    }
}
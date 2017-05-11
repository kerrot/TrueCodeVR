using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RayCastBase : TriggerObject
{
    [SerializeField]
    List<ActBase.ActionParam> enterActs = new List<ActBase.ActionParam>();
    [SerializeField]
    List<ActBase.ActionParam> leaveActs = new List<ActBase.ActionParam>();
    [SerializeField]
    private GameObject target;

    protected RaycastHit hit;
    public RaycastHit Hit { get { return hit; } }

    public abstract void Cast();

    protected void CastIn()
    {
        Act(enterActs, target == null ? obj : target);
    }

    protected void CastOut(GameObject outObj)
    {
        Act(leaveActs, target == null ? outObj : target);
    }
}

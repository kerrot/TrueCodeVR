using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ColliderAction : TriggerObject
{
    [SerializeField]
    List<ActBase.ActionParam> enterActs = new List<ActBase.ActionParam>();
    [SerializeField]
    List<ActBase.ActionParam> leaveActs = new List<ActBase.ActionParam>();
    [SerializeField]
    private GameObject target;

    protected virtual void OnCollisionEnter(Collision coll)
    {
        obj = coll.gameObject;
        Act(enterActs, target == null ? coll.gameObject : target);
    }

    protected virtual void OnTriggerEnter(Collider coll)
    {
        obj = coll.gameObject;

        Act(enterActs, target == null ? coll.gameObject : target);
    }

    protected virtual void OnCollisionExit(Collision coll)
    {
        if (obj == coll.gameObject)
        {
            obj = null;
        }

        Act(leaveActs, target == null ? coll.gameObject : target);
    }

    protected virtual void OnTriggerExit(Collider coll)
    {
        if (obj == coll.gameObject)
        {
            obj = null;
        }

        Act(leaveActs, target == null ? coll.gameObject : target);
    }
}
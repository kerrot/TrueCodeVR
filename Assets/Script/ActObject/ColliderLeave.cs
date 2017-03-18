using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ColliderLeave : InteractableObject
{
    protected virtual void OnCollisionExit(Collision coll)
    {
        target = coll.gameObject;
        Act();
    }

    protected virtual void OnTriggerExit(Collider coll)
    {
        target = coll.gameObject;
        Act();
    }
}
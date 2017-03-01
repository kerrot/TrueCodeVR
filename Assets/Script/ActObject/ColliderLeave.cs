using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ColliderLeave : ColliderObject
{
    protected virtual void OnCollisionExit(Collision coll)
    {
        Action(coll.gameObject);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        Action(other.gameObject);
    }
}
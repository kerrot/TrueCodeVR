using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ColliderEnter : InteractableObject
{
    protected virtual void OnCollisionEnter(Collision coll)
    {
        target = coll.gameObject;
        Act();
    }

    protected virtual void OnTriggerEnter(Collider coll)
    {
        target = coll.gameObject;
        Act();
    }
}
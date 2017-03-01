using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ColliderEnter : ColliderObject
{
    protected virtual void OnCollisionEnter(Collision coll)
    {
        Action(coll.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Action(other.gameObject);
    }
}
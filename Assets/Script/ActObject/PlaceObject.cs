using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaceObject : InteractableObject
{
    [SerializeField]
    private List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();

    

    private void Start()
    {
        
    }
}

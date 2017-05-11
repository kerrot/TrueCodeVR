using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputObject : InteractableObject
{
    List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();

    [SerializeField]
    private InputBase input;
    [SerializeField]
    private GameObject target;

    private void Start()
    {
        if (input)
        {
            input.OnInput.Subscribe(_ => Act(actions, target));
        }
    }
}

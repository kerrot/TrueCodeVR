using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ClickOnObject : InteractableObject
{
    List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();

    [SerializeField]
    private InputBase input;
    [SerializeField]
    private TriggerObject trigger;
    [SerializeField]
    private GameObject target;

    private void Start()
    {
        if (input)
        {
            input.OnInput.Where(_ => trigger && trigger.CurrentObj)
                         .Subscribe(_ => Act(actions, target == null ? trigger.CurrentObj : target));
        }
    }
}

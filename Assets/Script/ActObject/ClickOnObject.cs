using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ClickOnObject : InteractableObject
{
    [SerializeField]
    private List<ActBase.ActionParam> actions = new List<ActBase.ActionParam>();

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
            input.OnInput.Where(_ => trigger && trigger.CurrentObj == gameObject)
                         .Subscribe(_ => Act(actions, target == null ? trigger.gameObject : target));
        }
    }
}

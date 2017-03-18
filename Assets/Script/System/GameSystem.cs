using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private RayCastBase rayCast;
    [SerializeField]
    private InputBase input;
    [SerializeField]
    private GameObject chara;
/// <summary>
/// ////
/// </summary>

    void Start ()
    {
        this.UpdateAsObservable().Subscribe(_ => UniRxUpdate());

        if (input)
        {
            input.OnInput.Where(_ => RayCastBase.CurrentObject)
                         .Subscribe(_ => RayCastBase.CurrentObject.Act()).AddTo(this);
        }
	}

    public GameObject GetChara()
    {
        return chara;
    }

    void UniRxUpdate()
    {
        if (rayCast)
        {
            rayCast.Cast();
        }
    }
}

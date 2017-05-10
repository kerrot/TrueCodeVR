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

	public GameObject Chara { get { return chara; } }

    private void Awake()
    {
        Display.displays.ToObservable().Subscribe(d => d.Activate());
    }

    void Start ()
    {
        this.UpdateAsObservable().Subscribe(_ => UniRxUpdate());

        if (input)
        {
            input.OnInput.Where(_ => RayCastBase.CurrentObject)
                         .Subscribe(_ => RayCastBase.CurrentObject.Act()).AddTo(this);

            input.OnWarp.Where(_ => RayCastBase.CurrentObject && RayCastBase.CurrentObject is WarpableObject)
                        .Subscribe(_ =>
                        {
                            WarpableObject warp = RayCastBase.CurrentObject as WarpableObject;
                            warp.Warp();
                        }).AddTo(this);
        }
	}

    void UniRxUpdate()
    {
        if (rayCast)
        {
            rayCast.Cast();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private RayCastBase rayCast;

    private void Awake()
    {
        Display.displays.ToObservable().Subscribe(d => d.Activate());
    }

    void Start ()
    {
        if (rayCast)
        {
            this.UpdateAsObservable().Subscribe(_ => rayCast.RayCast());
        }
	}
}

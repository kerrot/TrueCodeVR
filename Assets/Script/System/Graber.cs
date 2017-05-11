using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Graber : MonoBehaviour {
    [SerializeField]
    private InputBase input;

    private GameObject obj;

    private void Awake()
    {
        if (input)
        {
            input.OnInput.Where(_ => obj).Subscribe(_ => Release()).AddTo(this);
        }
    }


    public void Grab(GameObject o)
    {
        if (obj != null)
        {
            Release();
        }

        obj = o;
        obj.transform.parent = transform;
    }

    public void Release()
    {
        obj.transform.parent = null;
        obj = null;
    }
}

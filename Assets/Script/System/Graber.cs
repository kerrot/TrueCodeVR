using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Graber : MonoBehaviour {
    [SerializeField]
    private InputBase input;
    [SerializeField]
    private Vector3 releaseShift;

    static private GameObject obj;
    static public GameObject GrabbingObj { get { return obj; } }

    private void Awake()
    {
        input.OnInput.Where(_ => obj)
                     .Subscribe(_ => Release()).AddTo(this);
    }

    void Start ()
    {
        

        this.LateUpdateAsObservable().Where(_ => obj).Subscribe(_ => obj.transform.localPosition = Vector3.zero);
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
        GameObject tmp = obj;
        obj.transform.parent = null;
        obj = null;

        tmp.transform.position = RayCastBase.Hit.point + releaseShift;
    }
}

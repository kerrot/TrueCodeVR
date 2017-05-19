using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Graber : MonoBehaviour {
    [SerializeField]
    private InputBase input;
	[SerializeField]
	private float strength = 1f;

    private GameObject obj;
	private Vector3 lastPosiiton;
	private System.IDisposable recordSubject;

    private void Awake()
    {
        if (input)
        {
			input.OnInputOff.Where(_ => obj).Subscribe(_ => Release()).AddTo(this);
        }
    }


    public void Grab(GameObject o)
    {
		if (o == obj) 
		{
			return;
		}

        if (obj != null)
        {
            Release();
        }

        obj = o;
		if (obj) 
		{
			obj.transform.parent = transform;

			Rigidbody rd = obj.GetComponent<Rigidbody> ();
			if (rd) 
			{
				rd.useGravity = false;
				rd.velocity = Vector3.zero;
				rd.angularVelocity = Vector3.zero;
			}

			recordSubject = this.LateUpdateAsObservable ().Subscribe (_ => lastPosiiton = obj.transform.position);
		}
    }

    public void Release()
    {
		if (obj) 
		{
			obj.transform.parent = null;
			Rigidbody rd = obj.GetComponent<Rigidbody> ();
			if (rd) 
			{
				rd.useGravity = true;
				rd.velocity = (obj.transform.position - lastPosiiton) * strength;
			}

			recordSubject.Dispose ();
		}

        obj = null;
    }
}

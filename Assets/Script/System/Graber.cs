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

    private Vector3 grabPosition;
    private Quaternion grabRotation;

    private void Awake()
    {
        if (input)
        {
			input.OnInputOff.Where(_ => obj).Subscribe(_ => Release()).AddTo(this);
        }
    }


    public void Grab(GameObject o)
    {
		if (o == null || o == obj || obj != null) 
		{
			return;
		}

        InteractableObject inter = o.GetComponent<InteractableObject>();
        if (inter == null)
        {
            return;
        }

        // change grab hand
        if (inter.Owner != null && inter.Owner != this)
        {
            inter.Owner.Release( );
        }

        obj = o;
		if (obj) 
		{
			obj.transform.parent = transform;

            grabPosition = obj.transform.localPosition;
            grabRotation = obj.transform.localRotation;

            obj.GetComponent<InteractableObject>().Owner = this;

            Collider coll = obj.GetComponent<Collider>();
            if (coll)
            {
                coll.isTrigger = true;
            }

			Rigidbody rd = obj.GetComponent<Rigidbody> ();
			if (rd) 
			{
				rd.useGravity = false;
				rd.velocity = Vector3.zero;
				rd.angularVelocity = Vector3.zero;
			}

			recordSubject = this.LateUpdateAsObservable ().Subscribe (_ => 
            {
                obj.transform.localPosition = grabPosition;
                obj.transform.localRotation = grabRotation;
                lastPosiiton = obj.transform.position;
            });
		}
    }

    public void Release()
    {
		if (obj) 
		{
            obj.GetComponent<InteractableObject>().Owner = null;

            Collider coll = obj.GetComponent<Collider>();
            if (coll)
            {
                coll.isTrigger = false;
            }

            obj.transform.parent = null;
			Rigidbody rd = obj.GetComponent<Rigidbody> ();
			if (rd) 
			{
				rd.useGravity = true;
				rd.AddForce((obj.transform.position - lastPosiiton) * strength);
			}

			recordSubject.Dispose ();
		}

        obj = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlaceObject : InteractableObject
{
    [SerializeField]
    private List<ActBase.ActionParam> inAct = new List<ActBase.ActionParam>();
	[SerializeField]
    private List<ActBase.ActionParam> outAct = new List<ActBase.ActionParam>();
	[SerializeField]
	private ColliderObject coll;
	[SerializeField]
	private GameObject target;
	[SerializeField]
	private float speed = 1f;

	private InteractableObject current;

    private void Start()
    {
        this.UpdateAsObservable().Where(_ => coll).Subscribe(_ => 
		{
			if (coll.CurrentObj)
			{
				InteractableObject inter = coll.CurrentObj.GetComponent<InteractableObject>();
				if (inter && inter.Owner == null)
				{
					if (Vector3.Distance(inter.transform.position, transform.position) < speed)
					{
						inter.transform.position = transform.position;
						inter.transform.rotation = transform.rotation;
						if (current == null)
						{
							current = inter;
							Act(inAct, target == null ? current.gameObject : target);
						}
					}
					else
					{
						inter.transform.position += (transform.position - inter.transform.position).normalized * speed;
					}
				}
			}
			else if (current != null)
			{
				Act(outAct, target == null ? current.gameObject : target);
				current = null; 
			}
		});
    }
}

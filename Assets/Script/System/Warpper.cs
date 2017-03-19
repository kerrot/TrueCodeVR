using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;

public class Warpper : MonoBehaviour
{
    [SerializeField]
    private GameObject chara;
    [SerializeField]
    private GameObject avatar;
    [SerializeField]
    private NavMeshAgent agent;

    private static bool canWarp;
    public static bool CanWarp { get { return canWarp; } }


    private CapsuleCollider coll;
    private MeshRenderer render;
    private Vector3 collShift;

    void Start ()
    {
        coll = avatar.GetComponent<CapsuleCollider>();
        render = avatar.GetComponentInChildren<MeshRenderer>();
        collShift = new Vector3(0, coll.height / 2 - coll.radius, 0);

        this.UpdateAsObservable().Subscribe(_ => UniRxUpdate());
	}

    void UniRxUpdate()
    {
        canWarp =   RayCastBase.CurrentObject != null && 
                    RayCastBase.CurrentObject is WarpableObject;

        chara.SetActive(canWarp);
        chara.transform.position = RayCastBase.Hit.point;

        render.material.color = (canWarp) ? Color.white : Color.red;

        if (canWarp)
        {
            //nav can reach
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(RayCastBase.Hit.point, path);
            canWarp &= path.status == NavMeshPathStatus.PathComplete;
            if (!canWarp)
            {
                render.material.color = Color.blue;
            }

            //avatar collision test
            canWarp &= Physics.OverlapCapsule(avatar.transform.position + collShift, avatar.transform.position - collShift, coll.radius).Length == 0;
            if (!canWarp)
            {
                render.material.color = Color.black;
            }

            //hit normal
            canWarp &= Vector3.Angle(Vector3.up, RayCastBase.Hit.normal) < 90;
            if (!canWarp)
            {
                render.material.color = Color.green;
            }
        }
    }
}

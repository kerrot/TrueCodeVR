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
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 Offset;

    private bool canWarp = false;
    public bool CanWarp { get { return canWarp; } }

    private CapsuleCollider coll;
    private MeshRenderer render;
    private Vector3 collShift;

    void Start ()
    {
        coll = avatar.GetComponent<CapsuleCollider>();
        render = avatar.GetComponentInChildren<MeshRenderer>();
        collShift = new Vector3(0, coll.height / 2 - coll.radius, 0);
	}

    public bool WarpTest(RaycastHit Hit)
    {
        canWarp = Hit.collider != null;

        chara.SetActive(canWarp);
        chara.transform.position = Hit.point;

        render.material.color = (canWarp) ? Color.white : Color.red;

        if (canWarp)
        {
            //nav can reach
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(Hit.point, path);
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
            canWarp &= Vector3.Angle(Vector3.up, Hit.normal) < 90;
            if (!canWarp)
            {
                render.material.color = Color.green;
            }
        }

        return canWarp;
    }

    public void Warp()
    {
        if (CanWarp)
        {
            player.transform.position = chara.transform.position + Offset;
        }
    }
}

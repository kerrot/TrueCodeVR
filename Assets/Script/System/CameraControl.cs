using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    Vector3 tmpPosition;
    Vector3 tmpMouse;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        SceneControler sc = GameObject.FindObjectOfType<SceneControler>();
        if (sc && sc.LoadFromAnother)
        {
            Fadout();
        }

        this.UpdateAsObservable().Subscribe(_ => UniRxUpdate());
    }

    public void FadinComplete()
    {
        SceneControler sc = GameObject.FindObjectOfType<SceneControler>();
        if (sc)
        {
            sc.DoChangeScene();
        }
    }

    public void Fadin()
    {
        anim.SetTrigger("Fadin");
    }

    public void Fadout()
    {
        anim.SetTrigger("Fadout");
    }

    void UniRxUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tmpPosition = target.transform.position;
            tmpMouse = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 diff = Input.mousePosition - tmpMouse;
            Vector3 now = tmpPosition + new Vector3(diff.x, 0, diff.y);

            transform.LookAt(now);
        }
    }
}

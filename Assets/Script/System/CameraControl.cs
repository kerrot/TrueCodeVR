using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        SceneControler sc = GameObject.FindObjectOfType<SceneControler>();
        if (sc && sc.LoadFromAnother)
        {
            Fadout();
        }
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
}

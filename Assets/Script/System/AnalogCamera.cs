using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnalogCamera : MonoBehaviour {

    [SerializeField]
    private List<Camera> cameras = new List<Camera>();

    private void Start()
    {
        Activate(0);
    }

    public void Activate(int num)
    {
        if (num >= 0 && num < cameras.Count)
        {
            cameras.ForEach(c => c.gameObject.SetActive(false));
            cameras[num].gameObject.SetActive(true);
        }
    }
}

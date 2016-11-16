using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	[SerializeField]
	private float x;
	[SerializeField]
	private float y;
	[SerializeField]
	private float speed;

	// Update is called once per frame
	void Update () {
		y += Input.GetAxis("Horizontal");
		transform.position += transform.forward * Input.GetAxis("Vertical") * speed;



		transform.rotation = Quaternion.Euler(0, y, 0);
	}
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;

	// Update is called once per frame
	void Update ()
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		Vector3 moveBy = new Vector3(horizontal, 0, vertical);
		Vector3 final = moveBy * this.speed * Time.deltaTime;

		this.transform.Translate(final);
	}
}

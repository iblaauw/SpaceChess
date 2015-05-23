using UnityEngine;
using System.Collections;

public class functions : MonoBehaviour {

	public Transform cube;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				Instantiate(cube, new Vector3(i, j, 0), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}

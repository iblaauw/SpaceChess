using UnityEngine;
using System.Collections;

public class functions : MonoBehaviour {

	public Transform cube;
	// Errors? What errors?
	// This is going to be a fun project.
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				for (int k = 0; k < 8; k++){
					Instantiate(cube, new Vector3(i, j, k), Quaternion.identity);
				}
			}
		}
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}

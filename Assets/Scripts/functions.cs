using UnityEngine;
using System.Collections;

public class functions : MonoBehaviour {

	public Transform cube;
	public Transform grid_line;
	public Transform queen_sphere;

	public Object[] cube_arr = new Object[512];
	public Object[] grid_arr = new Object[243];
	
	void Start () {
		// Make cubes
		int cube_arr_num = 0;
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				for (int k = 0; k < 8; k++) {
					cube_arr[cube_arr_num++] = 
						Instantiate (cube, new Vector3 (i, j, k), Quaternion.identity);
				}
			}
		}

		// Make grid lines
		float offset_large = 3.5f;
		float offset_small = -0.5f;

		int grid_num = 0;

		for (int axis = 1; axis < 4; ++axis) {
			for (float dim_1 = 0; dim_1 < 9; ++dim_1) {
				for (float dim_2 = 0; dim_2 < 9; ++dim_2) {

					if (axis == 1) {
						grid_arr[grid_num++] = Instantiate (grid_line, 
					         	new Vector3 (offset_large, offset_small + dim_1, offset_small + dim_2),
					            Quaternion.Euler (0, 0, 0));
					} 
					else if (axis == 2) {
						grid_arr[grid_num++] = Instantiate (grid_line, 
					            new Vector3 (offset_small + dim_1, offset_small + dim_2, offset_large),
					            Quaternion.Euler (0, 90, 0));
					} 
					else if (axis == 3) {
						grid_arr[grid_num++] = Instantiate (grid_line, 
					            new Vector3 (offset_small + dim_1, offset_large, offset_small + dim_2),
					            Quaternion.Euler (0, 0, 90));
					}
				}
			}
		}


		// Make queen sphere
		Instantiate (queen_sphere,
		             new Vector3 (3f, 7f, 0f),
		             Quaternion.identity);	           




	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			print ("Making progress!");
		} 
		else if (Input.GetKeyDown ("k")) {
			print ("Unser König!");
		}
	}
}

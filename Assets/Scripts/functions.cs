using UnityEngine;
using System.Collections;

public class functions : MonoBehaviour {

	public Transform cube;
	public Transform grid_line;
	public Transform queen_sphere;
	
	void Start () {
		// Make cubes
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				for (int k = 0; k < 8; k++) {
					Instantiate (cube, new Vector3 (i, j, k), Quaternion.identity);
				}
			}
		}

		// Make grid lines
		float offset_large = 3.5f;
		float offset_small = -0.5f;

		for (int axis = 1; axis < 4; ++axis) {
			for (float dim_1 = 0; dim_1 < 9; ++dim_1) {
				for (float dim_2 = 0; dim_2 < 9; ++dim_2) {

					if (axis == 1) {
						Instantiate (grid_line, 
					         	new Vector3 (offset_large, offset_small + dim_1, offset_small + dim_2),
					            Quaternion.Euler (0, 0, 0));
					} 
					else if (axis == 2) {
						Instantiate (grid_line, 
					            new Vector3 (offset_small + dim_1, offset_small + dim_2, offset_large),
					            Quaternion.Euler (0, 90, 0));
					} 
					else if (axis == 3) {
						Instantiate (grid_line, 
					            new Vector3 (offset_small + dim_1, offset_large, offset_small + dim_2),
					            Quaternion.Euler (0, 0, 90));
					}

				}
			}
		}


		// Make queen sphere
		//Instantiate (queen_sphere,
		//             new Vector3 (4, 4, 4),
		//             Quaternion.identity);



						           




	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Pieces
// 1: pawn
// 2: bishop
// 3: rook

public class GameBrain : MonoBehaviour {
	public int gridSize;
	public GameObject[, ,] cubeArr;
	public GameObject[, ,] pawnArr;
	public GameObject testCube;
	public int selectPhase;
	public int x_plane, y_plane, z_plane;
	public int x_plane_prev, y_plane_prev, z_plane_prev;
	public int[, ,] board;

	// Use this for initialization
	void Start () {

		gridSize = 8;

		// make and fill the board array, which stores which pieces are
		// in which cubes
		board = new int[gridSize, gridSize, gridSize];
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				for (int k = 0; k < gridSize; k++) {
					if (i == 1 || i == gridSize - 2) {
						board [i, j, k] = 2;
					} else {
						board [i, j, k] = 0;
					}
				}
			}
		}
					
		x_plane = y_plane = z_plane = 0;

		// Initialize the phase of plane selection for piece movement to 0
		selectPhase = 0;

		// Create "testCube", which will be used to pull a cube mesh for coloring the cubesr
		testCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		testCube.transform.localScale = new Vector3 (0, 0, 0);
		
		cubeArr = new GameObject[gridSize,gridSize,gridSize];
		GameObject cube = Resources.Load ("Cube") as GameObject;
		for (int i = 0; i < gridSize; i++){
			for (int j = 0; j < gridSize; j++) {
				for (int k = 0; k < gridSize; k++) {
					GameObject go = Instantiate (cube) as GameObject;
					go.transform.position = new Vector3 (i, j, k);
					cubeArr [i, j, k] = go;
				}
			}
		}
			
		GameObject gridLine  = Resources.Load("grid_line") as GameObject;
		// first do all const. x lines
		for (int i = 0; i < gridSize+1; i++){
			for (int j = 0; j < gridSize+1; j++){
				GameObject go1 = Instantiate(gridLine) as GameObject;
				go1.transform.position = new Vector3 ((float)gridSize/2 - 0.5f, (float)i - 0.5f, (float)j - 0.5f);
				GameObject go2 = Instantiate (gridLine) as GameObject;
				go2.transform.position = new Vector3 ((float)i - 0.5f, (float)gridSize/2 - 0.5f, (float)j - 0.5f);
				go2.transform.rotation = Quaternion.Euler (0, 0, 90);
				GameObject go3 = Instantiate (gridLine) as GameObject;
				go3.transform.position = new Vector3 ((float)i - 0.5f, (float)j - 0.5f, (float)gridSize/2-0.5f);
				go3.transform.rotation = Quaternion.Euler (0, 90, 0);
			}
		}

		pawnArr = new GameObject[gridSize, gridSize, 2];
		GameObject pawn = Resources.Load ("pawn") as GameObject;
		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				GameObject go1 = Instantiate (pawn) as GameObject;
				go1.transform.position = new Vector3 (1, (float)i, (float)j);
				pawnArr [i, j, 0] = go1;

				GameObject go2 = Instantiate (pawn) as GameObject;
				go2.transform.position = new Vector3 (gridSize-2, (float)i, (float)j);
				pawnArr [i, j, 1] = go2;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			GameObject.Find ("Main Camera").transform.position = new Vector3 (4.54f, 4.07f, -10.94f);
			GameObject.Find ("Main Camera").transform.rotation = Quaternion.Euler (0, 0.339f, -0.563f);
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			//print ("hi!");
			pawnArr [0, 0, 1].transform.position = new Vector3 (7, 0, 0);


			// Give the cube a mesh

			cubeArr [1, 0, 0].GetComponent<MeshFilter> ().mesh = testCube.GetComponent<MeshFilter> ().mesh;

			// Then color the cube red
			//Material newMaterial = new Material(Shader.Find("Standard"));
			//newMaterial.color = Color.red;
			cubeArr [1, 0, 0].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_pink");
		}

		// First phase of plane selection
		if (Input.GetKeyDown (KeyCode.Alpha1) && selectPhase == 0) {
			selectPhase = 1;
			print ("selectPhase = " + selectPhase.ToString ());
			print ("x_plane = " + x_plane.ToString ());

			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					cubeArr [x_plane, i, j].GetComponent<MeshFilter> ().mesh = testCube.GetComponent<MeshFilter> ().mesh;
					cubeArr [x_plane, i, j].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_pink");
				}
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha1) && selectPhase == 1) {
			print ("selectPhase = " + selectPhase.ToString ());

			x_plane_prev = x_plane;
			x_plane = (x_plane + 1) % gridSize;

			print ("x_plane = " + x_plane.ToString ());

			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					Destroy (cubeArr [x_plane_prev, i, j].GetComponent<MeshFilter> ().mesh);
					//cubeArr [x_plane_prev, i, j].GetComponent<MeshRenderer>().material =  new Material(Shader.Find ("Transparent"));
					cubeArr [x_plane, i, j].GetComponent<MeshFilter> ().mesh = testCube.GetComponent<MeshFilter> ().mesh;
					cubeArr [x_plane, i, j].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_pink");
				}
			}
		}
			
				
		// Second phase of plane selection
		if (Input.GetKeyDown (KeyCode.Alpha2) && selectPhase == 1) {
			selectPhase = 2;

			for (int i = 0; i < gridSize; i++) {
				cubeArr [x_plane, y_plane, i].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_red");
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && selectPhase == 2) {
			y_plane_prev = y_plane;
			y_plane = (y_plane + 1) % gridSize;

			for (int i = 0; i < gridSize; i++) {
				cubeArr [x_plane, y_plane_prev, i].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_pink");
				cubeArr [x_plane, y_plane, i].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_red");
			}
		}
			
		// Third phase of plane selection
		if (Input.GetKeyDown (KeyCode.Alpha3) && selectPhase == 2) {
			selectPhase = 3;

			cubeArr [x_plane, y_plane, z_plane].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_yellow");

			if (board [x_plane, y_plane, z_plane] == 1) {
				cubeArr [x_plane + 1, y_plane, z_plane].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_yellow");
			} else if (board [x_plane, y_plane, z_plane] == 2) {
				for (int i = 0; i < gridSize; i++) {
					for (int j = 0; j < gridSize; j++) {
						for (int k = 0; k < gridSize; k++) {
							if (Mathf.Abs (x_plane - i) == Mathf.Abs (y_plane - j) && Mathf.Abs (y_plane - j) == Mathf.Abs (z_plane - k)) {
								cubeArr [i, j, k].GetComponent<MeshFilter> ().mesh = testCube.GetComponent<MeshFilter> ().mesh;
								cubeArr [i, j, k].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_yellow");
							}
						}
					}
				}
			}
	
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && selectPhase == 3) {
			print ("selectPhase = " + selectPhase.ToString ());

			/// First clear and set colored cubes on board
			// Clear possible moves from previous piece selection
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					for (int k = 0; k < gridSize; k++) {
						if (i != x_plane) {
							Destroy (cubeArr [i, j, k].GetComponent<MeshFilter>().mesh);
						}
					}
				}
			}
			// Remake x_plane
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					cubeArr [x_plane, i, j].GetComponent<MeshFilter> ().mesh = testCube.GetComponent<MeshFilter> ().mesh;
					cubeArr [x_plane, i, j].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_pink");
				}
			}
			// Remake y_plane
			for (int i = 0; i < gridSize; i++) {
				cubeArr [x_plane, y_plane, i].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_red");
			}

			/// Now remake colored cube selected and possible peice moves if cube is occupied
			z_plane = (z_plane + 1) % gridSize;
			cubeArr [x_plane, y_plane, z_plane].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_yellow");
			print ("piece in cube: " + board [x_plane, y_plane, z_plane].ToString ());


			if (board [x_plane, y_plane, z_plane] == 1) {
				cubeArr [x_plane + 1, y_plane, z_plane].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_yellow");
			} else if (board [x_plane, y_plane, z_plane] == 2) {
				for (int i = 0; i < gridSize; i++) {
					for (int j = 0; j < gridSize; j++) {
						for (int k = 0; k < gridSize; k++) {
							if (Mathf.Abs (x_plane - i) == Mathf.Abs (y_plane - j) && Mathf.Abs (y_plane - j) == Mathf.Abs (z_plane - k)) {
								cubeArr [i, j, k].GetComponent<MeshFilter> ().mesh = testCube.GetComponent<MeshFilter> ().mesh;
								cubeArr [i, j, k].GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("translucent_yellow");
							}
						}
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			selectPhase = 0;
			// reset all cube colors
			for (int i = 0; i < gridSize; i++) {
				for (int j = 0; j < gridSize; j++) {
					for (int k = 0; k < gridSize; k++) {
						Destroy (cubeArr [i, j, k].GetComponent<MeshFilter> ().mesh);
					}
				}
			}
		}

			


			

			/*
		if (Input.GetKeyDown(KeyCode.S)){ 
			for (int i = 0; i < 9; i++){
				for (int j = 0; j < 9; j++){
					
					cubeArr[i, j, 0].
		*/

	
	}


}

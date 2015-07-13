// Code for the King. Rex. Der König. De Koning.
// You get the idea.

using UnityEngine;
using System.Collections;

public class king : MonoBehaviour {

	public Transform king_piece;

	void Start() {

		// Placing king in top middle of cube,
		// at least for now.
		Instantiate (king_piece, new Vector3 (5, 8, 1),
		             Quaternion.identity);
	}

	void Update() {

	}
}


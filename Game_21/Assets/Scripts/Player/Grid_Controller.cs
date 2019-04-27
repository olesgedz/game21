using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Controller : MonoBehaviour
{	
	// public float singleSize = 1f;
	public Camera mainCamera;

	void Update() {
		Vector3 pos = Input.mousePosition;
		pos = mainCamera.ScreenToWorldPoint(pos);
		transform.position = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), transform.position.z);
		if (Input.GetMouseButtonDown(0))
			Debug.Log("Clicked on X: " + Mathf.RoundToInt(pos.x) + ", Y: " + Mathf.RoundToInt(pos.y));
	}

	// When added to an object, draws colored rays from the
    // transform position.

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Base : MonoBehaviour
{
	public Vector2 sizeOnGrid;
    public bool isBuilt = false; // REVIEW private me
	public Color blueprintColor;
	public Color wrongColor;
	private SpriteRenderer sr;
	private bool colPresent = false;
	private Camera cam;

	void Start() {
		sr = GetComponent<SpriteRenderer>();
		sr.color = blueprintColor;
		sr.sortingOrder = 500;
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	public virtual void OnBuild() {
		isBuilt = true;
		sr.sortingOrder = 0;
		sr.color = Color.white;
		Debug.Log("A tower has been built!");
	}

	public virtual bool tryBuild() {
		if (!colPresent && !isBuilt) {
			OnBuild();
			return (true);
		} else {
			return (false);
		}
	}

	void preBuiltUpdate() {
		Vector3 pos = Input.mousePosition;
		pos = cam.ScreenToWorldPoint(pos);
		pos = new Vector3(sizeOnGrid.x % 2 == 1 ? Mathf.Round(pos.x) : Mathf.Ceil(pos.x) - 0.5f, sizeOnGrid.y % 2 == 1 ? Mathf.Round(pos.y) : Mathf.Ceil(pos.y) - 0.5f, 0f);
		transform.position = pos;
		if (colPresent) {
			sr.color = wrongColor;
		} else {
			sr.color = blueprintColor;
		}
	}

	void Update() {
		if (!isBuilt) {
			preBuiltUpdate();
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.tag == "Tower" || col.tag == "Ground") {
			colPresent = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Tower" || col.tag == "Ground") {
			colPresent = false;
		}
	}
}

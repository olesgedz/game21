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
	private Collider2D myCol;

	public virtual void Start() {
		sr = GetComponent<SpriteRenderer>();
		sr.color = blueprintColor;
		sr.sortingOrder = 500;
		myCol = GetComponent<Collider2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	public virtual void OnBuild() {
		isBuilt = true;
		sr.sortingOrder = 0;
		sr.color = Color.white;
		myCol.isTrigger = false;
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

	void preBuildUpdate() {
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

	public virtual void Update() {
		if (!isBuilt) {
			preBuildUpdate();
		}
	}

	public virtual void OnTriggerStay2D(Collider2D col) {
		if (!isBuilt) {
			if (col.tag == "Tower" || col.tag == "Ground") {
				colPresent = true;
			}
		}
	}

	public virtual void OnTriggerExit2D(Collider2D col) {
		if (!isBuilt) {
			if (col.tag == "Tower" || col.tag == "Ground") {
				colPresent = false;
			}
		}
	}
}

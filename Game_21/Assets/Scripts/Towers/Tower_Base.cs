using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower_Base : MonoBehaviour
{
	public Vector2 sizeOnGrid;
	public Color blueprintColor;
	public Color wrongColor;
	public float rpm;
	public float radius = 3f;
	public LayerMask nonbuildLayer;

    private bool isBuilt = false;
	private SpriteRenderer sr;
	private bool colPresent = false;
	private Camera cam;
	private Collider2D myCol;
	private List<GameObject> targetList;
	private GameObject target;
	private float timer;
	private CircleCollider2D cirCol;
<<<<<<< HEAD

=======
	
>>>>>>> jblack-b
	void Start() {
		sr = GetComponent<SpriteRenderer>();
		sr.color = blueprintColor;
		sr.sortingOrder = 500;
		myCol = GetComponent<Collider2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		targetList = new List<GameObject>();
	}

	void OnBuild() {
		isBuilt = true;
		sr.sortingOrder = 0;
		sr.color = Color.white;
		myCol.enabled = false;
		transform.Find("Collider").GetComponent<BoxCollider2D>().enabled = true;
		cirCol = gameObject.AddComponent<CircleCollider2D>();
		cirCol.radius = radius;
		cirCol.isTrigger = true;
		timer = Time.time;
		GameObject.Find("Enemy Nexus").GetComponent<Test_Spawner>().buildingBuilt();
	}

	public bool tryBuild() {
		if (!colPresent && !isBuilt) {
			OnBuild();
			return true;
		} else {
			return false;
		}
	}

	void preBuildUpdate() {
		if (myCol.IsTouchingLayers(nonbuildLayer))
			colPresent = true;
		else
			colPresent = false;
		Vector3 pos = Input.mousePosition;
		pos = cam.ScreenToWorldPoint(pos);
		pos = new Vector3(sizeOnGrid.x % 2 == 1 ? Mathf.Round(pos.x) : Mathf.Ceil(pos.x) - 0.5f, sizeOnGrid.y % 2 == 1 ? Mathf.Round(pos.y) : Mathf.Ceil(pos.y) - 0.5f, 0f);
		transform.position = pos;
		if (colPresent) {
			sr.color = wrongColor;
		} else {
			sr.color = blueprintColor;
	
<<<<<<< HEAD
		}
	}

	void postBuildUpdate() {
		if (isBuilt && Time.time > timer + 60 / rpm) {
			timer = Time.time;
			if (targetList.Count == 0) {
				return;
			}
			target = targetList[0];
			if (Shoot(target) <= 0) {
				targetList.Remove(target);
			}
		}
	}

=======
		}
	}

	void postBuildUpdate() {
		if (isBuilt && Time.time > timer + 60 / rpm) {
			timer = Time.time;
			if (targetList.Count == 0) {
				return;
			}
			target = targetList[0];
			if (Shoot(target) <= 0) {
				targetList.Remove(target);
			}
		}
	}

>>>>>>> jblack-b
	public virtual int Shoot(GameObject target) {
		int res = target.GetComponent<g_Health>().damage(1);
		return res;
	}

	void Update() {
		if (!isBuilt) {
			preBuildUpdate();
		}
		if (isBuilt) {
			postBuildUpdate();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (isBuilt) {
			if (col.tag == "Enemy") {
				targetList.Add(col.gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (isBuilt) {
			if (col.tag == "Enemy") {
				targetList.Remove(col.gameObject);
			}
		}
	}
}

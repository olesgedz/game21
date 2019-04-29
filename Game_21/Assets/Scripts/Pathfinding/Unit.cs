using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	private Transform target;
	[SerializeField]float speed = 20;
	Vector3[] path;
	int targetIndex;
	Rigidbody2D rb;
	float waypointMargin = 0.08f;
	public bool dead = false;

	public void ReCalcPath() {
		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}

	void Start() {
		target = GameObject.Find("Nexus").transform;
		ReCalcPath();
		rb = GetComponent<Rigidbody2D>();
	}

	void Movement(Vector3 wp) {
		if (!dead) {
			Vector3 dir = wp - transform.position;
			rb.velocity = dir.normalized * speed;
		} else {
			rb.velocity = Vector3.zero;
		}
	}

	void MovementEnd() {
		rb.velocity = Vector3.zero;
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (Vector3.Distance(transform.position, currentWaypoint) <= waypointMargin) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					MovementEnd();
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			Movement(currentWaypoint);
			// transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i - 1],path[i]);
				}
			}
		}
	}
}
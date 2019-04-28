using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
	public GameObject nexus;
	public float velocity = 3f;
	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		Vector3 dir = nexus.transform.position - transform.position;
		rb.velocity = dir.normalized * velocity;
	}
}

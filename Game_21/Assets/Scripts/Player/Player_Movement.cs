using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
	public float velocity = 5f;
	private Rigidbody2D rb;
	enum direction {
		top,
		top_left,
		top_right,
		left,
		right,
		bottom_right,
		bottom_left,
		buttom
	}

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

    void Update() {
		Vector2 dir = Vector2.zero;
		if (Input.GetKey(KeyCode.D))
			dir.x++;
		if (Input.GetKey(KeyCode.A))
			dir.x--;
		if (Input.GetKey(KeyCode.W))
			dir.y++;
		if (Input.GetKey(KeyCode.S))
			dir.y--;
		rb.velocity = dir.normalized * velocity;
	}
}

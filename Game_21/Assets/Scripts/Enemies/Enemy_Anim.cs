using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Anim : MonoBehaviour
{
    private Rigidbody2D rb;
	private Animator anim;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update() {
		int dir = 0;
		if (rb.velocity.normalized.x > 0.2f) {
			dir += 1;
		}
		if (rb.velocity.normalized.x < -0.2f) {
			dir += 2;
		}
		if (rb.velocity.normalized.y > 0.2f) {
			dir += 4;
		}
		if (rb.velocity.normalized.y < -0.2f) {
			dir += 8;
		}
		anim.SetInteger("Direction", dir);
	}

	public void deathAnim() {
		anim.SetBool("Dead", true);
		GetComponent<Unit>().dead = true;
	}
}

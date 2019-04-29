using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : g_Health
{
	public float deathTimer = 1f;

	IEnumerator waitForDespawn() {
		yield return new WaitForSeconds(deathTimer);
		Destroy(gameObject);
	}

    public override void death() {
<<<<<<< HEAD
		GetComponent<Enemy_Anim>().deathAnim();
		GetComponent<Unit>().dead = true;
		GetComponent<Collider2D>().enabled = false;
		StartCoroutine("waitForDespawn");
=======
		// GetComponent<Enemy_Anim>().deathAnim();
		// GetComponent<Unit>().dead = true;

		// GetComponent<Collider2D>().enabled = false;
		// StartCoroutine("waitForDespawn");
>>>>>>> jblack-b
		// Destroy(gameObject);
	}
}

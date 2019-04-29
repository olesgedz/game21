using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : g_Health {
	
	[SerializeField]
	private int healthPoints;
	[SerializeField]
	private int rewardAmt;
	[SerializeField]
	private Transform exitPoint;
	
	[SerializeField]
	private float navigationUpdate;
	[SerializeField]
	private Animator anim;
	private int target = 0;
	private Transform enemy;
	private Collider2D enemyCollider;
	private float navigationTime = 0;
	private bool isDead = false; 

	public bool IsDead {
		get {
			return isDead;
		}
	}

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform> ();
		anim = GetComponent<Animator>();	
		enemyCollider = GetComponent<Collider2D>();
		GameManager.Instance.RegisterEnemy(this);	
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "WayPoint")
			target += 1;
		else if (other.tag == "Finish") {
			GameManager.Instance.TotalEscaped += 1;
			GameManager.Instance.RoundEscaped += 1;
			GameManager.Instance.UnRegister(this);
			GameManager.Instance.isWaveOver();
		} else if (other.tag == "Projectile") {
			Projectile newP = other.gameObject.GetComponent<Projectile>();
			enemyHit(newP.AttackStrength);
			Destroy(other.gameObject);
		}
	}

	public float deathTimer = 1f;

	IEnumerator waitForDespawn() {
		yield return new WaitForSeconds(deathTimer);
		//Destroy(gameObject);
	}

    public override void death() {
		GetComponent<Enemy_Anim>().deathAnim();
		GetComponent<Unit>().dead = true;
		GetComponent<Collider2D>().enabled = false;
		isDead = true;
		// StartCoroutine("waitForDespawn");
		// Destroy(gameObject);
	}

	public void enemyHit(int hitPoints) {
		Debug.Log(healthPoints);
		if (healthPoints - hitPoints > 0) {
			//GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
			healthPoints -= hitPoints;
		} else {
			//anim.SetBool("Dead", true);
			death();
		}
	}

	public void die() {
		// Destroy(this.gameObject);
		//  isDead = true;
		// anim.SetTrigger("didDie");
		// GameManager.Instance.TotalKilled += 1;
		// enemyCollider.enabled = false;
		// GameManager.Instance.addMoney(rewardAmt);
		// GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Die);
		// GameManager.Instance.isWaveOver();
		
	}
}

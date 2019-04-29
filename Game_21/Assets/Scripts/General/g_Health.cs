using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_Health : MonoBehaviour
{
	public int maxHealth;
    public int currentHealth;

	public virtual void death() {
		Debug.Log(transform.name + "should die");
	}

	public int heal(int ammount) {
		currentHealth += ammount;
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		return (currentHealth);
	}

	public int damage(int ammount) {
		currentHealth -= ammount;
		if (currentHealth <= 0) {
			currentHealth = 0;
			death();
		}
		return (currentHealth);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : g_Health
{
    public override void death() {
		Debug.Log("Player should die");
	}
}

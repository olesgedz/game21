using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : g_Health
{
    public override void death() {
		Destroy(gameObject);
	}
}

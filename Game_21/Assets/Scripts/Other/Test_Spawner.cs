using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Spawner : MonoBehaviour
{
	public Vector3 spawnPoint;
	public GameObject enemyPrefab;
	public float frequency;
	private float timer;

	void Update() {
		if (Time.time > timer + frequency) {
			Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
			timer = Time.time;
		}
	}
}

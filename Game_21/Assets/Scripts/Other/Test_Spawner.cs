using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Spawner : MonoBehaviour
{
	public Vector3 spawnPoint;
	public GameObject enemyPrefab;
	public float frequency;
	private float timer;
	private bool abc = true;
	private Grid gr;
	private List<GameObject> family;

	void Start() {
		gr = GetComponent<Grid>();
		family = new List<GameObject>();
	}

	void Update() {
		if (Time.time > timer + frequency && abc) {
			abc = false;
			GameObject temp = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
			family.Add(temp);
			timer = Time.time;
		}
	}

	public void removeMe(GameObject woof) {
		family.Remove(woof);
	}

	public void buildingBuilt() {
		gr.CreateGrid();
		foreach (GameObject broda in family) {
			broda.GetComponent<Unit>().ReCalcPath(); // REVIEW possible bottleneck
		}
	}
}

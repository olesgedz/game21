using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BuildMenu : MonoBehaviour
{
    public bool menuOpen = false;
	public Grid_Render g_r;
	public GameObject towerPrefab;
	private GameObject currentTower;

	void Update() {
		if (menuOpen && Input.GetMouseButtonDown(0) && currentTower) {
			bool res = currentTower.GetComponent<Tower_Base>().tryBuild();
			if (res)
				currentTower = null;
		}
		if (menuOpen && Input.GetMouseButtonDown(0) && !currentTower) {
			currentTower = Instantiate(towerPrefab);
		}
		if (menuOpen && Input.GetKeyDown(KeyCode.Alpha1)) {
			menuOpen = false;
			g_r.setGrid(false);
		} else if (!menuOpen && Input.GetKeyDown(KeyCode.Alpha1)) {
			menuOpen = true;
			g_r.setGrid(true);
		}
	}
}

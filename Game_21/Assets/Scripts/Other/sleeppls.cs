using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleeppls : MonoBehaviour
{
    public bool yes = true;

	void Start() {
		gameObject.SetActive(!yes);
	}
}

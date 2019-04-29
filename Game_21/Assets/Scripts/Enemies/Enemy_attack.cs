using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack : MonoBehaviour
{
    public int attack;
    public GameObject GG;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(GG.transform.position, transform.position) < radius)
        {
            GG.GetComponent<Player_Health>().damage(attack);
        }
    }
}

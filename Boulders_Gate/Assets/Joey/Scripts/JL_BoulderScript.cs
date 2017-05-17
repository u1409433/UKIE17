using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_BoulderScript : MonoBehaviour
{
    Rigidbody RB;
    JL_LevelManager SC_LevelManager;

    // Use this for initialization
    void Start()
    {
        SC_LevelManager = GameObject.Find("LevelManager").GetComponent<JL_LevelManager>();

        RB = gameObject.GetComponent<Rigidbody>();

        RB.AddForce(transform.forward * 300 * SC_LevelManager.FL_Power);
        RB.AddForce(transform.up * 300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider vCollided)
    {
        if (vCollided.transform.tag == "CollisionPlane")
        {
            Invoke("Die", 3);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

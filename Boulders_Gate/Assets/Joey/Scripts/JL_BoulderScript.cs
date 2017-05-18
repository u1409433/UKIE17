using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_BoulderScript : MonoBehaviour
{
    public bool bl_prediction;
    Rigidbody RB;
    JL_LevelManager SC_LevelManager;

    // Use this for initialization
    void Start()
    {
        SC_LevelManager = GameObject.Find("LevelManager").GetComponent<JL_LevelManager>();

        RB = gameObject.GetComponent<Rigidbody>();
        RB.AddForce(transform.up * 600 * SC_LevelManager.FL_Power);
        RB.AddForce(-transform.forward * 75 * SC_LevelManager.FL_Power);
        if (bl_prediction)
            Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider vCollided)
    {
        if (vCollided.transform.tag == "CollisionPlane")
        {
            if (!bl_prediction)
                Invoke("Die", 3);
            else
            {
                transform.GetChild(0).parent = null;
                Destroy(gameObject);
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

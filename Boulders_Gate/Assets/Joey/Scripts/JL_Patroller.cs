using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_Patroller : MonoBehaviour
{
    public bool BL_MoveRight = true;
    private JL_LevelManager SC_LevelManager;
    // Use this for initialization
    void Start()
    {
        SC_LevelManager = GameObject.Find("LevelManager").GetComponent<JL_LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BL_MoveRight)
        {
            transform.Translate(0.1f, 0, 0);
            if (transform.position.x > 20) BL_MoveRight = false;
        }
        else
        {
            transform.Translate(-0.1f, 0, 0);
            if (transform.position.x < -20) BL_MoveRight = true;
        }
    }

    public void OnCollisionEnter(Collision vCollision)
    {
        if (vCollision.transform.tag == "Boulder")
        {
            switch (gameObject.name)
            {
                case "Patroller":
                    SC_LevelManager.ST_Powerup = "Triple Shot";
                    break;
                case "Drone":
                    SC_LevelManager.ST_Powerup = "Big Shot";
                    Invoke("SummonDrones", 0);
                    break;
                default:
                    break;
            }
            SC_LevelManager.BL_PoweredUP = true;
            Destroy(gameObject);
        }

        if (vCollision.transform.tag == "Block")
        {
            Destroy(gameObject);
        }
    }

    public void SummonDrones()
    {
        for (int i = 0; i < 3; i++)
        {
            //Random random = new Random.Range(0, 3);
        }
    }
}

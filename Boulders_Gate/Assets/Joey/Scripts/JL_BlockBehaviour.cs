using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_BlockBehaviour : MonoBehaviour
{
    public Vector3 v3_CurrentPos;
    public bool BL_Moving;

    public float FL_NextCheck;

    public GameObject Explosion;

    // Use this for initialization
    void Start()
    {
        v3_CurrentPos = gameObject.transform.position;

        InvokeRepeating("MovedCheck", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (BL_Moving && FL_NextCheck < Time.time)
        {
            DeleteCheck();
        }
    }

    public void MovedCheck()
    {
        if (Vector3.Distance(v3_CurrentPos, transform.position) > 1f)
        {

            CancelInvoke();
            v3_CurrentPos = transform.position;
            Invoke("MovingTrue", 3);
        }
        else
        {
            v3_CurrentPos = transform.position;
        }
    }
    void MovingTrue()
    {
        BL_Moving = true;
    }

    public void DeleteCheck()
    {
        if (Vector3.Distance(transform.position, v3_CurrentPos) < 0.5f)
        {
            transform.GetComponentInParent<JL_BuildingBehaviour>().IN_Bricks--;
            float tFL_PingChance = Random.Range(0, 6);
            if (tFL_PingChance == 0)
            {
                GameObject.Find("AudioManager").GetComponent<JL_AudioManager>().Coin();
            }

            switch (gameObject.name)
            {
                case "Trump":
                    Instantiate(Explosion, transform.position, transform.rotation);
                    Destroy(gameObject);
                    break;
                case "Tan Tank":
                    Instantiate(Explosion, transform.position, transform.rotation);
                    Destroy(gameObject);
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
            
        }
        else
        {
            v3_CurrentPos = transform.position;
            float tFL_timer = Random.Range(1, 4);
            float tFL_timer2 = Random.Range(1, 10);
            float tFL_NextCheck = tFL_timer + tFL_timer2 / 10;
            FL_NextCheck = Time.time + tFL_NextCheck;
        }
    }
}

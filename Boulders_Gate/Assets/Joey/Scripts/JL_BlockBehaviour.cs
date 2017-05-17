using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_BlockBehaviour : MonoBehaviour
{
    public Vector3 v3_CurrentPos;
    public bool BL_Moving;

    // Use this for initialization
    void Start()
    {
        v3_CurrentPos = gameObject.transform.position;

        InvokeRepeating("MovedCheck", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //MovedCheck();
    }

    public void MovedCheck()
    {
        if (Vector3.Distance(v3_CurrentPos, transform.position) > 1f)
        {
            //BL_Moving = true;
            CancelInvoke();
            v3_CurrentPos = transform.position;
            InvokeRepeating("DeleteCheck", 3, 3);
        }
        else
        {
            v3_CurrentPos = transform.position;
        }
    }

    public void DeleteCheck()
    {
        if (Vector3.Distance(transform.position, v3_CurrentPos) < 0.5f)
        {
            transform.GetComponentInParent<JL_BuildingBehaviour>().IN_Bricks--;
            Destroy(gameObject);
        }
        else
        {
            v3_CurrentPos = transform.position;
        }
    }
}

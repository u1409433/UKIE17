using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Drone_Spawner : MonoBehaviour {
    //----------------------Variables and Declarations--------------------------

    public int in_Drone_Count;

    public GameObject GO_Drone;
    

    //--------------------------------------------------------------------------
	// Use this for initialization
	void Start () {

        in_Drone_Count = 0;
		
	}//-----
	//--------------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {

	}//-----
    //--------------------------------------------------------------------------
    //-Spawn Drones
    public void SummonDrones()
    {
        if (in_Drone_Count >=1)
        {
                Instantiate(GO_Drone, transform.position + transform.TransformDirection(Vector3.up / 2), transform.rotation);
                in_Drone_Count--;
                Invoke("SummonDrones",5);
        }
        
    }//-----
    //-------------------------------------------------------------------------
}//=====

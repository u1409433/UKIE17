using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_BuildingBehaviour : MonoBehaviour
{
    public int IN_StartingBricks;
    public int IN_Bricks;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("LevelManager").GetComponent<JL_LevelManager>().IN_BlocksLeft += IN_Bricks;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.name == "Roof")
        {
            if (IN_Bricks <= IN_StartingBricks / 3)
            {

            }
        }
    }
}

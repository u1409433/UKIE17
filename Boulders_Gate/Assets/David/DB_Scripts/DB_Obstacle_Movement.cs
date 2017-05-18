using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Obstacle_Movement : MonoBehaviour {
    //---------Variables-----------------------------------

    private Vector3 V3_Left_Target;
    private Vector3 V3_Left_Turnpoint;
    private Vector3 V3_Right_Target;
    private Vector3 V3_Right_Turnpoint; //These are the points in the game world that the Obstacle will move towards (Target) and turn around at (turnpoint)

    private int in_Length_Count; //How many lengths the Obstacle has made (Only referred to, seeing it is unnecessary)
    public int in_Pattern_Length; //How many lengths in the Obstacle's movement pattern - Manually set
    public int in_Pattern_Delay_Time; //The number of seconds between each pattern run - Set to 0 for an infinite pattern

    public float fl_Speed; //The Obstacle's move speed
    public float fl_Initial_Time_Delay; //Upon starting the level, how long until the Obstacle begins its first pattern
    private float fl_Pattern_Delay; //The countdown until the Obstacle begins another pattern run (Only referred to, seeing it is unnecessary)

    public float fl_Time_Till_Destroyed; //The time until the Obstacle is removed from the scene after ragdolling

    public float fl_LeftOutOfView_Target; //The Left point outside the Camera's field of view the Obstacle aims for
    public float fl_LeftOutOfView_Turnpoint; //The Left point outside the Camera's field of view, but before the target, that the Obstacle will turn around at
    public float fl_RightOutOfView_Target; //The Right point outside the Camera's field of view the Obstacle aims for
    public float fl_RightOutOfView_Turnpoint; //The Right point outside the Camera's field of view, but before the target, that the Obstacle will turn around at

    public JL_LevelManager SC_LevelManager;

    private bool bl_Action; //Controls which behaviour the Obstacle will take on update
    private bool bl_MoveDir; //Controls the direction the Obstacle moves in
    private bool bl_Pattern_End; //Indicates that the pattern has ended and a delay has begun

    private Rigidbody rb_Obstacle;

    //------------------------------------------------------
	// - Use this for initialization
	void Start () 
    {

        bl_Action = false;
        bl_MoveDir = false;
        bl_Pattern_End = false;

        rb_Obstacle = GetComponent<Rigidbody>(); // Find the Rigidbody

        //Set the parameters of the Targets and Turnarounds. The y and z values are the same as those the Obstacle has when placed in the scene, determined on startup. The X value can be manually set.
        V3_Left_Target.x = fl_LeftOutOfView_Target;
        V3_Left_Target.y = transform.position.y;
        V3_Left_Target.z = transform.position.z;
        V3_Left_Turnpoint.x = fl_LeftOutOfView_Turnpoint;
        V3_Left_Turnpoint.y = transform.position.y;
        V3_Left_Turnpoint.z = transform.position.z;
        V3_Right_Target.x = fl_RightOutOfView_Target;
        V3_Right_Target.y = transform.position.y;
        V3_Right_Target.z = transform.position.z;
        V3_Right_Turnpoint.x = fl_RightOutOfView_Turnpoint;
        V3_Right_Turnpoint.y = transform.position.y;
        V3_Right_Turnpoint.z = transform.position.z;

        
	}//-----
    //-----------------------------------------------------	
	// - Update is called once per frame
	void Update () 
    {

        fl_Initial_Time_Delay -= Time.deltaTime; //Begin the initial countdown

        if (bl_Pattern_End == true) //If the Obstacle has finished its pattern, begin the countdown until it begins again
        {
            fl_Pattern_Delay -= Time.deltaTime;
        }
        else //Otherwise, do nothing
        {

        }

        if (fl_Initial_Time_Delay <= 0) //If the timer reaches 0, stay at 0
        {
            fl_Initial_Time_Delay = 0;
        }

        if (fl_Pattern_Delay <= 0) //If the timer reaches 0, stay at 0
        {
            fl_Pattern_Delay = 0;
        }

        if (fl_Time_Till_Destroyed <= 0) //If the timer reaches 0, stay at 0
        {
            fl_Time_Till_Destroyed = 0;
        }

        if (bl_Action == false) //If the boolean is false, run this method
        {
            Movement();
        }
        if (bl_Action == true) //If the boolean is true, run this method
        {
            Falling();
        }
	}//-----
    //-----------------------------------------------------
    // - Collision Behaviour
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Boulder") //If the Obstacle is hit by a boulder, set the action boolean to true, making the Obstacle fall
        {
            bl_Action = true;

             switch (gameObject.name)
            {
                case "Patroller":
                    SC_LevelManager.ST_Powerup = "Triple Shot";
                    break;
                case "Drone":
                    SC_LevelManager.ST_Powerup = "Big Shot";
                    Invoke("SummonDrones", 0);
                    break;
                 case "SummonArmySupport":
                    SC_LevelManager.ST_Powerup = "Bounce Shot";
                    Invoke("SummonArmySupport", 0);
                    break;
                 case "FighterJet":
                    SC_LevelManager.ST_Powerup = "Fast Fire";
                    break;
                 case "Bomber Plane":
                    SC_LevelManager.ST_Powerup = "Explosive Shot";
                    break;
                 case "Chinook":
                    SC_LevelManager.ST_Powerup = "More Ammo";
                    break;
                default:
                    break;
            }
            SC_LevelManager.BL_PoweredUP = true;
        }

    }//-----
    //-----------------------------------------------------
    // - Movement Behaviour
    void Movement()
    {

        if (fl_Initial_Time_Delay == 0 && fl_Pattern_Delay == 0) //If all countdown timers are at 0
        {

            bl_Pattern_End = false; //Pattern begins again

            if (bl_MoveDir == false) //If the move bool is false, move to the right
            {
                transform.position = Vector3.MoveTowards(transform.position, V3_Right_Target, fl_Speed);
                if (transform.position.x > V3_Right_Turnpoint.x) //If the Obstacle leaves the field of view and reaches the turn point, raise the length count by 1 and turn the Move bool on
                {
                    in_Length_Count++;
                    bl_MoveDir = true;
                }
            }

            if (bl_MoveDir == true) //If the move bool is true, move to the left
            {
                transform.position = Vector3.MoveTowards(transform.position, V3_Left_Target, fl_Speed);
                if (transform.position.x < V3_Left_Turnpoint.x) //If the Obstacle leaves the field of view and reaches the turn point, raise the length count by 1 and turn the Move bool off
                {
                    in_Length_Count++;
                    bl_MoveDir = false;
                }
            }
            if (in_Length_Count == in_Pattern_Length) //If the number of lengths travelled is the same as the manually set pattern length, signal that the pattern has finished, set the length counter to 0, and begin the pattern delay timer.
            {
                fl_Pattern_Delay = in_Pattern_Delay_Time;
                bl_Pattern_End = true;
                in_Length_Count = 0;
            }
        }

    }//-----
    //-----------------------------------------------------
    // - Falling Behaviour
    void Falling()
    {
        rb_Obstacle.isKinematic = false; //Turn off kinematics
        fl_Time_Till_Destroyed -= Time.deltaTime; //Begin death timer

        if (fl_Time_Till_Destroyed == 0) //If death timer is at 0, destroy self
        {
            Destroy(gameObject);
        }

    }//-----
    //------------------------------------------------------

    //HOLDING CELL
    //public void OnCollisionEnter(Collision vCollision)
    //{
    //    if (vCollision.transform.tag == "Boulder")
    //    {
    //        switch (gameObject.name)
    //        {
    //            case "Patroller":
    //                SC_LevelManager.ST_Powerup = "Triple Shot";
    //                break;
    //            case "Drone":
    //                SC_LevelManager.ST_Powerup = "Big Shot";
    //                Invoke("SummonDrones", 0);
    //                break;
    //            default:
    //                break;
    //        }
    //        SC_LevelManager.BL_PoweredUP = true;
    //        Destroy(gameObject);
    //    }

    //    if (vCollision.transform.tag == "Block")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}//=====

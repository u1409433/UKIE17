using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_LevelManager : MonoBehaviour
{
    public GameObject PF_Boulder;

    public GameObject GO_Cannon;

    public float FL_YRot;
    public bool BL_Ypos = true;

    public float FL_ZRot;
    public bool BL_Zpos = true;

    public float FL_Power;
    public bool BL_PowerPos;

    public float FL_FiringTime;
    public float FL_Cooldown;
    public float FL_ShotsLeft;

    public bool BL_PoweredUP = false;
    public string ST_Powerup;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();

        MoveCannon();
    }

    private void InputCheck()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= FL_FiringTime && FL_ShotsLeft > 0)
        {
            if (BL_PoweredUP)
            {
                switch (ST_Powerup)
                {
                    case "Triple Shot":
                        Invoke("Fire", 0.1f);
                        Invoke("Fire", 0.3f);
                        Invoke("Fire", 0.5f);
                        break;
                    case "Big Shot":
                        GameObject BigBall = (GameObject)Instantiate(PF_Boulder, GO_Cannon.transform.position + new Vector3(0, 2, 3), GO_Cannon.transform.rotation);
                        BigBall.transform.localScale = new Vector3(3,3,3);
                        break;
                    default:
                        break;
                }

                FL_FiringTime += FL_Cooldown;
                FL_ShotsLeft--;
                BL_PoweredUP = false;
            }
            else
            {
                Invoke("Fire", 0);
                FL_FiringTime += FL_Cooldown;
                FL_ShotsLeft--;
            }

        }

        if (Input.GetMouseButton(0))
        {
            UpdatePower();
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray tRY_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit tRH_Hit;

        //    if (Physics.Raycast(tRY_Ray, out tRH_Hit, 100f))
        //    {
        //        if (tRH_Hit.transform.tag == "Block")
        //        {
        //            Blocksplosion(tRH_Hit.transform.gameObject);
        //        }
        //    }
        //}
    }

    public void Fire()
    {
        Instantiate(PF_Boulder, GO_Cannon.transform.position + new Vector3(0, 2, 3), GO_Cannon.transform.rotation);
    }

    public void Blocksplosion(GameObject vHitBlock)
    {
        //For each block that exists
        foreach (GameObject Block in GameObject.FindGameObjectsWithTag("Block"))
        {
            if (Vector3.Distance(vHitBlock.transform.position, Block.transform.position) < 3f)
            {
                Destroy(Block);
            }
        }

        Destroy(vHitBlock);
    }

    private void MoveCannon()
    {
        if (BL_Ypos)
        {
            GO_Cannon.transform.Rotate(new Vector3(0, 0.5f, 0));
            if (GO_Cannon.transform.rotation.eulerAngles.y > 25 && GO_Cannon.transform.rotation.eulerAngles.y < 30) BL_Ypos = false;
        }
        else
        {
            GO_Cannon.transform.Rotate(new Vector3(0, -0.5f, 0));
            if (GO_Cannon.transform.rotation.eulerAngles.y > 330 && GO_Cannon.transform.rotation.eulerAngles.y < 335) BL_Ypos = true;
        }

        FL_YRot = GO_Cannon.transform.rotation.eulerAngles.y;
        //GO_Cannon.transform.Rotate(0, FL_YRot, 0);
    }

    private void UpdatePower()
    {
        if (BL_PowerPos)
        {
            FL_Power += Time.deltaTime * 3;
            if (FL_Power >= 6) BL_PowerPos = false;
        }
        else
        {
            FL_Power -= Time.deltaTime * 3;
            if (FL_Power <= 1) BL_PowerPos = true;
        }
    }
}

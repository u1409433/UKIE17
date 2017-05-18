using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_LevelManager : MonoBehaviour
{
    public GameObject PF_Boulder;
    public GameObject PF_Boulder_Predict;

    public GameObject GO_Cannon;
    public GameObject GO_Cam;
    private float fl_FOV_start;
    private float fl_FOV_current;
    private float fl_Cam_lerp_progress;
    private float fl_Cam_lerp_rate = 15;

    public float FL_YRot;
    public bool BL_Ypos = true;

    public float FL_ZRot;
    public bool BL_Zpos = true;

    public float Ymin;
    public float Ymax;

    public float FL_Power;
    public bool BL_PowerPos;

    public float FL_FiringTime;
    public float FL_FiringTime_Predict;
    public float FL_Cooldown;
    public float FL_ShotsLeft;

    public bool BL_PoweredUP = false;
    public string ST_Powerup;

    public float FL_MousePos;

    public int IN_BuildingsLeft;
    public int IN_BlocksLeft;
    public Dictionary<GameObject, int> DI_Structures;

    static public bool BL_CamRotation = false;

    // Use this for initialization
    void Start()
    {
        DI_Structures = new Dictionary<GameObject, int>();
        fl_FOV_start = Camera.main.fieldOfView;
        foreach (GameObject Structure in GameObject.FindGameObjectsWithTag("Building"))
        {
            CountBricks(Structure);
            IN_BuildingsLeft++;
        }
    }

    // Update is called once per frame
    void Update()
    {
            Fire_Predict();
        FL_MousePos = Input.mousePosition.y;

        InputCheck();

        MoveCannon();

        IN_BlocksLeft = 0;

        Debug.Log(BL_CamRotation.ToString());

        foreach (GameObject Structure in GameObject.FindGameObjectsWithTag("Building"))
        {
            if (Structure.GetComponent<JL_BuildingBehaviour>().IN_Bricks < Structure.GetComponent<JL_BuildingBehaviour>().IN_StartingBricks / 2)
            {
                IN_BuildingsLeft--;
                Debug.Log("Building Destroyed");
            }
            else
            {
                IN_BlocksLeft += Structure.GetComponent<JL_BuildingBehaviour>().IN_Bricks;
            }
        }

        if (IN_BuildingsLeft <= 0)
        {
            Application.LoadLevel(2);
        }
    }

    private void InputCheck()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= FL_FiringTime && FL_ShotsLeft > 0)
        {
            StartCoroutine(Camera_Shake());
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
                        GameObject BigBall = (GameObject)Instantiate(PF_Boulder, GO_Cannon.transform.position + new Vector3(0, 2, 3), GO_Cannon.transform.FindChild("Barrel").transform.rotation);
                        BigBall.transform.localScale = new Vector3(3, 3, 3);
                        BigBall.GetComponent<Rigidbody>().AddForce(transform.up * 500);
                        break;
                    default:
                        break;
                }

                FL_FiringTime = Time.time + FL_Cooldown;
                FL_ShotsLeft--;
                BL_PoweredUP = false;
            }
            else
            {
                Invoke("Fire", 0);
                FL_FiringTime = Time.time + FL_Cooldown;
                FL_ShotsLeft--;

                if (FL_ShotsLeft <= 0)
                {
                    Invoke("Loss", 10);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            UpdatePower();
        }
    }

    public void Fire()
    {
        Instantiate(PF_Boulder, GO_Cannon.transform.position + new Vector3(0, 2, 3), GO_Cannon.transform.FindChild("Barrel").transform.rotation);
    }

    void Fire_Predict()
    {
        FL_FiringTime_Predict = Time.time + 1;
        Instantiate(PF_Boulder_Predict, GO_Cannon.transform.position + new Vector3(0, 2, 3), GO_Cannon.transform.FindChild("Barrel").transform.rotation);
    }

    IEnumerator Camera_Shake()
    {
        fl_Cam_lerp_progress = 0;
        fl_FOV_current = fl_FOV_start;
        while ((fl_FOV_start * 1.1f) > fl_FOV_current)
        {
            fl_Cam_lerp_progress += Time.deltaTime * fl_Cam_lerp_rate;
            fl_FOV_current = Camera.main.fieldOfView = Mathf.LerpUnclamped(fl_FOV_start, (fl_FOV_start * 1.1f), fl_Cam_lerp_progress);
            yield return null;
        }
        fl_Cam_lerp_progress = 0;
        while (fl_FOV_current > fl_FOV_start)
        {
            fl_Cam_lerp_progress += Time.deltaTime * fl_Cam_lerp_rate;
            fl_FOV_current = Camera.main.fieldOfView = Mathf.Lerp((fl_FOV_start * 1.1f), fl_FOV_start, fl_Cam_lerp_progress);
            yield return null;
        }
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
            GO_Cannon.transform.Rotate(new Vector3(0, 0.25f, 0));
            if (BL_CamRotation) GO_Cam.transform.Rotate(new Vector3(0, 0.25f, 0));
            if (GO_Cannon.transform.rotation.eulerAngles.y > 35 && GO_Cannon.transform.rotation.eulerAngles.y < 40) BL_Ypos = false;
        }
        else
        {
            GO_Cannon.transform.Rotate(new Vector3(0, -0.25f, 0));
            if (BL_CamRotation) GO_Cam.transform.Rotate(new Vector3(0, -0.25f, 0));
            if (GO_Cannon.transform.rotation.eulerAngles.y > 320 && GO_Cannon.transform.rotation.eulerAngles.y < 325) BL_Ypos = true;
        }

        FL_YRot = GO_Cannon.transform.rotation.eulerAngles.y;


        FL_ZRot = Input.GetAxis("Mouse Y") * Time.deltaTime * 50;
        GO_Cannon.transform.FindChild("Barrel").transform.Rotate(-FL_ZRot, 0, 0);

        if (GO_Cannon.transform.FindChild("Barrel").transform.localRotation.eulerAngles.y > 90)
        {
            GO_Cannon.transform.FindChild("Barrel").transform.Rotate(FL_ZRot, 0, 0);
        }

        if (GO_Cannon.transform.FindChild("Barrel").transform.localRotation.eulerAngles.x > 350 && GO_Cannon.transform.FindChild("Barrel").transform.localRotation.eulerAngles.x < 360)
        {
            GO_Cannon.transform.FindChild("Barrel").transform.Rotate(FL_ZRot, 0, 0);
        }

        FL_ZRot = GO_Cannon.transform.FindChild("Barrel").transform.rotation.eulerAngles.x;

        
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

    private void CountBricks(GameObject vStructure)
    {
        int tIN = vStructure.transform.childCount;
        DI_Structures.Add(vStructure, tIN);
    }

    private void Loss()
    {
        Application.LoadLevel(3);
    }

    public void SwitchCamRot()
    {
        BL_CamRotation = (BL_CamRotation) ? false : true;
    }

    public string GetRot()
    {
        return BL_CamRotation.ToString();
    }
}

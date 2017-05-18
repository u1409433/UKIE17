using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JL_MenuUIManager : MonoBehaviour
{
    public Text UI_CamRot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UI_CamRot.text = GameObject.Find("LevelManager").GetComponent<JL_LevelManager>().GetRot();
    }

    public void RestartButton()
    {
        Application.LoadLevel(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        Application.LoadLevel(1);
    }

    public void CamButton()
    {
        GameObject.Find("LevelManager").GetComponent<JL_LevelManager>().SwitchCamRot();
    }
}

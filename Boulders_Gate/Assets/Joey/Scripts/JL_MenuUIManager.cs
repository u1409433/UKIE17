using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_MenuUIManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartButton()
    {
        Application.LoadLevel(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

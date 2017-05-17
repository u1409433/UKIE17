using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JL_UIManager : MonoBehaviour
{
    public Text UI_ShotsLeft;
    public Slider UI_PowerSlider;

    private JL_LevelManager SC_LevelManager;

    // Use this for initialization
    void Start()
    {
        SC_LevelManager = GameObject.Find("LevelManager").GetComponent<JL_LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UI_ShotsLeft.text = SC_LevelManager.FL_ShotsLeft.ToString();
        UI_PowerSlider.value = SC_LevelManager.FL_Power;
    }
}

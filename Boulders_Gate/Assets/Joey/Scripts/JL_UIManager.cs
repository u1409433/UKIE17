using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JL_UIManager : MonoBehaviour
{
    public Text UI_ShotsLeft;
    public Slider UI_PowerSlider;

    public Text UI_BricksLeft;
    public Text UI_TotalBricks;

    private int IN_BrickstoDestroy;

    private JL_LevelManager SC_LevelManager;

    // Use this for initialization
    void Start()
    {
        SC_LevelManager = GameObject.Find("LevelManager").GetComponent<JL_LevelManager>();
        IN_BrickstoDestroy = SC_LevelManager.IN_BlocksLeft / 2;
        UI_TotalBricks.text = IN_BrickstoDestroy.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UI_ShotsLeft.text = SC_LevelManager.FL_ShotsLeft.ToString();
        UI_PowerSlider.value = SC_LevelManager.FL_Power;
        int BricksLeft = IN_BrickstoDestroy * 2 - SC_LevelManager.IN_BlocksLeft;
        UI_BricksLeft.text = BricksLeft.ToString();
    }

    public void ExitButton()
    {
        Application.LoadLevel(0);
    }
}

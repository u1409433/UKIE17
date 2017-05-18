using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_FloatingText : MonoBehaviour
{
    public int Number_To_Display;
    Vector3 Lerp_To;    // where it will lerp up to
    TextMesh TM;

    void Start()
    {
        TM = GetComponent<TextMesh>();
        TM.text = Number_To_Display.ToString();
        Lerp_To = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);    //where it will lerp to
        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);   //keeps it looking at the camera
        transform.localEulerAngles = new Vector3(0, 0, 0);  //change y value if rotation feels wrong
        transform.position = Vector3.Lerp(transform.position, Lerp_To, Time.deltaTime);
        float alpha = TM.color.a;
        alpha -= Time.deltaTime;
        TM.color = new Color(TM.color.r, TM.color.g, TM.color.b, alpha); //fades it away over time
    }
}

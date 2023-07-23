using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMap1_Manager : MonoBehaviour
{
    public static BuildMap1_Manager instance = null;

    public GameObject Canvas;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {       
        Dialogue_Manager.instance.Start_Dialogue("Intro");
    }
    public void BuildMap_Canvas_On()
    {
        // ºôµå¸Ê ³Ñ¹ö¿¡ µû¶ó Äµ¹ö½º ÄÁÆ®·Ñ
        Canvas.SetActive(true);
    }    
}

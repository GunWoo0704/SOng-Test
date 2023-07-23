using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BuildMap2_Manager : MonoBehaviour
{
    public static BuildMap2_Manager instance = null;

    public GameObject Canvas;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    
    public void BuildMap_Canvas_On()
    {
        // ºôµå¸Ê ³Ñ¹ö¿¡ µû¶ó Äµ¹ö½º ÄÁÆ®·Ñ
        Canvas.SetActive(true);
    }
}

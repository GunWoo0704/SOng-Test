using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Eye_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{    
    public void OnPointerDown(PointerEventData eventData)
    {        
        Quiz1_Manager.instance.Show_Full();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Quiz1_Manager.instance.Off_Full();
    }
}

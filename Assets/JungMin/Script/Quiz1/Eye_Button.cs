using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Eye_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool First_Click = false;

    public void OnPointerDown(PointerEventData eventData)
    {        
        Quiz1_Manager.instance.Show_Full();
        
        if(First_Click == false)
        {
            First_Click = true;
            Quiz1_Manager.instance.SetText(1);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Quiz1_Manager.instance.Off_Full();
    }
}

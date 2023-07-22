using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Touch_Input_Canvas : MonoBehaviour, IPointerClickHandler
{
    float touchInterval = 0.2f;
    float time = 0;
    // 상속 받아 dialogueManager를 설정해주어야 합니다.
    public bool canClick = true;
    public bool clicked = false;
    
    void Update()
    {
        time += Time.deltaTime;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("터캔 클릭");

        if (!canClick) return;
        if (time < touchInterval) return;

        time = 0;
        Dialogue_Manager.instance.isTouch = true;
        clicked = true;        
    }
}


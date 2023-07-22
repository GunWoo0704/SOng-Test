using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Touch_Canvas : MonoBehaviour, IPointerClickHandler
{
    float touchInterval = 0.2f;
    float time = 0;
    // 상속 받아 dialogueManager를 설정해주어야 합니다.
    public bool canClick = true;
    int count = 0;
    void Update()
    {
        time += Time.deltaTime;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick) return;
        if (time < touchInterval) return;

        time = 0;        
        Quiz3_Manager.instance.Maple_Ending(count);
        if (count >= 3)
            Destroy(this.gameObject);
        count++;
    }
}
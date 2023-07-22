using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Glass_Button : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{    
    Vector3 originPos;
    Vector3 mousePos;
    Vector3 objPos;

    bool isCollision = false;
    bool isLockedCollision = false;
    bool isClear = false;
    bool isLockedClear = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        isCollision = false;
        originPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.z = originPos.z;
        transform.position = objPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCollision == true && isClear == false)
        {
            isClear = true;
            //폴더 접촉했을 때 발생하는 상황 실행.
            Quiz3_Manager.instance.active_Maple_Canvas(true);
        }
        if (isLockedCollision == true && isLockedClear == false)
        {
            isLockedClear = true;

            // 락드 폴더 접촉했을 때, 발생하는 상황 실행.
            Quiz3_Manager.instance.active_Locked_Folder();
        }
        transform.position = originPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 폴더와 충돌했을 경우.
        if(collision.collider.CompareTag("Folder"))                    
            isCollision = true;
        // 잠긴 폴더와 충돌햇을 경우.
        else if (collision.collider.CompareTag("Locked"))
            isLockedCollision = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Folder"))        
            isCollision = false;
        else if (collision.collider.CompareTag("Locked"))
            isLockedCollision = false;
    }
}
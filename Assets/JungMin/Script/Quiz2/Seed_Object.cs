using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Seed_Object : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 originPos;
    Vector3 mousePos;
    Vector3 objPos;

    bool isCollision = false;        
    bool CanDrag = true;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CanDrag == false)
            return;
        isCollision = false;
        originPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CanDrag == false)
            return;
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.z = originPos.z;
        transform.position = objPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CanDrag == false)
            return;
        if (isCollision == true)
        {
            CanDrag = false;
            //폴더 접촉했을 때 발생하는 상황 실행.
            Quiz2_Manager.instance.Seed_Collide() ;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 폴더와 충돌했을 경우.
        if (collision.collider.CompareTag("Ground"))
            isCollision = true;                
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isCollision = false;        
    }
}

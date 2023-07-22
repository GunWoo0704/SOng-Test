using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Maple_Leaf_Object : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private GameObject Maple_Leaf;
    private Animator Maple_Leaf_Animator;

    Vector3 originPos;
    Vector3 mousePos;
    Vector3 objPos;

    bool canDrag = false;
    bool isCollision = false;
    bool isClear = false;
    private void Awake()
    {
        Maple_Leaf = this.gameObject;
        Maple_Leaf_Animator = this.gameObject.GetComponent<Animator>();
    }

    public void Stop_Anima()
    {
        Maple_Leaf_Animator.enabled = false;
        canDrag = true;
        Quiz3_Manager.instance.Active_Maple_Machine(true);
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if (canDrag == false)
            return;

        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.z = originPos.z;
        transform.position = objPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag == false)
            return;

        if(isCollision == true && isClear == false)
        {
            // 엔딩 연결
            Debug.Log("일단 엔딩 초입 성공");
            isClear = true;
            Quiz3_Manager.instance.Active_Touch_Canvas(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 폴더와 충돌했을 경우.
        if (collision.collider.CompareTag("Maple"))
            isCollision = true;        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Maple"))
            isCollision = false;        
    }
}

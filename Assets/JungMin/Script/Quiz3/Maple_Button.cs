using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Maple_Button : MonoBehaviour, IPointerClickHandler
{
    private GameObject MB;
    private Animator MB_Animator;

    bool canClick = false;
    private void Awake()
    {
        MB = this.gameObject;
        MB_Animator = this.gameObject.GetComponent<Animator>();
    }

    public void Stop_Anima()
    {
        MB_Animator.enabled = false;        
        // ���� ���̾�α� ����
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("��ǳ ��ư Ŭ��");
        if (canClick == true)
            return;

        canClick = false;
        MB_Animator.enabled = true;
    }

    public void Start_Anima()
    {
        if (canClick == true)
            return;

        canClick = false;
        MB_Animator.enabled = true;
    }
}

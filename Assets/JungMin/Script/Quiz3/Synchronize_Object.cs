using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synchronize_Object : MonoBehaviour
{
    private GameObject Synchronize;
    private Animator Synchronize_Animator;

    private void Awake()
    {
        Synchronize = this.gameObject;
        Synchronize_Animator = this.gameObject.GetComponent<Animator>();
    }

    public void Stop_Anima()
    {
        Synchronize_Animator.enabled = false;

        // 퀴3 매니저에서 동기화 버튼 끄고, 박스 3개 없에고 잠긴 박스 온
        Quiz3_Manager.instance.Synchronize_End();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Object : MonoBehaviour
{
    private GameObject Plant;
    private Animator Plant_Animator;
    
    private void Awake()
    {
        Plant = this.gameObject;
        Plant_Animator = this.gameObject.GetComponent<Animator>();
    }

    public void Stop_Anima()
    {
        Plant_Animator.enabled = false;
        // 엔딩 다이얼로그 연결
    }
}

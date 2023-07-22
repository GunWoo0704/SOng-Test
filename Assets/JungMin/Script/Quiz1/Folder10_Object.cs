using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder10_Object : MonoBehaviour
{
    private GameObject Folder10;
    private Animator Folder10_Animator;

    private void Awake()
    {
        Folder10 = this.gameObject;
        Folder10_Animator = this.gameObject.GetComponent<Animator>();
    }

    public void Stop_Anima()
    {
        Folder10_Animator.enabled = false;
        Folder10.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 240f);

        Quiz1_Manager.instance.SetText(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder5_Object : MonoBehaviour
{
    private GameObject Folder5;
    private Animator Folder5_Animator;

    private void Awake()
    {
        Folder5 = this.gameObject;
        Folder5_Animator = this.gameObject.GetComponent<Animator>();
    }

    public void Stop_Anima()
    {
        Folder5_Animator.enabled = false;
        Folder5.GetComponent<RectTransform>().anchoredPosition = new Vector2(320f, 240f);

    }
}

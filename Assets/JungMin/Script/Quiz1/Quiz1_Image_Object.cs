using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Quiz1_Image_Object : MonoBehaviour, IPointerClickHandler
{
    public bool isToggle = false;
    [SerializeField]
    Sprite Highlight_Folder_Sprite;
    Sprite Default_Sprite;

    Image This_Img;
    void Awake()
    {        
        This_Img = this.gameObject.GetComponent<Image>();
        Default_Sprite = This_Img.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isToggle = !isToggle;

        if(isToggle == true)        
            This_Img.sprite = Highlight_Folder_Sprite;
        else
            This_Img.sprite = Default_Sprite;
    }
}

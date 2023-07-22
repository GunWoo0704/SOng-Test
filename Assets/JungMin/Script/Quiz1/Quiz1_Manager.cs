using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quiz1_Manager : MonoBehaviour
{
    public static Quiz1_Manager instance = null;

    [SerializeField]
    GameObject[] Folders;
    [SerializeField]
    Sprite Full_Folder_Sprite;
    Sprite Default_Sprite;    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Default_Sprite = Folders[0].GetComponent<Image>().sprite;
    }
    
    public void Show_Full()
    {
        Folders[0].GetComponent<Image>().sprite = Full_Folder_Sprite;
        Folders[5].GetComponent<Image>().sprite = Full_Folder_Sprite;
        Folders[10].GetComponent<Image>().sprite = Full_Folder_Sprite;
    }

    public void Off_Full()
    {
        Folders[0].GetComponent<Image>().sprite = Default_Sprite;
        Folders[5].GetComponent<Image>().sprite = Default_Sprite;
        Folders[10].GetComponent<Image>().sprite = Default_Sprite;
    }

    // 정답 확인 함수.
    public void Delete_Folder()
    {
        bool isClear = true;
        for (int i = 0; i < 12; i++)
        {
            if (i == 0 || i == 5 || i == 10)
            {
                if (Folders[i].GetComponent<Quiz1_Image_Object>().isToggle != false)
                {
                    isClear = false;
                    break;
                }                
            }
            else
            {
                if (Folders[i].GetComponent<Quiz1_Image_Object>().isToggle != true)
                {
                    isClear = false;
                    break;
                }
            }
        }

        if (isClear == false)
            return;
        else
            Clear_Folder();
    }

    void Clear_Folder()
    {
        for (int i = 0; i < 12; i++)
        {
            if (i == 0 || i == 5 || i == 10)
                Folders[i].SetActive(true);
            else
                Folders[i].SetActive(false);
        }

        Folders[5].GetComponent<Animator>().enabled = true;
        Folders[10].GetComponent<Animator>().enabled = true;
    }
}
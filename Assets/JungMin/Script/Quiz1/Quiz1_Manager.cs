using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Quiz1_Manager : MonoBehaviour
{
    public static Quiz1_Manager instance = null;

    [SerializeField]
    GameObject WholeQuiz_Field;
    [SerializeField]
    GameObject[] Folders;
    [SerializeField]
    Sprite Highlight_Folder_Sprite;
    [SerializeField]
    Sprite Full_Folder_Sprite;
    [SerializeField]
    TextMeshProUGUI Upper_Text;
    
    Sprite Default_Sprite;    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Default_Sprite = Folders[0].GetComponent<Image>().sprite;
        Upper_Text.text = "숨겨진 것을 볼 수 있게 하세요.";
    }

    private void Start()
    {
        Dialogue_Manager.instance.Start_Dialogue("Chapter1");
    }
    public void Quiz1_Start()
    {
        // 퀴즈 1 총괄 스타트.
        WholeQuiz_Field.SetActive(true);
    }

    public void Show_Full()
    {
        Folders[0].GetComponent<Image>().sprite = Full_Folder_Sprite;
        Folders[5].GetComponent<Image>().sprite = Full_Folder_Sprite;
        Folders[10].GetComponent<Image>().sprite = Full_Folder_Sprite;
    }

    public void Off_Full()
    {
        if(Folders[0].GetComponent<Quiz1_Image_Object>().isToggle == false)
            Folders[0].GetComponent<Image>().sprite = Default_Sprite;
        else
            Folders[0].GetComponent<Image>().sprite = Highlight_Folder_Sprite;

        if (Folders[5].GetComponent<Quiz1_Image_Object>().isToggle == false)
            Folders[5].GetComponent<Image>().sprite = Default_Sprite;
        else
            Folders[5].GetComponent<Image>().sprite = Highlight_Folder_Sprite;

        if (Folders[10].GetComponent<Quiz1_Image_Object>().isToggle == false)
            Folders[10].GetComponent<Image>().sprite = Default_Sprite;
        else
            Folders[10].GetComponent<Image>().sprite = Highlight_Folder_Sprite;
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
        SetText(2);

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

    public void SetText(int num)
    {
        switch(num)
        {
            case 1:
                Upper_Text.text = "텅 비어 버린 것들은 전부 지워야합니다.";
                break;
            case 2:
                Upper_Text.text = "재정렬을 진행합니다...";
                break;
            case 3:
                Upper_Text.text = "1단계 완료.";
                Invoke("switch_Next_Scene", 1f);                
                break;
        }
    }

    void switch_Next_Scene()
    {
        // 다음 씬으로 넘어가기.
        SceneManager.LoadScene("BuildMap 2");
    }
}
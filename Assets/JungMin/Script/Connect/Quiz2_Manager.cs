using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Quiz2_Manager : MonoBehaviour
{
    public static Quiz2_Manager instance = null;

    [SerializeField]
    GameObject Seed;
    [SerializeField]
    GameObject Sands;
    [SerializeField]
    GameObject Round_Sand;
    [SerializeField]
    GameObject Clouds;
    [SerializeField]
    GameObject Rain;
    [SerializeField]
    GameObject Plant;
    [SerializeField]
    GameObject Sun;
    [SerializeField]
    GameObject Tree;
    [SerializeField]
    TextMeshProUGUI upper_Text;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);        
    }

    [SerializeField]
    GameObject WholeQuiz_Field;

    int Sand_Cnt = 0;
    public void Start()
    {
        Dialogue_Manager.instance.Start_Dialogue("Chapter2");
    }
    public void Quiz2_Start()
    {
        // 퀴즈 1 총괄 스타트.
        WholeQuiz_Field.SetActive(true);
    }

    public void Seed_Collide()
    {
        Sands.SetActive(true);
        Seed.SetActive(false);
        
    }
    public void Sand_Collide()
    {
        Sand_Cnt++;
        if (Sand_Cnt == 3)
        {
            Round_Sand.SetActive(true);
            Clouds.SetActive(true);
            Sands.SetActive(false);
        }
    }
    public void Cloud_Collide()
    {               
        Rain.SetActive(true);               
    }
    public void After_Rain()
    {
        Round_Sand.SetActive(false);
        Clouds.SetActive(false);
        Plant.SetActive(true);
        Sun.SetActive(true);
    }
    public void Sun_Collide()
    {
        Plant.SetActive(false);
        Tree.SetActive(true);
        setText(3);
    }
    public void switch_Next_Scene()
    {
        // 다음 씬으로 넘어가기.
        SceneManager.LoadScene("BuildMap 3");
    }

    public void setText(int n)
    {
        switch(n)
        {
            case 1:
                upper_Text.text = "조금 더 안을 들여다보세요.";
                break;
            case 2:
                upper_Text.text = "조금 더 안을 들여다보세요.\n\n씨앗을 키워서 나무로 자라게 하세요.";                
                break;
            case 3:
                upper_Text.text = "2단계 성공";
                break;
        }
    }
}

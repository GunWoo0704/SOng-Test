using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Quiz3_Manager : MonoBehaviour
{
    public static Quiz3_Manager instance = null;

    [SerializeField]
    GameObject WholeQuiz_Field;
    [SerializeField]
    GameObject Maple_Object;
    [SerializeField]
    GameObject Camera_Shutter;
    [SerializeField]
    GameObject Folder;
    [SerializeField]
    GameObject Synchronized_Object;
    [SerializeField]
    GameObject Locked_Folder;
    [SerializeField]
    GameObject Circuit_Canvas;
    [SerializeField]
    TextMeshProUGUI upper_Text;
    [SerializeField]
    GameObject Maple_Leaf;
    
    [Space]
    [Header("엔딩 처리용")]
    [SerializeField]
    GameObject Quiz_Canvas;
    [SerializeField]
    GameObject Higher_Canvas;
    [SerializeField]
    GameObject Maple_Machine;
    [SerializeField]
    GameObject Maple_Button;

    [Space]
    [Header("터치 처리용")]
    [SerializeField]
    GameObject Touch_Canvas;
    [SerializeField]
    GameObject Maple_Ending_Text;
    float Camera_Time = 0f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        SetText(1);
    }

    private void Start()
    {
        //Dialogue_Manager.instance.Start_Dialogue("Chapter3");
        Dialogue_Manager.instance.Start_Dialogue("Outro");
    }
    public void Quiz3_Start()
    {
        // 퀴즈 3 스타트 함수.
        WholeQuiz_Field.SetActive(true);
    }
    public void active_Maple_Canvas(bool toggle)
    {
        Maple_Object.SetActive(toggle);
    }    
    public void Start_Camera_Effect()
    {        
        StartCoroutine(Camera_Effect());
        Maple_Object.transform.GetChild(1).gameObject.SetActive(true);
    }
    IEnumerator Camera_Effect()
    {        
        while(Camera_Time < 0.25f)
        {
            yield return null;
            Camera_Time += Time.deltaTime;

            if(Camera_Time < 0.15f)
                Camera_Shutter.SetActive(true);
            else
                Camera_Shutter.SetActive(false);
        }
        active_Maple_Canvas(false);
        Start_Synchronize();        
        yield return null;
    }

    public void Start_Synchronize()
    {
        SetText(2);
        Synchronized_Object.SetActive(true);
        Synchronized_Object.GetComponent<Animator>().enabled = true;
    }
    public void Synchronize_End()
    {
        Folder.SetActive(false);
        Synchronized_Object.SetActive(false);

        Locked_Folder.SetActive(true);
        SetText(3);
    }
    public void active_Locked_Folder()
    {
        Circuit_Canvas.SetActive(true);
        SetText(4);
    }

    // 회로 퀴즈 귀찮아서 여기서 전부 처리함.
    string letter = "";
    public void Active_Circuit_Button()
    {
        for(int i = 0; i < Circuit_Canvas.transform.GetChild(0).transform.childCount; i++)      
            Circuit_Canvas.transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(true);
        Circuit_Canvas.transform.GetChild(1).gameObject.SetActive(true);
        Circuit_Canvas.transform.GetChild(2).gameObject.SetActive(true);
        SetText(5);
    }
    public void input_Letter(string s)
    {
        letter += s;
        check_Letter();
    }

    void check_Letter()
    {
        Debug.Log(letter);
        if(letter == "LASTLEAF")        
        {
            // 클리어처리
            Debug.Log("클리어");
            SetText(6);
            // 오브젝트 다 지우고, 단풍잎 서서히 등장.
            Clear_Chapter3();
        }
        else if (letter == "L" || letter == "LA" || letter == "LAS" || letter == "LAST" 
                || letter == "LASTL" || letter == "LASTLE" || letter == "LASTLEA" || letter == "LASTLEAF")
        {
            // 정답 가능성이 있으니 초기화 패스.
            return;
        }
        // 위에 해당되지 않으면 초기화.
        else        
            letter = "";        
    }

    void Clear_Chapter3()
    {
        // 퀴즈 캔버스 내부 다 끄고
        // 하이어 서킷은 둘다 그냥 끔.
        for(int i = 0; i < Quiz_Canvas.transform.childCount; i++)       
            Quiz_Canvas.transform.GetChild(i).gameObject.SetActive(false);
        Higher_Canvas.SetActive(false);
        Circuit_Canvas.SetActive(false);

        // 서서히 등장하는 단풍잎 표시
        Maple_Leaf.GetComponent<Animator>().enabled = true;
    }

    public void Active_Maple_Machine(bool toggle)
    {
        Maple_Machine.SetActive(toggle);
    }    
    public void Active_Touch_Canvas(bool toggle)
    {
        Touch_Canvas.SetActive(toggle);
    }
    public void Maple_Ending(int num)
    {
        Maple_Ending_Text.SetActive(true);
        switch (num)
        {
            case 0:
                Maple_Ending_Text.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                Maple_Ending_Text.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                Maple_Ending_Text.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                Maple_Ending_Text.SetActive(false);
                Maple_Leaf.SetActive(false);
                Maple_Machine.SetActive(false);
                Maple_Button.SetActive(true);
                break;
        }
    }
    void SetText(int num)
    {
        switch (num)
        {
            case 1:
                upper_Text.text = "계속해서 조금 더 안을 들여다보세요.";
                break;
            case 2:
                upper_Text.text = "동기화 시키세요.";
                break;
            case 3:
                upper_Text.text = "마지막 폴더의 잠금을 해제하세요.";
                break;
            case 4:
                upper_Text.text = "중앙에 초점을 맞추세요";
                break;
            case 5:
                upper_Text.text = "희로 속 숨겨진 코드를 완성하세요.";
                break;
            case 6:
                upper_Text.text = "마지막 폴더 잠금 해제 및 3단계 완료.";
                break;
            case 7:
                upper_Text.text = "모두에게 공유하세요.\n\n\"CODE NAME: LAST LEAF\"";
                break;
            case 8:
                upper_Text.text = "\"CODE NAME: LAST LEAF\"\n\n마지막 조각을 맞추면 모든 작동이 중지됩니다.";
                break;
            case 9:
                upper_Text.gameObject.SetActive(false);
                break;
        }
    }
}

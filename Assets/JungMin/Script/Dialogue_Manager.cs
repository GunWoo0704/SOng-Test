using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dialogue_Manager : MonoBehaviour
{
    public static Dialogue_Manager instance = null;
    
    [SerializeField]
    GameObject Touch_Input_Canvas; // 터치 입력을 받는 캔버스
    [SerializeField]
    GameObject Notice_Canvas; // 하단 출력 캔버스
    [SerializeField]
    GameObject Talk_Canvas; // 대화 출력 캔버스    
    [SerializeField]
    GameObject Select_Canvas; // 선택지 출력 캔버스
    [SerializeField]
    GameObject Hint_Canvas; // 힌트 캔버스

    [Space]
    [SerializeField]
    Sprite[] Character_Sprite; // 캐릭터 프사 배열
    [SerializeField]
    GameObject Talk_Box_Prefab; // 토크박스 프리팹
    [SerializeField]
    GameObject Long_Talk_Box_Prefab; // 토크박스 롱프리팹
    [SerializeField]
    GameObject Talk_Box_Hero_Prefab; // 토크박스 주인공 프리팹
    [SerializeField]
    GameObject Long_Talk_Box_Hero_Prefab; // 토크박스 주인공 롱프리팹
    [SerializeField]
    Sprite[] Talk_Box_Sprite; // 왼쪽, 왼쪽롱, 오른, 오른롱 순으로 사용.
    [SerializeField]
    GameObject Select_Box_Prefab; // 선택지박스 프리팹
    [SerializeField]
    Sprite[] Select_Active; // 선택지 스프라이트 On/OFF
    [SerializeField]
    GameObject Notice_Character; // 하단 알림 캐릭터 오브젝트

    private List<Dialogue> current_Dialogues; // 현재 카테고리에 속한 다이얼로그를 List로 불러옴.
    private Dialogue cur_Dialogue; // 현재 다이얼로그 리스트에서 진행해야할 다이얼로그
    public bool isTouch = false; // 터치 캔버스에서 터치를 받으면 isTouch Toggle되는 형식으로 사용.
    private Coroutine current_Coroutine = null; // 코루틴 실행되는지 확인용.
    private int dialogue_Count = 0; // 현재 다이얼로그의 인덱스
    private List<List<string>> talk_List = new List<List<string>>(); // 채팅창을 구현하기 위한 이중 리스트.
    private int cur_Select_Idx = -1;
    private int talk_Box_Y_Pos;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        //Start_Dialogue("Intro");
    }
    void Processing_Dialogue()
    {
        All_Canvas_Close();

        if (cur_Dialogue.type == "하단" || cur_Dialogue.type == "대화")
            Touch_Input_Canvas.SetActive(true);
        switch (cur_Dialogue.type)
        {
            case "하단":
                Notice_Canvas.SetActive(true);
                Notice_Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cur_Dialogue.text;

                for (int i = 0; i < Notice_Character.transform.childCount; i++)
                    Notice_Character.transform.GetChild(i).gameObject.SetActive(false);

                Debug.Log(cur_Dialogue.character);
                switch(cur_Dialogue.character)
                {
                    case "Alien1":
                        Notice_Character.transform.GetChild(0).gameObject.SetActive(true);
                        break;
                    case "Alien2":
                        Notice_Character.transform.GetChild(1).gameObject.SetActive(true);
                        break;
                    case "Alien3":
                        Notice_Character.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                }                
                break;
            case "대화":
                Talk_Canvas.SetActive(true);
                
                // Type = 대화이고, Action란에 Reset이 들어잇는경우, Reset
                if (cur_Dialogue.action == "Reset")
                {
                    int cnt = talk_List.Count;
                    for(int i = 0; i < cnt; i++)                   
                        talk_List.RemoveAt(0);                    
                }

                // 대화창 먼저 초기화하고.
                foreach (Transform prefab in Talk_Canvas.transform)
                {
                    Destroy(prefab.gameObject);
                }

                // 리스트에 캐릭터와 텍스트를 저장한 후, 이중 리스트에 집어넣음.
                List<string> talk_Temp = new List<string>();                
                talk_Temp.Add(cur_Dialogue.character);
                talk_Temp.Add(cur_Dialogue.text);
                talk_List.Add(talk_Temp);

                // 6개가 넘어가면 제일 앞 대화 삭제
                if (talk_List.Count > 6)
                    talk_List.RemoveAt(0);

                // 채팅의 가장 위 Y좌표 설정.
                talk_Box_Y_Pos = 740;
                for (int i = 0; i < talk_List.Count; i++)                
                    Set_Text_Box(i);                
                break;                        
            case "선택지2":
            case "선택지3":            
                // 선택지 뒤에 붙은 숫자가 뭔지 GetNumeric을 통해 추출.
                int optionCount = (int)char.GetNumericValue(cur_Dialogue.type[3]);
                cur_Select_Idx = -1;

                // 먼저 선택지 오브젝트들을 초기화.
                foreach (Transform prefab in Select_Canvas.transform.GetChild(0).transform)
                {
                    Destroy(prefab.gameObject);
                }
                Select_Canvas.SetActive(true);                

                // 선택지 개수만큼 생성.
                for (int i = 0; i < optionCount; i++)
                {
                    Debug.Log("선택지" + i);
                    Instantiate(Select_Box_Prefab, Select_Canvas.transform.GetChild(0).transform).GetComponent<Select_Object>().SetText(current_Dialogues[dialogue_Count], i);
                    if (i != optionCount - 1)
                        dialogue_Count++;
                }                
                break;            
        }
    }

    void All_Canvas_Close()
    {
        Touch_Input_Canvas.SetActive(false);
        Select_Canvas.SetActive(false);
        Notice_Canvas.SetActive(false);
        Talk_Canvas.SetActive(false);
    }
    // 카테고리 이름을 변수로 받아서 해당 다이얼로그 출력
    public void Start_Dialogue(string category)
    {
        // 현재 다이얼로그의 인덱스를 0으로.
        dialogue_Count = 0;

        // 시작할 다이얼로그 불러오기.
        current_Dialogues = Database_Manager.instance.P1_Dialogues[category];

        // 대사를 토대로 코루틴 실행.
        isTouch = false;
        current_Coroutine = StartCoroutine(Continue_Dialogue());
    }
    
    void End_Dialogue()
    {
        // 해당 카테고리의 다이얼로그가 끝나면 코루틴 중단
        if (current_Coroutine != null)
            StopCoroutine(current_Coroutine);

        // 모든 캔버스 닫기.
        All_Canvas_Close();
    }
    void Print_Next_Dialogue()
    {        
        // 현재 인덱스에 해당되는 다이얼로그 저장.
        cur_Dialogue = current_Dialogues[dialogue_Count];        
        Processing_Dialogue();

        // 테스트용
        Debug.Log(cur_Dialogue.text);

        // 다이얼로그 카운트 추가.
        dialogue_Count++;
    }

    IEnumerator Continue_Dialogue()
    {        
        while (dialogue_Count < current_Dialogues.Count)
        {                         
            Print_Next_Dialogue();
            // 클릭 입력 대기
            while (!isTouch)
            {                
                yield return null;
            }
            isTouch = false;            
        }
        End_Dialogue();
    }

    Sprite return_Character_Sprite(string character)
    {
        switch (character)
        {
            case "Red":
                return Character_Sprite[0];
            default:
                return Character_Sprite[1];
        }
    }    

    void Set_Text_Box(int i)
    {
        GameObject clone;
        bool isLong = false;
        // 두 줄 대화창을 써야하는 경우.
        if (talk_List[i][1].Length > 17)                    
            isLong = true;                

        // 주인공이 아닌 경우.
        if (talk_List[i][0] != "외계인")
        {            
            if (isLong == false)
            {
                clone = Instantiate(Talk_Box_Prefab, Talk_Canvas.transform);

                talk_Box_Y_Pos -= 100;
                talk_Box_Y_Pos -= 80;
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(-220f, talk_Box_Y_Pos);
                clone.GetComponent<Image>().sprite = Talk_Box_Sprite[0];
                talk_Box_Y_Pos -= 80;
            }
            else
            {
                clone = Instantiate(Long_Talk_Box_Prefab, Talk_Canvas.transform);

                talk_Box_Y_Pos -= 100;
                talk_Box_Y_Pos -= 120;
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(-220f, talk_Box_Y_Pos);
                clone.GetComponent<Image>().sprite = Talk_Box_Sprite[1];
                talk_Box_Y_Pos -= 120;
            }
            // 프사 띄우고 왼쪽에 생성.
            clone.transform.GetChild(1).gameObject.SetActive(true);
            clone.transform.GetChild(1).GetComponent<Image>().sprite = return_Character_Sprite(talk_List[i][0]);
        }
        // 주인공이면 오른쪽에 생성만.
        else
        {            
            if (isLong == false)
            {
                clone = Instantiate(Talk_Box_Hero_Prefab, Talk_Canvas.transform);

                talk_Box_Y_Pos -= 100;
                talk_Box_Y_Pos -= 80;
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(220f, talk_Box_Y_Pos);
                clone.GetComponent<Image>().sprite = Talk_Box_Sprite[2];
                talk_Box_Y_Pos -= 40;
            }
            else
            {
                clone = Instantiate(Long_Talk_Box_Hero_Prefab, Talk_Canvas.transform);

                talk_Box_Y_Pos -= 100;
                talk_Box_Y_Pos -= 120;
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(220f, talk_Box_Y_Pos);
                clone.GetComponent<Image>().sprite = Talk_Box_Sprite[3];
                talk_Box_Y_Pos -= 60;
            }
        }
        // 텍스트 삽입.
        clone.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = talk_List[i][1];
    }    

    public void Select_Box(int idx)
    {
        Transform Select_Background = Select_Canvas.transform.GetChild(0).transform;
        for (int i = 0; i < Select_Background.childCount; i++)
        {
            if (i == idx)
            {
                Select_Background.GetChild(i).GetComponent<Image>().sprite = Select_Active[1];
                cur_Select_Idx = i;
            }
            else
                Select_Background.GetChild(i).GetComponent<Image>().sprite = Select_Active[0];
        }
    }

    public void Select_Send_Button()
    {
        Transform Select_Background = Select_Canvas.transform.GetChild(0).transform;

        // 아무것도 고르지 않았다면 리턴.
        if (cur_Select_Idx == -1)
            return;

        Start_Dialogue(Select_Background.GetChild(cur_Select_Idx).GetComponent<Select_Object>().action);
    }
}

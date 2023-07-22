using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dialogue_Manager : MonoBehaviour
{
    public static Dialogue_Manager instance = null;
    
    [SerializeField]
    GameObject Touch_Input_Canvas; // ��ġ �Է��� �޴� ĵ����
    [SerializeField]
    GameObject Notice_Canvas; // �ϴ� ��� ĵ����
    [SerializeField]
    GameObject Talk_Canvas; // ��ȭ ��� ĵ����    
    [SerializeField]
    GameObject Select_Canvas; // ������ ��� ĵ����
    [SerializeField]
    GameObject Hint_Canvas; // ��Ʈ ĵ����

    [Space]
    [SerializeField]
    Sprite[] Character_Sprite; // ĳ���� ���� �迭
    [SerializeField]
    GameObject Talk_Box_Prefab; // ��ũ�ڽ� ������
    [SerializeField]
    GameObject Long_Talk_Box_Prefab; // ��ũ�ڽ� ��������
    [SerializeField]
    GameObject Talk_Box_Hero_Prefab; // ��ũ�ڽ� ���ΰ� ������
    [SerializeField]
    GameObject Long_Talk_Box_Hero_Prefab; // ��ũ�ڽ� ���ΰ� ��������
    [SerializeField]
    Sprite[] Talk_Box_Sprite; // ����, ���ʷ�, ����, ������ ������ ���.
    [SerializeField]
    GameObject Select_Box_Prefab; // �������ڽ� ������
    [SerializeField]
    Sprite[] Select_Active; // ������ ��������Ʈ On/OFF
    [SerializeField]
    GameObject Notice_Character; // �ϴ� �˸� ĳ���� ������Ʈ

    private List<Dialogue> current_Dialogues; // ���� ī�װ��� ���� ���̾�α׸� List�� �ҷ���.
    private Dialogue cur_Dialogue; // ���� ���̾�α� ����Ʈ���� �����ؾ��� ���̾�α�
    public bool isTouch = false; // ��ġ ĵ�������� ��ġ�� ������ isTouch Toggle�Ǵ� �������� ���.
    private Coroutine current_Coroutine = null; // �ڷ�ƾ ����Ǵ��� Ȯ�ο�.
    private int dialogue_Count = 0; // ���� ���̾�α��� �ε���
    private List<List<string>> talk_List = new List<List<string>>(); // ä��â�� �����ϱ� ���� ���� ����Ʈ.
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

        if (cur_Dialogue.type == "�ϴ�" || cur_Dialogue.type == "��ȭ")
            Touch_Input_Canvas.SetActive(true);
        switch (cur_Dialogue.type)
        {
            case "�ϴ�":
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
            case "��ȭ":
                Talk_Canvas.SetActive(true);
                
                // Type = ��ȭ�̰�, Action���� Reset�� ����մ°��, Reset
                if (cur_Dialogue.action == "Reset")
                {
                    int cnt = talk_List.Count;
                    for(int i = 0; i < cnt; i++)                   
                        talk_List.RemoveAt(0);                    
                }

                // ��ȭâ ���� �ʱ�ȭ�ϰ�.
                foreach (Transform prefab in Talk_Canvas.transform)
                {
                    Destroy(prefab.gameObject);
                }

                // ����Ʈ�� ĳ���Ϳ� �ؽ�Ʈ�� ������ ��, ���� ����Ʈ�� �������.
                List<string> talk_Temp = new List<string>();                
                talk_Temp.Add(cur_Dialogue.character);
                talk_Temp.Add(cur_Dialogue.text);
                talk_List.Add(talk_Temp);

                // 6���� �Ѿ�� ���� �� ��ȭ ����
                if (talk_List.Count > 6)
                    talk_List.RemoveAt(0);

                // ä���� ���� �� Y��ǥ ����.
                talk_Box_Y_Pos = 740;
                for (int i = 0; i < talk_List.Count; i++)                
                    Set_Text_Box(i);                
                break;                        
            case "������2":
            case "������3":            
                // ������ �ڿ� ���� ���ڰ� ���� GetNumeric�� ���� ����.
                int optionCount = (int)char.GetNumericValue(cur_Dialogue.type[3]);
                cur_Select_Idx = -1;

                // ���� ������ ������Ʈ���� �ʱ�ȭ.
                foreach (Transform prefab in Select_Canvas.transform.GetChild(0).transform)
                {
                    Destroy(prefab.gameObject);
                }
                Select_Canvas.SetActive(true);                

                // ������ ������ŭ ����.
                for (int i = 0; i < optionCount; i++)
                {
                    Debug.Log("������" + i);
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
    // ī�װ� �̸��� ������ �޾Ƽ� �ش� ���̾�α� ���
    public void Start_Dialogue(string category)
    {
        // ���� ���̾�α��� �ε����� 0����.
        dialogue_Count = 0;

        // ������ ���̾�α� �ҷ�����.
        current_Dialogues = Database_Manager.instance.P1_Dialogues[category];

        // ��縦 ���� �ڷ�ƾ ����.
        isTouch = false;
        current_Coroutine = StartCoroutine(Continue_Dialogue());
    }
    
    void End_Dialogue()
    {
        // �ش� ī�װ��� ���̾�αװ� ������ �ڷ�ƾ �ߴ�
        if (current_Coroutine != null)
            StopCoroutine(current_Coroutine);

        // ��� ĵ���� �ݱ�.
        All_Canvas_Close();
    }
    void Print_Next_Dialogue()
    {        
        // ���� �ε����� �ش�Ǵ� ���̾�α� ����.
        cur_Dialogue = current_Dialogues[dialogue_Count];        
        Processing_Dialogue();

        // �׽�Ʈ��
        Debug.Log(cur_Dialogue.text);

        // ���̾�α� ī��Ʈ �߰�.
        dialogue_Count++;
    }

    IEnumerator Continue_Dialogue()
    {        
        while (dialogue_Count < current_Dialogues.Count)
        {                         
            Print_Next_Dialogue();
            // Ŭ�� �Է� ���
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
        // �� �� ��ȭâ�� ����ϴ� ���.
        if (talk_List[i][1].Length > 17)                    
            isLong = true;                

        // ���ΰ��� �ƴ� ���.
        if (talk_List[i][0] != "�ܰ���")
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
            // ���� ���� ���ʿ� ����.
            clone.transform.GetChild(1).gameObject.SetActive(true);
            clone.transform.GetChild(1).GetComponent<Image>().sprite = return_Character_Sprite(talk_List[i][0]);
        }
        // ���ΰ��̸� �����ʿ� ������.
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
        // �ؽ�Ʈ ����.
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

        // �ƹ��͵� ���� �ʾҴٸ� ����.
        if (cur_Select_Idx == -1)
            return;

        Start_Dialogue(Select_Background.GetChild(cur_Select_Idx).GetComponent<Select_Object>().action);
    }
}

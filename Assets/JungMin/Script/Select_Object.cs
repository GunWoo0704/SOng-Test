using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Select_Object : MonoBehaviour
{    
    public string action;
    int idx;    
    
    // 선택지에 들어갈 텍스트를 수정하고 OnclickListener를 추가합니다.
    public void SetText(Dialogue dialogue, int i)
    {        
        action = dialogue.action;
        idx = i;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogue.text;
        transform.GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    // 선택지를 누르면, 그 선택지 텍스트 뒤에 입력된 action으로 다이얼로그 새로 진행.
    public void OnClickButton()
    {                
        Dialogue_Manager.instance.Select_Box(idx);        
    }    
}

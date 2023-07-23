using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Manager : MonoBehaviour
{
    public static Database_Manager instance = null;

    List<Dictionary<string, object>> P1_Texts;
    public Dictionary<string, List<Dialogue>> P1_Dialogues;
    string P1_CSV_Name = "P1_Texts";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        P1_Texts = ReadData(P1_CSV_Name);
        P1_Dialogues = RefineData(P1_Texts);
    }

    protected List<Dictionary<string, object>> ReadData(string CSVName)
    {
        return CSV_Reader.Read(CSVName);
    }

    protected Dictionary<string, List<Dialogue>> RefineData(List<Dictionary<string, object>> texts)
    {
        Dictionary<string, List<Dialogue>> temp = new Dictionary<string, List<Dialogue>>();

        // 쉼표를 기준으로 잘려져있는 단어의 갯수만큼 반복.
        for (int i = 0; i < texts.Count; i++)
        {
            string category = texts[i]["category"].ToString();

            Dialogue tempDialogue = new Dialogue();
            tempDialogue.category = category;
            tempDialogue.type = texts[i]["type"].ToString();
            tempDialogue.character = RefineText(texts[i]["character"].ToString());
            tempDialogue.text = RefineText(texts[i]["text"].ToString());            
            tempDialogue.action = texts[i]["action"].ToString();            

            if (!temp.ContainsKey(category))
            {
                List<Dialogue> tempDialogues = new List<Dialogue>();
                tempDialogues.Add(tempDialogue);
                temp.Add(category, tempDialogues);
            }
            else
            {
                temp[category].Add(tempDialogue);
            }
        }
        return temp;
    }
    
    protected string RefineText(string tempString)
    {
        return tempString.Replace("@", ",").Replace("\\n", "\n")
                         .Replace("\"", "");
    }
}
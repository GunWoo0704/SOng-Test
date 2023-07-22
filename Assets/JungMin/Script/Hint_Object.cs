using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Hint_Object : MonoBehaviour
{
    float time;
    TextMeshProUGUI Time_Text;

    public void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Time_Text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        time = 3.5f;       
        StartCoroutine(Start_Timer());
    }

    IEnumerator Start_Timer()
    {
        while(time >= 0.9f)
        {            
            time -= Time.deltaTime;
            yield return null;

            if (time > 3f)
                Time_Text.text = "3 Second..";
            else if (time > 2f)
                Time_Text.text = "2 Second..";
            else if (time > 1f)
                Time_Text.text = "1 Second..";
        }
        gameObject.SetActive(false);
        yield return null;
    }
}

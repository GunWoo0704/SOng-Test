using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeLoadingScreen : MonoBehaviour
{
    public float delayTime = 5.0f;
    public string nextSceneName; // 새로운 변수를 추가하십시오.

    void Start()
    {
        StartCoroutine(DelayedLoad());
    }

    IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(nextSceneName); // 수정된 코드.
    }
}

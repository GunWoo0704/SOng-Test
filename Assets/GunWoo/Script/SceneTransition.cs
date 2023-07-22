using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public Image overlayImage;
	public float fadeDuration = 2.0f;
	public Color startColor = Color.clear;  // 초기 색상을 투명하게 설정
	public Color endColor = Color.black;

	private void Start()
	{
		overlayImage.color = startColor; // 시작 색상을 투명하게 설정
	}

	public void FadeOutAndLoadScene(string nextScene)
	{
		StartCoroutine(FadeAndLoadScene(nextScene));
	}

	private IEnumerator FadeAndLoadScene(string nextScene)
	{
		float elapsedTime = 0.0f;

		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			overlayImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
			yield return null;
		}

		SceneManager.LoadScene(nextScene);
	}
}

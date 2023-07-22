using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public Image overlayImage;
	public float fadeDuration = 2.0f;
	public Color startColor = Color.clear;  // �ʱ� ������ �����ϰ� ����
	public Color endColor = Color.black;

	private void Start()
	{
		overlayImage.color = startColor; // ���� ������ �����ϰ� ����
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

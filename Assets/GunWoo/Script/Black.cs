using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Black : MonoBehaviour
{
	public Image overlayImage;
	public float fadeDuration = 2.0f;
	public Color startColor = Color.clear;
	public Color endColor = Color.black;

	public void FadeToScene(string nextScene)
	{
		StartCoroutine(FadeAndLoadScene(nextScene));
	}

	private IEnumerator FadeAndLoadScene(string nextScene)
	{
		float elapsedTime = 0.0f;

		// Fade in (ȭ���� ���� ���������� ����)
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			overlayImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
			yield return null;
		}

		SceneManager.LoadScene(nextScene);
	}
}

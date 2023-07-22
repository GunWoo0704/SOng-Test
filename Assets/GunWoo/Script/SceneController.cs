using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public GameObject loadingScreenPrefab;

	// 예시 게임 완료 조건
	public int completionScore = 100;
	private int currentScore = 0;

	void Update()
	{
		// 게임 완료 조건 확인 (예: 현재 점수가 완료 점수 이상인 경우)
		if (currentScore >= completionScore)
		{
			LevelCompleted();
		}
	}

	public void AddScore(int points)
	{
		// 점수 추가 메서드 (예: 게임 내에서 목표를 충족하거나 항목을 수집할 때 점수 추가)
		currentScore += points;
	}

	public void LevelCompleted()
	{
		StartCoroutine(ShowLoadingScreenAndLoadNextLevel());
	}

	IEnumerator ShowLoadingScreenAndLoadNextLevel()
	{
		// 로딩 화면 Prefab을 화면에 추가
		GameObject loadingScreen = Instantiate(loadingScreenPrefab);
		// 로딩 화면이 표시되는 동안 잠시 (약 5초) 대기
		yield return new WaitForSeconds(5f);
		// 다음 씬 로드
		SceneManager.LoadScene(loadingScreen.GetComponent<FakeLoadingScreen>().nextSceneName);
	}
}

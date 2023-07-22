using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnClick : MonoBehaviour
{
	public Button button; // 버튼을 참조할 변수입니다.
	public GameObject image; // 이미지 게임 오브젝트를 참조할 변수입니다.

	void Start()
	{
		button.onClick.AddListener(ShowImage);
	}

	void ShowImage()
	{
		image.SetActive(true); // 이미지를 활성화합니다.
	}
}


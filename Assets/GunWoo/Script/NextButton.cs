using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
	public Button Button1;
	public Button Button2;
	public Button Button3;

	private void Start()
	{
		Button1.onClick.AddListener(ShowButton2);
		Button2.onClick.AddListener(ShowButton3);
	}

	private void ShowButton2()
	{
		Button2.gameObject.SetActive(true);
	}

	private void ShowButton3()
	{
		Button3.gameObject.SetActive(true);
	}
}
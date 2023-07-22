using System.Collections;
using UnityEngine;

public class Del : MonoBehaviour
{
	// 세 번째 이미지 게임 오브젝트를 참조하는 변수
	public GameObject objectToRemove;

	void Start()
	{
		Destroy(objectToRemove);
	}
}


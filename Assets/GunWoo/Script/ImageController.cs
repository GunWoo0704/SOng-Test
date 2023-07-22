//using System.Collections;
//using UnityEngine;

//public class ImageController : MonoBehaviour
//{
//	public GameObject firstImage; // 첫 번째 이미지 게임 오브젝트를 참조하는 변수
//	public GameObject secondImage; // 두 번째 이미지 게임 오브젝트를 참조하는 변수

//	void Start()
//	{
//		firstImage.SetActive(true); // 첫 번째 이미지를 활성화합니다.
//		secondImage.SetActive(false); // 두 번째 이미지를 비활성화합니다.
//		StartCoroutine(SwitchImagesAfterDelay(2f)); // 3초 후에 이미지를 변경하는 코루틴을 실행합니다.
//	}

//	IEnumerator SwitchImagesAfterDelay(float delay)
//	{
//		yield return new WaitForSeconds(delay); // 지연 시간이 지날 때까지 기다립니다.
//		firstImage.SetActive(false); // 첫 번째 이미지를 비활성화합니다.
//		secondImage.SetActive(true); // 두 번째 이미지를 활성화합니다.
//	}
//}using System.Collections;
//using System.Collections;
//using UnityEngine;

//public class ImageController : MonoBehaviour
//{
//	public GameObject firstImage;
//	public GameObject secondImage;
//	public GameObject objectToRemove;

//	void Start()
//	{
//		firstImage.SetActive(true);
//		secondImage.SetActive(false);
//		StartCoroutine(SwitchImagesAfterDelay(3f));
//	}

//	IEnumerator SwitchImagesAfterDelay(float delay)
//	{
//		yield return new WaitForSeconds(delay);
//		firstImage.SetActive(false);
//		secondImage.SetActive(true);

//		// 오브젝트를 비활성화하거나 파괴합니다.
//		// objectToRemove.SetActive(false); // 오브젝트를 비활성화
//		Destroy(objectToRemove); // 오브젝트를 파괴
//	}
//}
using System.Collections;
using UnityEngine;

public class ImageController : MonoBehaviour
{
	public GameObject firstImage;
	public GameObject secondImage;
	public GameObject thirdImage; // 세 번째 이미지 게임 오브젝트를 참조하는 변수
	public GameObject objectToRemove;

	void Start()
	{
		firstImage.SetActive(true);
		secondImage.SetActive(false);
		thirdImage.SetActive(false); // 세 번째 이미지를 비활성화합니다.
		StartCoroutine(SwitchImagesAfterDelay(3f));
	}

	IEnumerator SwitchImagesAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		firstImage.SetActive(false);
		secondImage.SetActive(true);
		thirdImage.SetActive(true); // 두 번째 이미지와 동시에 세 번째 이미지를 활성화합니다.
		Destroy(objectToRemove);
	}
}




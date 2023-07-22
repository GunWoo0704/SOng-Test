using UnityEngine;

public class DraggableObject : MonoBehaviour
{
	public GameObject targetImage; // 이미지 게임 오브젝트를 참조하는 변수
	public Transform targetArea;   // 오브젝트를 놓아야 하는 영역
	public float allowedDistance = 1f; // 허용되는 오차 거리

	private Vector3 offset;
	private Camera mainCamera;
	private bool isDragging = false;

	private void Start()
	{
		mainCamera = Camera.main;
		targetImage.SetActive(false); // 이미지를 초기에 비활성화
	}

	private void Update()
	{
		if (isDragging)
		{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(transform.position).z);
			transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
		}
	}

	private void OnMouseDown()
	{
		isDragging = true;
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(transform.position).z);
		offset = transform.position - mainCamera.ScreenToWorldPoint(mousePosition);
	}

	private void OnMouseUp()
	{
		isDragging = false;

		// 드래그 된 오브젝트와 대상 영역 사이의 거리 확인
		float distance = Vector2.Distance(transform.position, targetArea.position);

		// 거리가 허용되는 오차 범위 내에 있는 경우
		if (distance <= allowedDistance)
		{
			targetImage.SetActive(true); // 이미지를 활성화
		}
	}
}

using UnityEngine;

public class DraggableObject : MonoBehaviour
{
	public GameObject targetImage; // 이미지 게임 오브젝트를 참조하는 변수
	public Transform targetArea;   // 오브젝트를 놓아야 하는 영역
	public float allowedDistance = 0.5f; // 허용되는 오차 거리

	private Vector3 offset;
	private Camera mainCamera;
	private bool isDragging = false;
	private bool isOverTarget = false;

	void Start()
	{
		mainCamera = Camera.main;
		targetImage.SetActive(false); // 이미지를 초기에 비활성화
	}

	void Update()
	{
		if (isDragging)
		{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(transform.position).z);
			transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;

			// 드래그 된 오브젝트와 대상 영역 사이의 거리 확인
			float distance = Vector2.Distance(transform.position, targetArea.position);

			// 거리가 허용되는 오차 범위 내에 있는 경우
			if (distance <= allowedDistance)
			{
				isOverTarget = true;
			}
			else
			{
				isOverTarget = false;
			}
		}
	}

	void OnMouseDown()
	{
		isDragging = true;
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(transform.position).z);
		offset = transform.position - mainCamera.ScreenToWorldPoint(mousePosition);
	}

	void OnMouseUp()
	{
		isDragging = false;

		// 대상 영역 위에 드롭된 경우
		if (isOverTarget)
		{
			targetImage.SetActive(true); // 이미지를 활성화
			gameObject.SetActive(false); // 오브젝트 비활성화
										 // 오브젝트 고정을 위해 필요한 추가 작업
			Quiz2_Manager.instance.setText(2);
		}
		else // 대상 영역 밖으로 드롭된 경우
		{
			targetImage.SetActive(false); // 이미지를 비활성화
		}
	}
}

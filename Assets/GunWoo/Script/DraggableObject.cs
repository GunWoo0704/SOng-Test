using UnityEngine;

public class DraggableObject : MonoBehaviour
{
	public GameObject targetImage; // �̹��� ���� ������Ʈ�� �����ϴ� ����
	public Transform targetArea;   // ������Ʈ�� ���ƾ� �ϴ� ����
	public float allowedDistance = 1f; // ���Ǵ� ���� �Ÿ�

	private Vector3 offset;
	private Camera mainCamera;
	private bool isDragging = false;

	private void Start()
	{
		mainCamera = Camera.main;
		targetImage.SetActive(false); // �̹����� �ʱ⿡ ��Ȱ��ȭ
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

		// �巡�� �� ������Ʈ�� ��� ���� ������ �Ÿ� Ȯ��
		float distance = Vector2.Distance(transform.position, targetArea.position);

		// �Ÿ��� ���Ǵ� ���� ���� ���� �ִ� ���
		if (distance <= allowedDistance)
		{
			targetImage.SetActive(true); // �̹����� Ȱ��ȭ
		}
	}
}

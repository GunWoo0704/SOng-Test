using UnityEngine;

public class GrabShadow : MonoBehaviour
{
	private Vector3 offset;
	private Camera mainCamera;
	private bool isDragging = false;

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (isDragging)
		{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(transform.position).z);
			transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
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
	}
}

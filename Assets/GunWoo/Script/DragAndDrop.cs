using UnityEngine;
using UnityEngine.SceneManagement;
public class DragAndDrop : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
	public Transform targetArea;
	public float allowedDistance = 0.5f;
	public string nextSceneName = "BuildMap 1";
	private void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	private void OnMouseDrag()
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
	}

	private void OnMouseUp()
	{
		if (Vector3.Distance(transform.position, targetArea.position) <= allowedDistance)
		{
			SceneManager.LoadScene("BuildMap 1");
		}
	}
}
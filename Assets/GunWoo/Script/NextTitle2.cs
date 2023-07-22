using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
	public void SceneChange()
	{
		SceneManager.LoadScene("Title 2");
	}
}

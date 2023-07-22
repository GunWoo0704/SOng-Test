using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextBuild2 : MonoBehaviour
{
	public void SceneChange()
	{
		SceneManager.LoadScene("BuildMap 2");
	}
}

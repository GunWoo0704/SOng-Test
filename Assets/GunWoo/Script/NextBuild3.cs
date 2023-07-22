using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextBuild3 : MonoBehaviour
{
	public void SceneChange()
	{
		SceneManager.LoadScene("BuildMap 3");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	private void Update()
	{
		if (Debug.isDebugBuild)
		{
			SkipToNextLevel();
		}
	}

	public void LoadNextScene()
	{
		var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadSceneAsync(CurrentSceneIndex + 1);
	}

	public void RestartScene()
	{
		var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadSceneAsync(CurrentSceneIndex);
	}

	private void SkipToNextLevel()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

			if (CurrentSceneIndex < (SceneManager.sceneCountInBuildSettings -1))
			{
				SceneManager.LoadSceneAsync(CurrentSceneIndex + 1);
			}
			else if (CurrentSceneIndex >= (SceneManager.sceneCountInBuildSettings -1))
			{
				SceneManager.LoadSceneAsync(0);
			}
		}
	}
}
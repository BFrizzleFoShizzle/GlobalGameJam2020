using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayBtn : MonoBehaviour
{
	const string nextSceneName = "Scenes/Scavenge";
	AsyncOperation combatLoad;

	// Start is called before the first frame update
	void Start()
	{
		combatLoad = SceneManager.LoadSceneAsync(nextSceneName);
		combatLoad.allowSceneActivation = false;
		Button button = GetComponent<Button>();
		button.onClick.AddListener(() => { combatLoad.allowSceneActivation = true; });

	}

	// Update is called once per frame
	void Update()
	{

	}
}

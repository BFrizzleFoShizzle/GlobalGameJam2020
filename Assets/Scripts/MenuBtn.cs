using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
	const string nextSceneName = "Scenes/MainMenu";
	AsyncOperation menuLoad;

	// Start is called before the first frame update
	void Start()
    {
		//menuLoad = SceneManager.LoadSceneAsync(nextSceneName);
		//menuLoad.allowSceneActivation = false;
		Button button = GetComponent<Button>();
		button.onClick.AddListener(() => { SceneManager.LoadScene(nextSceneName); });
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

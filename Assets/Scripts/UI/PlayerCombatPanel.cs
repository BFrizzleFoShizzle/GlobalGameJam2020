using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatPanel : MonoBehaviour
{
	public RawImage healthBar;
	public Text playerTitle;
	public Text healthText;
	public GameObject deadPanel;

	private Robot robot;

	// Start is called before the first frame update
	void Start()
    {
		Debug.Assert(healthBar != null);
		Debug.Assert(playerTitle != null);
		Debug.Assert(healthText != null);
		Debug.Assert(deadPanel != null);
	}

	// Update is called once per frame
	void Update()
	{
		if (robot != null)
		{
			healthBar.transform.localScale = new Vector3(robot.GetHealth() / robot.GetMaxHealth(), 1.0f, 1.0f); ;
			healthText.text = "" + robot.GetHealth() + " / " + robot.GetMaxHealth();
			if (!robot.IsAlive())
				deadPanel.SetActive(true);
		}
	}

	public void SetPlayer(string playerName, Robot robot)
	{
		playerTitle.text = playerName;
		this.robot = robot;
	}
}

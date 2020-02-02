using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Combat : MonoBehaviour
{
	private static int winnerIdx = -1;

	public static int GetWinner()
	{
		return winnerIdx;
	}

	public List<Transform> playerSpawns;
	public List<GameObject> robotPrefabs;
	// TODO how does this get set?
	private List<List<Part>> playerParts;

	private List<Robot> players;

	// test code
	public List<GameObject> debugPartPrefabs;

	public GameObject HUDPanel;
	public GameObject playerCombatPanelPrefab;

	const string nextSceneName = "Scenes/GameOver";
	AsyncOperation gameOverLoad;

	// Start is called before the first frame update
	void Start()
    {
		Debug.Log(HUDPanel != null);
		Debug.Log(playerCombatPanelPrefab != null);

		players = new List<Robot>();

		Debug.Log(Gamepad.all.Count);

		gameOverLoad = SceneManager.LoadSceneAsync(nextSceneName);
		gameOverLoad.allowSceneActivation = false;

		foreach (Controller controller in Scavenge.GetPlayerControllers())
		{
				int prefabIdx = Random.Range(0, robotPrefabs.Count);
				GameObject robotObj = Instantiate(robotPrefabs[prefabIdx]); ;

				Robot robot = robotObj.GetComponent<Robot>();
				Debug.Assert(robot != null);
				robot.SetController(controller);
				robot.transform.position = playerSpawns[players.Count].position;
				players.Add(robot);

				GameObject newPanelObj = Instantiate(playerCombatPanelPrefab, HUDPanel.transform);
				PlayerCombatPanel newPanel = newPanelObj.GetComponent<PlayerCombatPanel>();
				newPanel.SetPlayer("Player " + players.Count, robot);
		}

		//GenerateRandomRobots();
		
		playerParts = Scavenge.GetPlayerPartLists();

		for (int i=0;i<players.Count; ++i)
		{
			Robot robot = players[i];

			foreach (Part part in playerParts[i])
			{
				part.gameObject.SetActive(true);
				robot.AddPartToRandomPoint(part);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
		int alivePlayers = 0;

		for (int i = 0; i < players.Count; ++i)
			if (players[i].IsAlive())
				++alivePlayers;

		if (alivePlayers == 1)
		{
			// one player left, winner
			for (int i = 0; i < players.Count; ++i)
				if (players[i].IsAlive())
					winnerIdx = i;

			EndCombat();
		}

		// escape to go back to menu
		if(Keyboard.current.escapeKey.isPressed)
			EndCombat();
	}

	// test code
	private void GenerateRandomRobots()
	{
		playerParts = new List<List<Part>>();
		for(int i=0;i<players.Count;++i)
		{
			playerParts.Add(new List<Part>());

			for (int j = 0; j < 3; ++j)
			{
				int partIdx = Random.Range(0, debugPartPrefabs.Count);
				GameObject partObj = Instantiate(debugPartPrefabs[partIdx]);
				Part part = partObj.GetComponent<Part>();
				Debug.Assert(part != null);
				playerParts[i].Add(part);
			}
		}
	}

	private void EndCombat()
	{
		Debug.Log("COMBAT END");
		// cleanup undestroyable parts
		foreach (List<Part> parts in playerParts)
			foreach (Part part in parts)
				Destroy(part);

		gameOverLoad.allowSceneActivation = true;
	}
}

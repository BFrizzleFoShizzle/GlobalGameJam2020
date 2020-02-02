using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
	public List<Transform> playerSpawns;
	public GameObject robotPrefab;
	// TODO how does this get set?
	private List<List<Part>> playerParts;

	private List<Robot> players;

	// test code
	public List<GameObject> debugPartPrefabs;

	public GameObject HUDPanel;
	public GameObject playerCombatPanelPrefab;

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(HUDPanel != null);
		Debug.Log(playerCombatPanelPrefab != null);

		players = new List<Robot>();

		Debug.Log(Gamepad.all.Count);

		foreach (Gamepad gamepad in Gamepad.all)
		{
			if (gamepad.enabled)
			{
				GameObject robotObj = Instantiate(robotPrefab);

				Robot robot = robotObj.GetComponent<Robot>();
				Debug.Assert(robot != null);
				robot.SetController(new GamepadController(gamepad));
				robot.transform.position = playerSpawns[players.Count].position;
				players.Add(robot);

				GameObject newPanelObj = Instantiate(playerCombatPanelPrefab, HUDPanel.transform);
				PlayerCombatPanel newPanel = newPanelObj.GetComponent<PlayerCombatPanel>();
				newPanel.SetPlayer("Player " + players.Count, robot);
			}
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
		// cleanup undestroyable parts
		foreach (List<Part> parts in playerParts)
			foreach (Part part in parts)
				Destroy(part);
	}
}

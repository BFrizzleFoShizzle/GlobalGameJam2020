using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scavenge : MonoBehaviour
{
	// used for passing state between scenes
	private static List<List<Part>> staticPlayerPartList;

	public static List<List<Part>> GetPlayerPartLists()
	{
		return staticPlayerPartList;
	}

	private static List<Controller> playerControllers;

	public static List<Controller> GetPlayerControllers()
	{
		return playerControllers;
	}


	public List<Transform> playerSpawns;
	public List<Transform> playerDrops;
	//public GameObject scavengerPrefab;
	public List<GameObject> scavengerPrefabs;
	public GameObject pickupPartPrefab;
	public GameObject partSpawn;
	public Text timerText;
	// parts lists
	public List<GameObject> weaponPrefabList;
	public List<GameObject> legsPrefabList;
	public List<GameObject> otherPrefabList;
	// TODO how does this get set?
	private List<List<Part>> playerParts;
	private List<ScavengerRobot> players;

	private List<Controller> potentialControllers;

	const float TotalScavengeTime = 20.0f;
	private float scavengeTime;

	const string nextSceneName = "Scenes/Combat";
	AsyncOperation combatLoad;

	private bool scavengingFinished = false;

	private float joinTime = 5.0f;
	private bool hasStarted = false;

	public GameObject joinImage;

	// Start is called before the first frame update
	void Start()
    {
		Debug.Assert(pickupPartPrefab != null);
		Debug.Assert(partSpawn != null);
		Debug.Assert(timerText != null);
		Debug.Assert(joinImage != null);
		Debug.Assert(playerSpawns.Count == playerDrops.Count);
		Debug.Assert(scavengerPrefabs.Count == playerSpawns.Count);

		players = new List<ScavengerRobot>();
		potentialControllers = new List<Controller>();
		playerControllers = new List<Controller>();

		foreach (Gamepad gamepad in Gamepad.all)
		{
			if (gamepad.enabled)
			{
				potentialControllers.Add(new GamepadController(gamepad));
				//CreateRobotWithController(new GamepadController(gamepad));
			}
		}

		//if(players.Count == 0)
		//	CreateRobotWithController(new KeyboardController());
		potentialControllers.Add(new KeyboardController());

		combatLoad = SceneManager.LoadSceneAsync(nextSceneName);
		combatLoad.allowSceneActivation = false;

	}

    // Update is called once per frame
    void Update()
    {
		if(!hasStarted)
		{
			joinTime -= Time.deltaTime;
			List<Controller> listCopy = new List<Controller>(potentialControllers);
			foreach(Controller controller in listCopy)
			{
				if (controller.IsDoingPickUp())
				{
					CreateRobotWithController(controller);
					potentialControllers.Remove(controller);
					playerControllers.Add(controller);
				}
			}

			if (joinTime <= 0.0f)
			{
				hasStarted = true;

				SpawnParts();

				scavengeTime = TotalScavengeTime;

				foreach(ScavengerRobot robot in players)
					robot.enabled = true;

				joinImage.SetActive(false);
			}

			return;
		}

		scavengeTime -= Time.deltaTime;

		timerText.text = string.Format("{0:F1}", scavengeTime);

		if (scavengeTime <= 0.0f && !scavengingFinished)
		{
			staticPlayerPartList = new List<List<Part>>();

			foreach (ScavengerRobot robot in players)
			{
				staticPlayerPartList.Add(new List<Part>());
				foreach (Part part in robot.GetCollectedParts())
				{
					part.transform.SetParent(null);
					DontDestroyOnLoad(part.gameObject);
					staticPlayerPartList[staticPlayerPartList.Count - 1].Add(part);

				}
			}
			combatLoad.allowSceneActivation = true;
			scavengingFinished = true;
		}
	}

	private void SpawnParts()
	{
		int numWeapons = (int)Random.Range(players.Count * 1.5f, players.Count * 3.0f);
		int numLegs = (int)Random.Range(players.Count * 1.5f, players.Count * 2.0f);
		int numOther = (int)Random.Range(players.Count * 1.5f, players.Count * 3.0f);

		for (int i = 0; i < numWeapons; ++i)
			SpawnRandomPart(weaponPrefabList);

		for (int i = 0; i < numLegs; ++i)
			SpawnRandomPart(legsPrefabList);

		for (int i = 0; i < numOther; ++i)
			SpawnRandomPart(otherPrefabList);
	}

	private void SpawnRandomPart(List<GameObject> partPrefabs)
	{
		int partIdx = Random.Range(0, partPrefabs.Count);
		GameObject pickupPartObj = Instantiate(pickupPartPrefab);
		GameObject partObj = Instantiate(partPrefabs[partIdx], pickupPartObj.transform);
		Vector3 spawnLoc = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
		spawnLoc = partSpawn.transform.TransformPoint(spawnLoc);
		
		pickupPartObj.transform.position = spawnLoc;
		partObj.transform.localScale *= 10.0f;
		partObj.transform.localPosition = new Vector3(0, 0, 0);
	}

	private void CreateRobotWithController(Controller controller)
	{
		GameObject robotObj = Instantiate(scavengerPrefabs[players.Count]);

		ScavengerRobot robot = robotObj.GetComponent<ScavengerRobot>();
		robot.enabled = false;
		Debug.Assert(robot != null);
		robot.SetController(controller);
		robot.transform.position = playerSpawns[players.Count].position;
		Collider partDropCollider = playerDrops[players.Count].GetComponent<Collider>();
		Debug.Assert(partDropCollider != null);
		robot.SetPartDrop(partDropCollider);
		players.Add(robot);
	}
}

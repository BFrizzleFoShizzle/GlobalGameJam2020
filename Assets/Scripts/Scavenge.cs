using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scavenge : MonoBehaviour
{
	public List<Transform> playerSpawns;
	public GameObject scavengerPrefab;
	public GameObject pickupPartPrefab;
	public GameObject partSpawn;
	// parts lists
	public List<GameObject> weaponPrefabList;
	public List<GameObject> legsPrefabList;
	public List<GameObject> otherPrefabList;
	// TODO how does this get set?
	private List<List<Part>> playerParts;
	private List<ScavengerRobot> players;

	// Start is called before the first frame update
	void Start()
    {
		Debug.Assert(scavengerPrefab != null);
		Debug.Assert(pickupPartPrefab != null);
		Debug.Assert(partSpawn != null);


		players = new List<ScavengerRobot>();

		foreach (Gamepad gamepad in Gamepad.all)
		{
			if (gamepad.enabled)
			{
				GameObject robotObj = Instantiate(scavengerPrefab);

				ScavengerRobot robot = robotObj.GetComponent<ScavengerRobot>();
				Debug.Assert(robot != null);
				robot.SetController(new GamepadController(gamepad));
				robot.transform.position = playerSpawns[players.Count].position;
				players.Add(robot);
			}
		}

		SpawnParts();
	}

    // Update is called once per frame
    void Update()
    {
        
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
}

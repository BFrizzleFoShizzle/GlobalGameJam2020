using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
	public GameObject robotPrefab;
	// TODO how does this get set?
	private List<List<Part>> playerParts;

	private List<Robot> players;

	// test code
	public List<GameObject> debugPartPrefabs;

    // Start is called before the first frame update
    void Start()
    {
		GenerateRandomRobots();

		for (int i=0;i<playerParts.Count; ++i)
		{
			GameObject robotObj = Instantiate(robotPrefab);

			Robot robot = robotObj.GetComponent<Robot>();
			Debug.Assert(robot != null);

			foreach (Part part in playerParts[i])
			{
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
		playerParts.Add(new List<Part>());

		for(int i=0; i<2; ++i)
		{
			int partIdx = Random.Range(0, debugPartPrefabs.Count);
			GameObject partObj = Instantiate(debugPartPrefabs[partIdx]);
			Part part = partObj.GetComponent<Part>();
			Debug.Assert(part != null);
			playerParts[0].Add(part);
		}

	}
}

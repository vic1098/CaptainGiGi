using System.Collections.Generic;
using UnityEngine;

public class BackgroundWarehouse : Factory
{
    [SerializeField] private Transform bgStart;
    private Transform bgParent;
    public Vector3 bgEnd_Right;
    public Vector3 bgEnd_Left; 
    private float distanceToSpawnBackground;
    private GameObject player;
    private Transform initBg;

    private void Awake()
    {

    }

    private void Start()
    {

        // Hierarchy Parent
        bgParent = GameObject.Find("Backgrounds_Active").transform;

        // First spawn point
        bgEnd_Right = bgStart.transform.position;


    }

    public void GenerateBackground_Right()
    {
        //int randomPick = UnityEngine.Random.Range(0, backgrounds.Count - 1);
        int randomPick = 0;
        Transform randomBg = backgrounds[randomPick].transform;

        // Spawn the Transform at the last end of section location
        Transform lastBgEnd_Right = GenerateBackground_Right(randomBg, bgEnd_Right, bgParent);

        // Find the next end of section in the new Transform
        bgEnd_Right = lastBgEnd_Right.Find("BgEnd_Right").position;
    }

    public Transform GenerateBackground_Right(Transform background, Vector3 spawnLocation, Transform backgroundParent)
    {
        //int randomBackground = UnityEngine.Random.Range(0, backgrounds.Count - 1);
        Transform bg = Instantiate(background, spawnLocation, Quaternion.identity, backgroundParent);
        Debug.Log("background generated");
        return bg;
    }

    
    public void GenerateBackground_Left()
    {
        //int randomPick = UnityEngine.Random.Range(0, backgrounds.Count - 1);
        int randomPick = 0;
        Transform randomBg = backgrounds[randomPick].transform;

        // Spawn the Transform at the last end of section location
        Transform lastBgEnd_Left = GenerateBackground_Left(randomBg, bgEnd_Right, bgParent);

        // Find the next end of section in the new Transform
        bgEnd_Right = lastBgEnd_Left.Find("BgEnd_Right").position;
    }

    public Transform GenerateBackground_Left(Transform background, Vector3 spawnLocation, Transform backgroundParent)
    {
        //int randomBackground = UnityEngine.Random.Range(0, backgrounds.Count - 1);
        Transform bg = Instantiate(background, spawnLocation, Quaternion.identity, backgroundParent);
        Debug.Log("background generated");
        return bg;
    }




}






























    /*
    // Grab Enemy Prefabs
    private List<GameObject> trapList = new List<GameObject>();
    private List<Vector3> trapSpawnPositions = new List<Vector3>();
    private List<GameObject> trapSpawnMarkers = new List<GameObject>();

    // Grab Spawn Positions 
    private UnityEngine.Object[] initArrayOfTrapSpawnMarkers;
    private Transform parentObj;
    private GameObject trapObj;
    private Transform trapToSpawn;

    private ProceduralAI ai;

    // Start is called before the first frame update
    void Awake()
    {
        // Fetch list of Platform prefabs (Runs in Start to ensure list has been generated by other script on Awake)
        trapList = ProceduralAI.trapPrefabs;
        try
        {
            //enemySpawnMarkers = GenerateEnemySpawnMarkerList();
            trapSpawnPositions = GenerateTrapSpawnMarkerPositions();

        }
        catch (NullReferenceException nre)
        {
            Debug.Log(nre.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Spawner
    public void SpawnTraps()
    {
        parentObj = gameObject.transform;

        foreach (Transform childObj in parentObj)
        {
            if (childObj.CompareTag("TrapSpawn"))
            {
                trapSpawnPositions.Add(childObj.position);
            }
        }
        //Get the transform to refrence the Spawn Positions
        //Transform spawnedEnemy = SpawnEnemies(enemySpawnPositions);
        foreach (GameObject trapSpawnMarker in trapSpawnMarkers)
        {
            foreach (Vector3 trapSpawnPosition in trapSpawnPositions)
            {
                SpawnTraps(trapSpawnPosition);
            }
        }
    }

    // SpawnEnemies(AtPosition)
    public Transform SpawnTraps(Vector3 trapSpawnPosition)
    {
        // Get random Enemy from List
        trapToSpawn = RandomTrapGenerator(0, trapList.Count);

        // Get the Transform to Spawn Instance and log to controller + console
        Transform spawnTrap = Instantiate(trapToSpawn, trapSpawnPosition, Quaternion.identity);
        ProceduralAI.trapSpawned += 1;
        Debug.Log("Enemy Spawned: " + spawnTrap.name + ". Total Enemies spawned: " + ProceduralAI.trapSpawned);

        // Return enemy transform
        return spawnTrap;
    }
    #endregion

    // REGGIE
    private Transform RandomTrapGenerator(int floor, int ceiling)
    {
        // Variable to hold the index of random list element
        int randomTrap = 0;

        try
        {
            // Get random index for list
            randomTrap = UnityEngine.Random.Range(floor, ceiling);

            // Call GameObject from list and get its transform
            trapObj = trapList[randomTrap];
            trapToSpawn = trapObj.transform;

            // Return the randomly-chosen enemy to spawn
            return trapToSpawn;
        }
        catch (ArgumentException ae)
        {
            Debug.Log(ae.Message);

            if (trapList.Count == 0)
            {
                // Generate new Marker list
                trapList = ai.GenerateEnemyList();
            }

            // Get random index for list
            randomTrap = UnityEngine.Random.Range(floor, ceiling);

            // Call GameObject from list and get its transform
            trapObj = trapList[randomTrap];
            trapToSpawn = trapObj.transform;

            // Return the randomly-chosen enemy to spawn
            return trapToSpawn;
        }
    }

    private Vector3 RandomTrapSpawnPosition(int floor, int ceiling)
    {
        // Variable to hold the index of random list element
        int randomSpawnPoint = 0;

        // Get random index for list
        randomSpawnPoint = UnityEngine.Random.Range(floor, ceiling);

        // Call Vector from list and get its transform
        Vector3 spawnPoint = trapSpawnPositions[randomSpawnPoint];
        //enemyToSpawn = enemyObj.transform;

        // Return the randomly-chosen enemy to spawn
        return spawnPoint;
    }

    public List<Vector3> GenerateTrapSpawnMarkerPositions()
    {
        if (trapSpawnPositions.Count > 0)
        {
            trapSpawnPositions.Clear();

            try
            {
                parentObj = gameObject.transform;

                foreach (Transform childObj in parentObj)
                {
                    if (childObj.CompareTag("TrapSpawn"))
                    {
                        trapSpawnPositions.Add(childObj.position);
                    }
                }
            }
            catch (NullReferenceException nre)
            {
                Debug.Log("Null Reference Exception! : " + nre);
            }

            return new List<Vector3>(trapSpawnPositions);
        }
        else
        {
            try
            {
                parentObj = gameObject.transform;

                foreach (Transform childObj in parentObj)
                {
                    if (childObj.CompareTag("TrapSpawn"))
                    {
                        trapSpawnPositions.Add(childObj.position);
                    }
                }
            }
            catch (NullReferenceException nre)
            {
                Debug.Log("Null Reference Exception! : " + nre);
            }

            // Return new Enemy List
            return new List<Vector3>(trapSpawnPositions);
        }
    }

    public List<GameObject> GenerateTrapSpawnMarkerList()
    {
        try
        {
            parentObj = gameObject.transform;

            foreach (GameObject childObj in parentObj)
            {
                if (childObj.CompareTag("EnemySpawn"))
                {
                    trapSpawnMarkers.Add(childObj);
                }
            }
        }
        catch (NullReferenceException nre)
        {
            Debug.Log("Null Reference Exception! : " + nre);
            parentObj = gameObject.transform;

            foreach (GameObject childObj in parentObj)
            {
                if (childObj.CompareTag("EnemySpawn"))
                {
                    trapSpawnMarkers.Add(childObj);
                }
            }
        }

        return new List<GameObject>(trapSpawnMarkers);
    }

    private void OnDisable()
    {
        // Code here
    }

}
    */




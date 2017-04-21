using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }


    public int columns = GlobalParameters.numberTilesX;
    public int rows = GlobalParameters.numberTilesZ;
    public Count wallCount;
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] brickWallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] concreteWallTiles;
    public GameObject player;                         

    private Transform boardHolder;                                 
    private List<Vector3> gridPositions = new List<Vector3>();
    private float y = 0f;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            exit.SetActive(true);
        }
    }

    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int z = 1; z < rows - 1; z++)
            {
                if (x % 2 == 0 && z % 2 == 0)
                {
                    GameObject toInstantiate = concreteWallTiles[Random.Range(0, concreteWallTiles.Length)];
                    InstantiateGameObject(toInstantiate, x, z);
                }
                else
                {
                    gridPositions.Add(new Vector3(x, y, z));
                }
            }
        }
    }

    void InstantiateGameObject(GameObject toInstantiate, int x, int z)
    {
        GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, z), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = 0; x < columns + 1; x++)
        {
            for (int z = 0; z < rows + 1; z++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == 0 || x == columns || z == 0 || z == rows)
                    toInstantiate = concreteWallTiles[Random.Range(0, concreteWallTiles.Length)];

                InstantiateGameObject(toInstantiate, x, z);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    private void LayoutExitAndPoverUps(List<GameObject> powerUps, GameObject exitObject)
    {
        List<GameObject> brickWalls = new List<GameObject>();
        brickWalls.AddRange(GameObject.FindGameObjectsWithTag("Brick"));
        int i = 0;
        foreach (GameObject powerUp in powerUps)
        {
            Instantiate(powerUp, brickWalls[i].transform.position, Quaternion.identity);
            i++;
        }
        exit = Instantiate(exitObject, brickWalls[i].transform.position, Quaternion.identity);
        exit.SetActive(false);
    }
 
    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        wallCount = new Count(13, 15);
        LayoutObjectAtRandom(brickWallTiles, wallCount.minimum, wallCount.maximum);
        GameObject playerObject = new BaseObjectCreator().GetPlayer();
        LayoutObjectAtRandom(new GameObject[] {playerObject}, 1, 1);
        enemyTiles = new GameObject[] {
            new BaseObjectCreator().GetEnemyByName("Oneal"),
            new BaseObjectCreator().GetEnemyByName("Balloom")
        };
        LayoutObjectAtRandom(enemyTiles, 1, 5);

        List<GameObject> powerUps = new List<GameObject>();
        powerUps.Add((Resources.Load(@"PowerUps\Wallpass") as GameObject));
        powerUps.Add((Resources.Load(@"PowerUps\Bombs (PowerUp)") as GameObject));
        powerUps.Add((Resources.Load(@"PowerUps\Speed") as GameObject));
        powerUps.Add((Resources.Load(@"PowerUps\Flames") as GameObject));
        powerUps.Add((Resources.Load(@"PowerUps\Detonator") as GameObject));
        exit = (Resources.Load(@"Exit") as GameObject);
        LayoutExitAndPoverUps(powerUps, exit);
        Instantiate(Resources.Load(@"Canvas") as GameObject, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

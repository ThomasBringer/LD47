using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public float forwardStep = 2;
    [SerializeField] public float worldWidth = 8;
    [SerializeField] float doorWidth = 4;
    [SerializeField] float verticalCallProbability = .5f;

    [SerializeField] GameObject wallPrefab;

    [SerializeField] float wallCountAtStart = 10;

    [HideInInspector] public float semiWorldWidth;
    float spawnPoint = 0;
    Vector2 verticalPos;

    float doorCenter;
    float prevDoorCenter = 0;

    Vector2 verticalWallPos;

    List<GameObject> walls;

    bool firstTime = true;

    public void Spawn()
    {


        doorCenter = Random.Range(-semiWorldWidth + doorWidth * .5f, semiWorldWidth - doorWidth * .5f);
        //GameObject wallInstance = Instantiate(wallPrefab);
        verticalPos = Vector2.up * (spawnPoint + forwardStep * .5f);
        SpawnWall(new Vector2[] { -Vector2.right * semiWorldWidth + verticalPos, Vector2.right * (doorCenter - doorWidth * .5f) + verticalPos });
        SpawnWall(new Vector2[] { Vector2.right * (doorCenter + doorWidth * .5f) + verticalPos, Vector2.right * semiWorldWidth + verticalPos });

        float doorDifference = doorCenter - prevDoorCenter;
        if (VerticalWallProbability && !firstTime)
        {
            if (doorDifference <= -doorWidth) { verticalWallPos = Vector2.right * Random.Range(doorCenter + doorWidth * .5f, prevDoorCenter - doorWidth * .5f); SpawnVerticalWall(); }
            if (doorDifference >= 0) { verticalWallPos = Vector2.right * Random.Range(doorCenter + doorWidth * .5f, semiWorldWidth); SpawnVerticalWall(); }
        }
        //SpawnWall(new Vector2[] { Vector2.left * semiWorldWidth + verticalPos-Vector2.up*forwardStep, Vector2.left * (doorCenter + doorWidth * .5f) + verticalPos });

        spawnPoint += forwardStep;
        prevDoorCenter = doorCenter;

        firstTime = false;
    }

    void SpawnWall(Vector2[] positions)
    {
        GameObject wall = Instantiate(wallPrefab);

        wall.GetComponent<LineRenderer>().SetPositions(positions.ToVector3Array());
        wall.GetComponent<EdgeCollider2D>().SetPoints(new List<Vector2>(positions));

        walls.Add(wall);
    }

    void SpawnVerticalWall()
    {
        SpawnWall(new Vector2[] { verticalWallPos + verticalPos - Vector2.up * forwardStep, verticalWallPos + verticalPos });
    }

    bool VerticalWallProbability
    {
        get { return verticalCallProbability.Probability(); }
    }

    public void Restart()
    {
        RemoveAllWalls();

        spawnPoint = 0;

        firstTime = true;

        for (int i = 0; i < wallCountAtStart; i++)
        {
            Spawn();
        }
    }

    void RemoveAllWalls()
    {
        if (walls != null) foreach (var wall in walls) Destroy(wall);

        walls = new List<GameObject>();
    }

    public static Spawner spawner;

    void Awake()
    {
        spawner = this;
        semiWorldWidth = worldWidth * .5f;
    }

    void Start()
    {
        Restart();

        //walls = new List<GameObject>();

        //for (int i = 0; i < wallCountAtStart; i++)
        //{
        //    Spawn();
        //}
    }
}
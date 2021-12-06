using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
/// 
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { private set; get; }

    public BaseEnemy[] EnemyTypes; //Enemy Pool to instantiate
    public GameObject EnemySpawnPoint;

    public int rows = 3; //instantiate enemies on a grid
    public int columns = 6;

    public float spacing; //space between enemies row and col wise

    public Vector3 movDirection = Vector2.right; //direction and speed the enemies will move
    public float speed;

    private void Awake()
    {
        rows = EnemyTypes.Length;

        for (int row = 0; row < rows; row++)
        {
            float width = spacing * (columns - 1); 
            float height = spacing * (rows - 1);

            Vector2 gridPivot = new Vector2(-width / 2, -height / 2); // obtains the center of the gameobject that our enemies will be parented to.

            Vector3 rowPos = new Vector3(gridPivot.x, gridPivot.y + (row * spacing), 0f);

            for (int col = 0; col < columns; col++)
            {
                BaseEnemy Enemy = Instantiate(EnemyTypes[row], EnemySpawnPoint.transform);
                Vector3 pos = rowPos;
                pos.x += col * spacing;
                Enemy.transform.localPosition = pos;
            }
        }
    }

    private void Start()
    {
        if(Instance != this || Instance == null)
        {
            Instance = this;
        }
    }


    private void Update()
    {
        EnemySpawnPoint.transform.position += movDirection * speed * Time.deltaTime;

        Vector3 leftBound = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform enemy in EnemySpawnPoint.transform)
        {
            if(movDirection == Vector3.right && enemy.position.x >= (rightBound.x - .16f))
            {
                SwapOrientation();
            }
            else if (movDirection == Vector3.left && enemy.position.x <= (leftBound.x + .16f))
            {
                SwapOrientation();
            }
        }
    }

    public void SwapOrientation() //Swap orientation of movement and the swarm advances one spot towards the player
    {
        movDirection.x *= -1f;

        var curPos = EnemySpawnPoint.transform.position;
        curPos.y -= .1f;

        EnemySpawnPoint.transform.position = curPos;
    }
}

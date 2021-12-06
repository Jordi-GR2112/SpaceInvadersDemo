using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    private int CurrentLevel;
    private int CurrentScore;
    private int BestScore;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

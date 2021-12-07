using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    private int CurrentLevel; //Current match values
    private int CurrentScore;
    private int BestScore;
    private bool isGameStarted = false;

    public TMP_Text uiScore; //GUI elements for current match value display
    public TMP_Text uiLevel;

    public GameObject GameOverPanel;//GameOver screen variables
    public TMP_Text FinalScore;

    public GameObject PlayerPrefab;
    public GameObject PlayerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        if (isGameStarted) { return; }

        var player = Instantiate(PlayerPrefab, PlayerSpawn.transform.localPosition,Quaternion.identity, PlayerSpawn.transform);

        CurrentLevel = 1;
        CurrentScore = 0;

        UpdateOnScreenScore(); 
        uiLevel.text = $"Level {CurrentLevel}";

        EnemySpawner.Instance.InstantiateEnemies();

    }

    public void UpdateScore(int earnedPoints)
    {
        CurrentScore += earnedPoints;
        UpdateOnScreenScore();
        CheckLevelStatus();
    }

    public void CheckLevelStatus()
    {
        if(EnemySpawner.Instance.ReturnEnemiesLeft() <= 1)
        {
            EnemySpawner.Instance.InstantiateEnemies();
            CurrentLevel += 1;
            UpdateOnScreenLevel();
        }
    }


    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        FinalScore.text = $"Player Score: {CurrentScore}";
        EnemySpawner.Instance.ClearEnemies();
        //ResetScoring();
    }


    public bool GameStatus() { return isGameStarted; }
    public void UpdateOnScreenScore() { uiScore.text = $"Score: {CurrentScore}"; }
    public void UpdateOnScreenLevel() { uiLevel.text = $"Level {CurrentLevel}"; }


    public void ResetScoring()
    {
        CurrentScore = 0;
        CurrentLevel = 1;

        UpdateOnScreenScore();
        UpdateOnScreenLevel();
    }

}

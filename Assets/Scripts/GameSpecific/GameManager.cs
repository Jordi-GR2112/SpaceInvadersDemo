using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

/// <summary>
/// Code made by Jordi Gonzalez Ramos
/// This code is part of a test for a job application for an Unity developer position.
/// </summary>
/// 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    [Header("Score")]
    private int CurrentLevel; //Current match values
    private int CurrentScore;
    private int BestScore;
    private bool isGameStarted = false;

    public TMP_Text uiScore; //GUI elements for current match value display
    public TMP_Text uiLevel;

    [Header ("Game over screen")]
    public GameObject GameOverPanel;//GameOver screen variables
    public TMP_Text FinalScore;

    [Header("Player assets")]
    public GameObject PlayerPrefab;
    public GameObject PlayerSpawn;

    [Header("Background settings")]//Json from we'll retrieve background level info
    public TextAsset levelBGs;
    public Image gameBackground;
    private string jsonLocation;
    public Sprite DefaultImage;
    public List<string> lvlSettings;
    public JObject rawData;

    [Header("Game SFX")]
    public AudioClip shoot;
    public AudioClip enemyDamaged;
    public AudioClip killEnemy;
    public AudioClip Death;
    public AudioSource AudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null || Instance != this)
        {
            Instance = this;
        }

        jsonLocation = Application.persistentDataPath + "/LEVELS.json";
        ReadJSON();
    }

    #region Json file reading
    public void ReadJSON()
    {
        string jsonData;

        if (File.Exists(jsonLocation))
        {
            jsonData = File.ReadAllText(jsonLocation);
        }
        else
        {
            jsonData = levelBGs.text;
        }
        try
        {
            rawData = JObject.Parse(jsonData);
            FillLevelList();
        }
        catch(Exception ex)
        {
            Debug.Log($"Invalid json structure: {ex.Message}");
        }
    }

    void FillLevelList()
    {
        try
        {
            lvlSettings.Clear();
            var imagePath = rawData["levels"].Value<JArray>();
            var paths = imagePath.Values("path").ToList();
            foreach (var path in paths)
            {
                lvlSettings.Add(path.ToString());
            }
        }
        catch(Exception ex)
        {
            Debug.Log($"Error: {ex.Message}");
        }
    }
    #endregion

    public void StartGame()
    {
        if (isGameStarted) { return; }
        
        var player = Instantiate(PlayerPrefab, PlayerSpawn.transform.localPosition,Quaternion.identity, PlayerSpawn.transform);

        CurrentLevel = 1;
        CurrentScore = 0;

        UpdateBackground(CurrentLevel);

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
            UpdateBackground(CurrentLevel);
            UpdateOnScreenLevel();
        }
    }

    public void UpdateBackground(int level)
    {
        if(lvlSettings.Count <= 0) 
        {
            gameBackground.sprite = DefaultImage;
            return; 
        }else if(level > lvlSettings.Count)
        {
            int index = UnityEngine.Random.Range(0, lvlSettings.Count);
            var spr = Resources.Load<Sprite>(lvlSettings[index]);
            gameBackground.sprite = spr;
            return;
        }

        var sprite = Resources.Load<Sprite>(lvlSettings[level - 1]);
        gameBackground.sprite = sprite;
    }


    public void GameOver()
    {
        PlayGameOver();
        GameOverPanel.SetActive(true);
        FinalScore.text = $"Player Score: {CurrentScore}";
        EnemySpawner.Instance.ClearEnemies();
        //ResetScoring();
    }


    public bool GameStatus() { return isGameStarted; }
    public void UpdateOnScreenScore() { uiScore.text = $"Score: {CurrentScore}"; }
    public void UpdateOnScreenLevel() { uiLevel.text = $"Level {CurrentLevel}"; }
    public int RetrieveCurrentLevel() { return CurrentLevel; }
    public void PlayShootingPlayer() { AudioSrc.PlayOneShot(shoot); }
    public void PlayEnemyDamaged() { AudioSrc.PlayOneShot(enemyDamaged); }
    public void PlayEnemyDeath() { AudioSrc.PlayOneShot(killEnemy); }
    public void PlayGameOver() { AudioSrc.PlayOneShot(Death); }

    public void ResetScoring()
    {
        CurrentScore = 0;
        CurrentLevel = 1;

        UpdateOnScreenScore();
        UpdateOnScreenLevel();
    }

}

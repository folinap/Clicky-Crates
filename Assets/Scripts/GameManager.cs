using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> _targets;
    [SerializeField]private TextMeshProUGUI _scoreText;
    [SerializeField]private TextMeshProUGUI _gameoverText;
    [SerializeField]private TextMeshProUGUI _livesText;
    [SerializeField]private Button _restartButton;
    [SerializeField]private GameObject _titleScreen;
    [SerializeField]private GameObject _pauseScreen;
    private int _score;
    private int _lives;
    private bool _gamePaused;
    private float _spawnRate = 1.0f;


    public static GameManager Instance { get; private set; }
    
    public bool IsGameActive { get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            TogglePause();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, _targets.Count);
            Instantiate(_targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = "Score: " + _score;
        _livesText.text = "Lives: " + _lives;
    }

    public void GameOver()
    {
        _restartButton.gameObject.SetActive(true);
        _gameoverText.gameObject.SetActive(true);
        IsGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        _spawnRate /= difficulty;
        IsGameActive = true;
        StartCoroutine(SpawnTarget());
        _score = 0;
        _lives = 3;
        _livesText.text = "Lives: " + _lives;
        UpdateScore(0);
        _titleScreen.gameObject.SetActive(false);
    }

    public void UpdateLives(int livesToUpdate)
    {
        _lives += livesToUpdate;
        if(_lives<= 0 )
        {
             _lives = 0;
             GameOver();
        }
        _livesText.text = "Lives: " + _lives;
    }

    private void TogglePause()
    {
        _gamePaused = !_gamePaused;
        IsGameActive = !IsGameActive;

        if( _gamePaused )
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        _pauseScreen.SetActive(_gamePaused);
    }
}

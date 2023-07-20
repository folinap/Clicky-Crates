using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;

    [SerializeField]private List<GameObject> targets;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private TextMeshProUGUI gameoverText;
    [SerializeField]private TextMeshProUGUI livesText;
    [SerializeField]private Button restartButton;
    [SerializeField]private GameObject titleScreen;
    [SerializeField]private GameObject pauseScreen;
    private int score;
    private int lives;
    private bool gamePaused;
    private float spawnRate = 1.0f;
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            TogglePause();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
       
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 3;
        livesText.text = "Lives: " + lives;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    public void UpdateLives(int livesToUpdate)
    {
        lives += livesToUpdate;
        if(lives<= 0 )
        {
             lives = 0;
             GameOver();
        }
        livesText.text = "Lives: " + lives;

    }

    private void TogglePause()
    {
        gamePaused = !gamePaused;
        isGameActive = !isGameActive;

        if( gamePaused )
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseScreen.SetActive(gamePaused);
    }
}

using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField]private int difficulty;
    [SerializeField]private GameManager gameManager;

    public void SetDifficulty()
    {
        gameManager.StartGame(difficulty);  
    }
}

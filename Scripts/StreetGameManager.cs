using UnityEngine;
using UnityEngine.UI;

public class StreetGameManager : MonoBehaviour
{
    public int playerScore;
    public int opponentScore;
    public Text scoreText;

    void Start() => RefreshScore();

    public void AddTouchdown(int points)
    {
        playerScore += points;
        RefreshScore();
        Debug.Log("TOUCHDOWN! Player score: " + playerScore);
    }

    public void AddOpponentScore(int points)
    {
        opponentScore += points;
        RefreshScore();
    }

    void RefreshScore()
    {
        if (scoreText) scoreText.text = playerScore + "  -  " + opponentScore;
    }
}

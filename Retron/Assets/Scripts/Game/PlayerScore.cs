using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public float score = 0;
    void Update()
    {
        if (!player.isDead)
        {
            score += 4.7f * Time.deltaTime;
            scoreText.text = "SCORE: " + (int)score;
        }
    }
    public int ReadBestScore()
    {
        int bestScore = 0;
        if (!PlayerPrefs.HasKey("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", (int)score);
            bestScore = (int)score;
        }
        else
        {
            bestScore = PlayerPrefs.GetInt("bestScore");
            if (bestScore < (int)score)
            {
                PlayerPrefs.SetInt("bestScore", (int)score);
                bestScore = (int)score;
            }
        }
        return bestScore;
    }
}



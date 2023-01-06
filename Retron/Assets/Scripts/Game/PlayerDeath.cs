using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private List<GameObject> showObjects;
    [SerializeField]
    private List<GameObject> hideObjects;
    [SerializeField]
    private PlayerScore score;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;

    void OnCollisionEnter (Collision collision)
    {
        if (!player.isDead)
        {
            if (collision.collider.tag == "Obstacle")
            {
                DoDeath();
            }
        }
    }

    void Update()
    {
        if (!player.isDead)
        {
            if (player.transform.position.y <= -2f)
            {
                DoDeath();
            }
        }
    }

    void DoDeath()
    {
        player.KillPlayer();

        int bestScore = score.ReadBestScore();

        for (int i = 0; i < showObjects.Count; i++)
        {
            showObjects[i].SetActive(true);
        }
        for (int i = 0; i < hideObjects.Count; i++)
        {
            hideObjects[i].SetActive(false);
        }

        bestScoreText.text = "Your best score: " + bestScore;
    }
}

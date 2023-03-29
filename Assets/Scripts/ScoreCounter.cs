using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public TextMesh text;
    private int score = 0;

    void Update()
    {
        int latestScore = GameController.GetScore();
        if (score != latestScore)
        {
            score = latestScore;
            text.text = score.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LastScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textLastScore;

    private void Awake()
    {
        textLastScore = GetComponent<TextMeshProUGUI>();
        int score = PlayerPrefs.GetInt("Score");
        textLastScore.text = "Last Score : " + score;
    }
}

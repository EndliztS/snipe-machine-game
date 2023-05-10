using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    [SerializeField] TMP_Text scoreText;

    void Update() {
        scoreText.text = score.ToString();
    }
}

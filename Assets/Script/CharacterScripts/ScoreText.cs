using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    void Start()
    {
        Enemy.scoreText= GetComponent<TextMeshProUGUI>();
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBasedScore : MonoBehaviour
{
    private float score = 0f;

    public TMP_Text scoreText;
    public Canvas canvas;

    void Update()
    {
        score += Time.deltaTime;

        if (scoreText != null && !canvas.gameObject.activeSelf)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }
    public void ResetScore()
    {
        score = 0f;
    }
}

using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        scoreText.text = "Score: " + Manager.instance.score;

    }
    void Update()
    {
        scoreText.text = "Score: " + Manager.instance.score;
    }
}

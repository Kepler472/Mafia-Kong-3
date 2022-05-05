using UnityEngine;
using UnityEngine.UI;
using System;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    public static HeartManager instance;

    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        instance = this;
        Array.Reverse(hearts);
        UpdateHeartBar();
    }

    // Update is called once per frame
    public void UpdateHeartBar()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Manager.instance.GetLives())
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private TMP_Text text;
    private bool isPlaying = true;
    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.PlayerWins += PlayerWins;
        Castle.Destroyed += PlayerLose;
        alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
            return;

        alpha += speed * Time.deltaTime;
        canvas.alpha = alpha;
    }

    private void PlayerWins() {
        text.text = "You win";
        isPlaying = false;
    }

    private void PlayerLose() {
        text.text = "You lose";
        isPlaying = false;
    }
}

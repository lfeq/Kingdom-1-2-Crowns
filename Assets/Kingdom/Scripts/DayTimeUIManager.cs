using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTimeUIManager : MonoBehaviour
{
    [SerializeField] private Image dayTimeIcon;
    [SerializeField] private TMP_Text dayTimeText;
    [SerializeField] private Sprite dayIcon, nightIcon;
    private float dayTime, nightTime, countdownTime;

    private void OnEnable()
    {
        GameManager.DayArrived += SetDayIcon;
        GameManager.NightArrived += SetNightIcon;
        dayTime = GameManager.GetDayTime();
        nightTime = GameManager.GetnightTime();
    }

    private void Update()
    {
        countdownTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        dayTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SetDayIcon()
    {
        dayTimeIcon.sprite = dayIcon;
        countdownTime = dayTime;
    }

    private void SetNightIcon()
    {
        dayTimeIcon.sprite = nightIcon;
        countdownTime = nightTime;
    }
}

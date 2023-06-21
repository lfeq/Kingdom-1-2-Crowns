using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public List<GameObject> targets;
    public AudioSource audioSource;
    public AudioClip dayTimeClip, nightTimeClip;
    [SerializeField, Range(10f, 30f)] private float dayTime = 10f;
    [SerializeField, Range(10f, 30f)] private float nightTime = 10f;
    public delegate void MyDelegate();
    public static event MyDelegate NightArrived;
    public static event MyDelegate DayArrived;
    public static event MyDelegate DestroyedTarget;
    public static event MyDelegate PlayerWins;
    private static float _daytime, _nighttime;

    private void Awake()
    {
        _nighttime = nightTime;
        _daytime = dayTime;      
    }

    void Start()
    {
        player.Dead += OnPlayerKilled;
        Castle.Destroyed += OnPlayerKilled;
        StartCoroutine(RunDayTime());
    }

    IEnumerator RunDayTime()
    {
        audioSource.clip = dayTimeClip;
        audioSource.Play();
        DayArrived();
        yield return new WaitForSeconds(dayTime);
        StartCoroutine(RunNightTime());
    }

    IEnumerator RunNightTime()
    {
        audioSource.clip = nightTimeClip;
        audioSource.Play();
        NightArrived();
        yield return new WaitForSeconds(nightTime);
        PlayerWins();
        StartCoroutine(RunDayTime());
    }

    private void OnPlayerKilled()
    {
        Debug.Log("You Died");
        player.gameObject.SetActive(false);
    }

    public static float GetDayTime()
    {
        return _daytime;
    }

    public static float GetnightTime()
    {
        return _nighttime;
    }

    public void InvokeDestroyTarget() {
        DestroyedTarget.Invoke();
    }
}

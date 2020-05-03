﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int count;
    public Text display;
    public bool active = true;

    private int timer;

    void Awake()
    {
        ResetTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnWin += DoOnWin;

        SetActive();
        StartCoroutine(CountdownStart());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DoOnWin()
    {
        SetInactive();
        ResetTimer();
        UpdateText();
    }

    private IEnumerator CountdownStart()
    {
        while (IsRunning())
        {
            if (!active)
            {
                yield break;
            }
            UpdateText();
            yield return new WaitForSeconds(1f);
            DecrementTimer();
        }
        UpdateText();
        GameEvents.instance.TriggerCountEnd();
    }

    private void ResetTimer()
    {
        timer = count;
    }

    private void DecrementTimer()
    {
        timer--;
    }

    private bool IsRunning()
    {
        return timer > 0;
    }

    private void UpdateText()
    {
        display.text = timer.ToString();
    }

    private void SetActive()
    {
        active = true;
    }

    private void SetInactive()
    {
        active = false;
    }
}

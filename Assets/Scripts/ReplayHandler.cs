﻿using UnityEngine;

public class ReplayHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleClick()
    {
        // GameEvents.instance.TriggerRemember();
        GameEvents.instance.TriggerPrepare();
    }
}

﻿using System;
using System.Linq;
using UnityEngine;

public class PrepareState : BaseGameState
{
    private Rotation[] prev = Array.Empty<Rotation>();

    public PrepareState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    protected override void DoOnStart()
    {
        game.GameOverlay.SetActive(true);
        GameEvents.instance.OnCountEnd += DoOnCountEnd;
        Restart();
    }

    protected override void DoOnUpdate()
    {
    }

    private bool IsSameAsBefore(Rotation[] rotations)
    {
        return Enumerable.SequenceEqual(rotations, prev);
    }

    private void Restart()
    {
        Rotation[] level = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        while (IsSameAsBefore(level))
        {
            level = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        }
        prev = level;


        GameStore.instance.SetLevel(level);
        GameStore.instance.SetTriangles(GenerateTriangles(GameStore.instance.level));

        LogUtils.LogState(GetType().Name);
    }

    public override void Unbind()
    {
        game.GameOverlay.SetActive(false);
        GameEvents.instance.OnCountEnd -= DoOnCountEnd;
    }

    private void DoOnCountEnd()
    {
        GameStore.instance.SetReady(true);
        GameEvents.instance.TriggerPrepareEnd();
    }
}

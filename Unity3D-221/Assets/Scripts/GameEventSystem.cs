using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem {
    private static readonly List<Action<GameEvent>> subscribers =
        new List<Action<GameEvent>>();

    public static void Subscribe(Action<GameEvent> action) {
        subscribers.Add(action);
    }
    public static void Unsubscribe(Action<GameEvent> action) {
        subscribers.Remove(action);
    }
    public static void EmitEvent(GameEvent gameEvent) {
        foreach (var action in subscribers) {
            action(gameEvent);
        }
    }
}

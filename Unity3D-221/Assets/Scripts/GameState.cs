using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static float _effectsVolume = 0.3f;
    public static float effectsVolume {
        get => _effectsVolume;
        set {
            if (_effectsVolume != value) {
                _effectsVolume = value;
                Notify(nameof(effectsVolume));
            }
        }
    }


    public static float _musicVolume = 0.01f;
    public static float musicVolume {
        get => _musicVolume;
        set {
            if (_musicVolume != value) {
                _musicVolume = value;
                Notify(nameof(musicVolume));
            }
        }
    }


    public static bool _isDay = true;
    public static bool isDay {
        get => _isDay;
        set {
            if (_isDay != value) {
                _isDay = value;
                Notify(nameof(isDay));
            }
        }
    }


    public static bool _isFpv = false;
    public static bool isFpv {
        get => _isFpv;
        set {
            if (_isFpv != value) {
                _isFpv = value;
                Notify(nameof(isFpv));
            }
        }
    }

    #region Change Notifier
    private static List<Action<string>> listeners = new List<Action<string>>();
    public static void AddListener(Action<string> listener) {
        listeners.Add(listener);
        listener(null);
    }
    public static void RemoveListener(Action<string> listener) {
        listeners.Remove(listener);
    }
    private static void Notify(string fieldName) {
        foreach (Action<string> listener in listeners) {
            listener.Invoke(fieldName);
        }
    }
    #endregion

    public static void SetProperty(string name, object value) {
        var prop = typeof(GameState).GetProperty(
                name,
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public
            );
        if (prop == null) {
            Debug.LogError($"Error prop setting. Name not found: '{name}' (value '{value}')");
        }
        else prop.SetValue(null, value);
    }
}

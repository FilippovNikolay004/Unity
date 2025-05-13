using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool _isKey1InTime = false;
    public static bool isKey1InTime {
        get => _isKey1InTime;
        set {
            if (_isKey1InTime != value) {
                _isKey1InTime = value;
                Notify(nameof(isKey1InTime));
            }
        }
    }

    public static bool _isKey1Collected = false;
    public static bool isKey1Collected {
        get => _isKey1Collected;
        set {
            if (_isKey1Collected != value) {
                _isKey1Collected = value;
                Notify(nameof(isKey1Collected));
            }
        }
    }



    public static bool _isKey2InTime = false;
    public static bool isKey2InTime {
        get => _isKey2InTime;
        set {
            if (_isKey2InTime != value) {
                _isKey2InTime = value;
                Notify(nameof(isKey2InTime));
            }
        }
    }

    public static bool _isKey2Collected = false;
    public static bool isKey2Collected {
        get => _isKey2Collected;
        set {
            if (_isKey2Collected != value) {
                _isKey2Collected = value;
                Notify(nameof(isKey2Collected));
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

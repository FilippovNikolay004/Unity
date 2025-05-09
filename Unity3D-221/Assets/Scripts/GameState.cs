using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool isKey1InTime { get; set; }


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

    public static bool _isKey2Collected = true;
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
}

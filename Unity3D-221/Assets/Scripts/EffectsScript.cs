using NUnit.Framework.Internal;
using UnityEngine;

public class EffectsScript : MonoBehaviour
{
    private AudioSource keyCollectedInTimeSound;
    private AudioSource keyCollectedOutOfTimeSound;
    private AudioSource batteryCollectedSound;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();

        keyCollectedInTimeSound = audioSources[0];
        batteryCollectedSound = audioSources[1];
        keyCollectedOutOfTimeSound = audioSources[2];
        
        GameEventSystem.Subscribe(OnGameEvent);
    }

    private void OnGameEvent(GameEvent gameEvent) {
        if (gameEvent.sound != null) {
            switch (gameEvent.sound) {
                case EffectSounds.batteryCollected: batteryCollectedSound.Play(); break;
                case EffectSounds.keyCollectedOutOfTime: keyCollectedOutOfTimeSound.Play(); break;
                default: keyCollectedInTimeSound.Play(); break;
            }
        }
    }
    private void OnDestroy() {
        GameEventSystem.Unsubscribe(OnGameEvent);
    }
}

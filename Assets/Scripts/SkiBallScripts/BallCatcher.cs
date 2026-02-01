using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallCatcher : MonoBehaviour
{
    public enum TeapotLevel
    {
        TOP,
        MIDDLE,
        BOTTOM,
        MISSED,
        OVER_THE_COUNTER
    }

    [SerializeField] private int mScoreValue = 1;
    [SerializeField] private BallCatcher.TeapotLevel level;
    [SerializeField] private GameObject _splash;

    [SerializeField] public UnityEvent ballCaughtEvent;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip scoreSound;
    [SerializeField, Range(0f, 1f)] private float volume = 0.5f;
    private AudioSource mAudioSource;

    private void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
        if (mAudioSource == null)
        {
            mAudioSource = gameObject.AddComponent<AudioSource>();
        }
        mAudioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ballScript = other.gameObject.GetComponent<Ball>();
        if (ballScript != null && !ballScript.HasGottenPoints)
        {
            if (scoreSound != null)
            {
                mAudioSource.PlayOneShot(scoreSound, volume);
            }

            Debug.Log(mScoreValue);
            ballScript.HasGottenPoints = true;
            EventSystem.AddSkiBallPoints(mScoreValue, level);

            Instantiate(_splash);

            ballCaughtEvent?.Invoke();
        }
    }
}

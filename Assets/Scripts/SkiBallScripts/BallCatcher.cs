using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    public enum TeapotLevel
    {
        TOP,
        MIDDLE,
        BOTTOM
    }

    [SerializeField] private int mScoreValue = 1;
    [SerializeField] private BallCatcher.TeapotLevel level;

    private void OnTriggerEnter(Collider other)
    {
        Ball ballScript = other.gameObject.GetComponent<Ball>();
        if (ballScript != null && !ballScript.HasGottenPoints)
        {
            Debug.Log(mScoreValue);
            ballScript.HasGottenPoints = true;
            EventSystem.AddSkiBallPoints(mScoreValue, level);
        }
    }
}

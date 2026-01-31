using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    [SerializeField] private int mScoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        Ball ballScript = other.gameObject.GetComponent<Ball>();
        if (ballScript != null && !ballScript.HasGottenPoints)
        {
            Debug.Log(mScoreValue);
            ballScript.HasGottenPoints = true;
            EventSystem.AddSkiBallPoints(mScoreValue);
        }
    }
}

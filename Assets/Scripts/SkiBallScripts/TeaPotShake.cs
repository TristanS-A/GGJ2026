using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPotShake : MonoBehaviour
{
    [SerializeField] private Animator mTeapotAnimator;
    [SerializeField] private float mTotalShakeTime = 1;
    [SerializeField] private float mShakeStrength = 1;
    [SerializeField] private float mRotationSpeed = 0.1f;
    [SerializeField] private float mRotationRange = 50f;
    [SerializeField] private float mRotationOfffset = 0f;
    [SerializeField] private AnimationCurve mRotationCurve;
    [SerializeField] private BallCatcher.TeapotLevel mLevel;
    private float mCurrTime = 0;

    private bool mRotationEnabled = true;

    private void OnEnable()
    {
        EventSystem.OnAddSkiballPoints += ShakeLevel;
    }

    private void OnDisable()
    {
        EventSystem.OnAddSkiballPoints -= ShakeLevel;
    }

    private void Awake()
    {
        if (mTeapotAnimator != null)
        {
            mTeapotAnimator.enabled = false;
        }
    }

    public void ShakeLevel(int points, BallCatcher.TeapotLevel levelToShake)
    {
        Debug.Log(levelToShake);
        mRotationEnabled = false;

        if (mTeapotAnimator != null)
        {
            mTeapotAnimator.enabled = true;
        }

        switch (levelToShake)
        {
            case BallCatcher.TeapotLevel.TOP:
                //StartCoroutine(Co_Shake(mTopLevel));
                mTeapotAnimator.SetTrigger("Top");
                break;
            case BallCatcher.TeapotLevel.MIDDLE:
                mTeapotAnimator.SetTrigger("Mid");
                break;
            case BallCatcher.TeapotLevel.BOTTOM:
                mTeapotAnimator.SetTrigger("Bottom");
                break;
        }

        StartCoroutine(Co_Shake(null));

    }

    private IEnumerator Co_Shake(Transform transform)
    {
        yield return new WaitForSeconds(0.5f);
        mRotationEnabled = true;

        if (mTeapotAnimator != null)
        {
            mTeapotAnimator.enabled = false;
        }

        //float currTime = 0;
        //Vector3 originalPos = transform.localPosition;

        //while (currTime < mTotalShakeTime)
        //{
        //    Vector3 randomOffset = Random.insideUnitSphere * mShakeStrength;
        //    transform.localPosition = originalPos + randomOffset;

        //    currTime += Time.deltaTime + 0.1f;

        //    yield return new WaitForSeconds(0.03f);
        //}

        //transform.localPosition = originalPos;
    }

    private void LateUpdate()
    {
        if (mRotationEnabled || mLevel == BallCatcher.TeapotLevel.BOTTOM)
        {
            transform.localEulerAngles = new Vector3(0, ((mRotationCurve.Evaluate(mCurrTime) * 2 - 1f) * mRotationRange) + mRotationOfffset, 0);
            mCurrTime += Time.deltaTime * mRotationSpeed;

            if (mCurrTime >= 1)
            {
                mCurrTime = 0;
            }
        }
    }
}

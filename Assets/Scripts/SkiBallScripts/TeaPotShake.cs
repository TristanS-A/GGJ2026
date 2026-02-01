using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPotShake : MonoBehaviour
{
    [SerializeField] private Animator mTopLevel;
    [SerializeField] private Animator mMidLevel;
    [SerializeField] private Animator mBottomLevel;
    [SerializeField] private float mTotalShakeTime = 1;
    [SerializeField] private float mShakeStrength = 1;

    private void OnEnable()
    {
        EventSystem.OnAddSkiballPoints += ShakeLevel;
    }

    private void OnDisable()
    {
        EventSystem.OnAddSkiballPoints -= ShakeLevel;
    }

    public void ShakeLevel(int points, BallCatcher.TeapotLevel levelToShake)
    {
        Debug.Log(levelToShake);
        switch (levelToShake)
        {
            case BallCatcher.TeapotLevel.TOP:
                //StartCoroutine(Co_Shake(mTopLevel));
                mTopLevel.SetTrigger("Top");
                break;
            case BallCatcher.TeapotLevel.MIDDLE:
                mMidLevel.SetTrigger("Mid");
                break;
            case BallCatcher.TeapotLevel.BOTTOM:
                mBottomLevel.SetTrigger("Bottom");
                break;
        }
    }

    private IEnumerator Co_Shake(Transform transform)
    {
        float currTime = 0;
        Vector3 originalPos = transform.localPosition;

        while (currTime < mTotalShakeTime)
        {
            Vector3 randomOffset = Random.insideUnitSphere * mShakeStrength;
            transform.localPosition = originalPos + randomOffset;

            currTime += Time.deltaTime + 0.1f;

            yield return new WaitForSeconds(0.03f);
        }

        transform.localPosition = originalPos;
    }
}

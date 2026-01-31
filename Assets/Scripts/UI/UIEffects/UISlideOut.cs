using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UISlideOut : MonoBehaviour
{
    [SerializeField] private UIType mUIType;
    [SerializeField] private Vector2 mStartingPos;
    [SerializeField] private Vector2 mEndingPos;
    [SerializeField] private float mTotalSlideTime;
    [SerializeField] private AnimationCurve mSlideCurve;

    private Vector2 mT0;
    private Vector2 mT1;

    private float mCurrTime = 0;

    public enum UIType
    {
        INGREDIENTS,
        TRAY
    }

    private void OnEnable()
    {
        EventSystem.OnSlideOutUI += StartSlide;
    }

    private void OnDisable()
    {
        EventSystem.OnSlideOutUI -= StartSlide;
    }

    private void StartSlide(UIType type, bool slidingIn)
    {
        //This way of checking UI is only acceptable because there arent too many UI elements
        // (otherwise would use UI manager for something like this)
        if (type == mUIType)
        {
            StartCoroutine(Co_Slide(slidingIn));
        }
    }

    private bool Slide()
    {
        mCurrTime += Time.deltaTime;

        float t = mCurrTime / mTotalSlideTime;


        transform.localPosition = Vector2.Lerp(mT0, mT1, mSlideCurve.Evaluate(t));

        return t >= 1;
    }

    private IEnumerator Co_Slide(bool slidingIn)
    {
        mCurrTime = 0;

        if (!slidingIn)
        {
            mT0 = mStartingPos;
            mT1 = mEndingPos;
        }
        else
        {
            mT0 = mEndingPos;
            mT1 = mStartingPos;
        }

        yield return new WaitUntil(Slide);
    }
}

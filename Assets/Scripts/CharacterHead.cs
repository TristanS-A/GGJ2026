using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UISlideOut;

public class CharacterHead : MonoBehaviour
{
    [SerializeField] private float mTotalEnterTime;
    [SerializeField] private AnimationCurve mEnterCurve;
    [SerializeField] private Vector3 m_TargetPos;
    private Vector3 mOriginalPos;

    Coroutine mCurrCoroutine = null;

    private Vector3 mT0;
    private Vector3 mT1;

    private float mCurrTime = 0;

    private void Awake()
    {
        mOriginalPos = m_TargetPos + Vector3.down * 50;
    }

    private void Start()
    {
        StartSlide(true);
    }

    private void Update()
    {
        //transform.LookAt(camer);
    }

    public void StartSlide(bool entering)
    {
        StartCoroutine(Co_Slide(entering));
    }

    private bool Slide()
    {
        mCurrTime += Time.deltaTime;

        float t = mCurrTime / mTotalEnterTime;

        transform.position = Vector3.Lerp(mT0, mT1, mEnterCurve.Evaluate(t));

        return t >= 1;
    }

    private IEnumerator Co_Slide(bool entering)
    {
        mCurrTime = 0;

        if (entering)
        {
            mT0 = mOriginalPos;
            mT1 = m_TargetPos;
        }
        else
        {
            mT0 = m_TargetPos;
            mT1 = mOriginalPos;
        }

        yield return new WaitUntil(Slide);

        if (!entering)
        {
            Destroy(gameObject);
        }
    }
}

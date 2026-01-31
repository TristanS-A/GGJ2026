using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float mThrowScaler = 0.5f;
    [SerializeField] private Transform mSlidePlaneT;


    private Vector3 mStartClickPos = Vector3.zero;
    private Vector3 mThrowDirection = Vector3.zero;

    private Rigidbody mRB;

    private bool mAiming = false;

    private bool t;
    private bool t1;

    private bool mReadyToThrow = false;
    private bool mHasGottenPoints = false;

    private IngredietSelector.IngredientType mType;

    public IngredietSelector.IngredientType IngredientType
    {
        get { return mType; }
        set { mType = value; }
    }

    public bool HasGottenPoints { 
        get { return mHasGottenPoints; }
        set { mHasGottenPoints = value; }
    }

    public void SetReadyToThrow()
    {
        mReadyToThrow = true;
    }

    private void Start()
    {
        mRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mReadyToThrow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mAiming = true;
                mStartClickPos = Input.mousePosition;
            }

            if (mAiming && Input.GetMouseButtonUp(0))
            {
                Vector3 dispVec = Input.mousePosition - mStartClickPos;
                float power = dispVec.magnitude;
                mThrowDirection = dispVec.normalized;

                Vector3 throwVec = new Vector3(-mThrowDirection.x * 0.6f, 0, -mThrowDirection.y);

                mRB.isKinematic = false;
                mRB.velocity = throwVec * power * mThrowScaler;

                mAiming = false;

                mReadyToThrow = false;
                StartCoroutine(Co_Delay());
            }
        }


        ////TEST CODE
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    EventSystem.SlideOutUI(UISlideOut.UIType.INGREDIENTS, t);
        //    t = !t;
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    EventSystem.SlideOutUI(UISlideOut.UIType.TRAY, t1);
        //    t1 = !t1;
        //}
    }

    private IEnumerator Co_Delay()
    {
        yield return new WaitForSeconds(3);
        EventSystem.CompletedBallThrow();
    }
}

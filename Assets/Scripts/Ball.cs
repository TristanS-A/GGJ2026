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

    private void Start()
    {
        mRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
        }
    }
}

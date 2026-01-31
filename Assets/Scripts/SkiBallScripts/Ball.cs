using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float mThrowScaler = 0.5f;
    [SerializeField] private Transform mSlidePlaneT;

    [Header("Audio")]
    [SerializeField] private AudioClip throwSound;
    [Range(0, 1)][SerializeField] private float volume = 0.5f;

    private Vector3 mStartClickPos = Vector3.zero;
    private Vector3 mThrowDirection = Vector3.zero;

    private Rigidbody mRB;
    private AudioSource mAudioSource;

    private bool mAiming = false;

    private bool t;
    private bool t1;

    private void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
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

            // Play the throw sound when released.
            if (throwSound != null)
            {
                float dynamicVolume = Mathf.Clamp(power / 500f, 0.2f, 1.0f) * volume;
                mAudioSource.PlayOneShot(throwSound, dynamicVolume);
            }

            mAiming = false;
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
}

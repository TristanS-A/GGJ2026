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
    [Range(0, 6)][SerializeField] private float volume = 0.5f;

    private Vector3 mStartClickPos = Vector3.zero;
    private Vector3 mThrowDirection = Vector3.zero;

    private Rigidbody mRB;
    private AudioSource mAudioSource;

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
        mAudioSource = GetComponent<AudioSource>();
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

                SkiballLine.instance.StartLine(gameObject);
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

                // Play the throw sound when released.
                if (throwSound != null)
                {
                    float dynamicVolume = Mathf.Clamp(power / 500f, 0.2f, 1.0f) * volume;
                    Debug.Log("Dynamic volume is " + dynamicVolume);
                    mAudioSource.PlayOneShot(throwSound, dynamicVolume);
                }

                SkiballLine.instance.EndLine();

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
        yield return new WaitForSeconds(6);
        EventSystem.CompletedBallThrow();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiBallManager1 : MonoBehaviour
{
    [SerializeField] private Transform mSkiballStartT;
    [SerializeField] private Transform mSkiballRackT;
    [SerializeField] private float mRackBallOffset = 1;

    [SerializeField] private IngredientToBallHolder[] mIngredientsToSkiBall;
    private Dictionary<IngredietSelector.IngredientType, GameObject> mIngredientToBallMap = new();

    [SerializeField] private int mMinPeakScore = 3;
    [SerializeField] private int mMinGoodScore = 2;
    [SerializeField] private int mMinMidScore = 1;
    [SerializeField] private int mMinBadScore = 0;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip missSound;
    [SerializeField, Range(0f, 1f)] private float volume = 0.5f;
    private AudioSource audioSource;

    private int mSkiballPoints = 0;

    private List<Ball> mBalls = new();

    private IngredietSelector.IngredientType mIngredietType;

    [Serializable]
    public struct IngredientToBallHolder
    {
        public IngredietSelector.IngredientType ingredientType;
        public GameObject ballObject;
    }

    public IngredietSelector.IngredientType IngredientType {  get { return mIngredietType; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        BuildMap();
    }

    private void OnEnable()
    {
        EventSystem.OnStartSkiball += StartSkiball;
        EventSystem.OnCompletedBallThrow += HandleNextBall;
        EventSystem.OnAddSkiballPoints += AddPoints;
    }

    private void OnDisable()
    {
        EventSystem.OnStartSkiball -= StartSkiball;
        EventSystem.OnCompletedBallThrow -= HandleNextBall;
        EventSystem.OnAddSkiballPoints -= AddPoints;
    }

    private void BuildMap()
    {
        for (int i = 0; i < mIngredientsToSkiBall.Length; i++)
        {
            mIngredientToBallMap.Add(mIngredientsToSkiBall[i].ingredientType, mIngredientsToSkiBall[i].ballObject);
        }
    }

    private void StartSkiball(List<TrayIngredient> ingredients)
    {
        float offset = 0;
        for (int i = 0; i < ingredients.Count; i++)
        {
            GameObject currSkiball;
            Ball ballScript;
            if (i == 0)
            {
                currSkiball = Instantiate(mIngredientToBallMap[ingredients[i].IngredientType], mSkiballStartT);
                ballScript = currSkiball.GetComponent<Ball>();
                ballScript.IngredientType = mIngredietType;
                ballScript.SetReadyToThrow();
            }
            else
            {
                currSkiball = Instantiate(mIngredientToBallMap[ingredients[i].IngredientType], mSkiballRackT);
                currSkiball.transform.position += currSkiball.transform.forward * offset;
                offset += mRackBallOffset;
                ballScript = currSkiball.GetComponent<Ball>();
                ballScript.IngredientType = mIngredietType;
            }

            mBalls.Add(ballScript);
        }
    }

    private void HandleNextBall()
    {
        // Play sound when player misses
        if (mBalls.Count > 0 && !mBalls[0].HasGottenPoints)
        {
            if (missSound != null)
            {
                audioSource.PlayOneShot(missSound, volume);
            }
            Debug.Log("MISS! No points for this ball.");
        }

        //Get rid of last thrown ball
        Destroy(mBalls[0].gameObject);
        mBalls.RemoveAt(0);

        //All balls throw
        if (mBalls.Count == 0)
        {
            Debug.Log("FINAL SKI Score: " + mSkiballPoints);
            Debug.Log("FINAL INgredientChoise: " + IngredientManager.Instance.IngredientChoicePoints);
            //Signify serve tea
            int finalScore = mSkiballPoints * IngredientManager.Instance.IngredientChoicePoints;

            OrderQuality orderQuality = OrderQuality.Bad;

            if (finalScore >= mMinMidScore)
            {
                orderQuality = OrderQuality.Mid;

                if (finalScore >= mMinGoodScore)
                {
                    orderQuality = OrderQuality.Good;

                    if (finalScore >= mMinPeakScore)
                    {
                        orderQuality = OrderQuality.Peak;
                    }
                }
            }

            EventSystem.RateOrder(orderQuality);
            StartCoroutine(Co_DelayNextCustomerTrigger());
            Debug.Log("FINAL: " + finalScore);
            EventSystem.AddTotalBrewPoints(finalScore);
            mSkiballPoints = 0;
            return;
        }

        StartCoroutine(Co_SlideBall());
    }

    private IEnumerator Co_SlideBall()
    {
        Vector3 ogPos = mBalls[0].transform.position;
        Vector3 endPos = mSkiballStartT.position;

        mBalls[0].gameObject.GetComponent<TrailRenderer>().enabled = false;

        float currTime = 0;
        float totalTime = 0.2f;

        while (currTime / totalTime < 1)
        {
            currTime += Time.deltaTime;
            mBalls[0].transform.position = Vector3.Lerp(ogPos, endPos, currTime / totalTime);
            yield return null;
        }

        mBalls[0].gameObject.GetComponent<TrailRenderer>().enabled = true;

        //Get next ball ready to throw
        mBalls[0].SetReadyToThrow();
    }

    private void AddPoints(int pointsToAdd, BallCatcher.TeapotLevel level)
    {
        if (pointsToAdd >= 2)
        {
            EventSystem.RateShot(true);
        }
        else
        {
            EventSystem.RateShot(false);
        }

        mSkiballPoints += pointsToAdd;
    }

    private IEnumerator Co_DelayNextCustomerTrigger()
    {
        yield return new WaitForSeconds(7);
        EventSystem.TriggerNextCustomer();
    }
}

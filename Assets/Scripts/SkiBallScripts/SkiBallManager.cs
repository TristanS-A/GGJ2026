using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiBallManager : MonoBehaviour
{
    [SerializeField] private GameObject mSkiballOBJ;
    [SerializeField] private Transform mSkiballStartT;
    [SerializeField] private Transform mSkiballRackT;
    [SerializeField] private float mRackBallOffset = 1;

    [SerializeField] private int mMinPeakScore = 3;
    [SerializeField] private int mMinGoodScore = 2;
    [SerializeField] private int mMinMidScore = 1;
    [SerializeField] private int mMinBadScore = 0;

    private int mSkiballPoints = 0;

    private List<Ball> mBalls = new();

    private IngredietSelector.IngredientType mIngredietType;

    public IngredietSelector.IngredientType IngredientType {  get { return mIngredietType; } }

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

    private void StartSkiball(List<TrayIngredient> ingredients)
    {
        float offset = 0;
        for (int i = 0; i < ingredients.Count; i++)
        {
            GameObject currSkiball;
            Ball ballScript;
            if (i == 0)
            {
                currSkiball = Instantiate(mSkiballOBJ, mSkiballStartT);
                ballScript = currSkiball.GetComponent<Ball>();
                ballScript.IngredientType = mIngredietType;
                ballScript.SetReadyToThrow();
            }
            else
            {
                currSkiball = Instantiate(mSkiballOBJ, mSkiballRackT);
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

        //Get next ball ready to throw
        mBalls[0].transform.position = mSkiballStartT.position;
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

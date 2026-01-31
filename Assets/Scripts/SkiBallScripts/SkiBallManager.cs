using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiBallManager : MonoBehaviour
{
    [SerializeField] private GameObject mSkiballOBJ;
    [SerializeField] private Transform mSkiballStartT;
    [SerializeField] private Transform mSkiballRackT;
    [SerializeField] private float mRackBallOffset = 1;

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
            //Signify serve tea
            int finalScore = mSkiballPoints * IngredientManager.Instance.IngredientChoicePoints;
            Debug.Log(finalScore);
            return;
        }

        //Get next ball ready to throw
        mBalls[0].transform.position = mSkiballStartT.position;
        mBalls[0].SetReadyToThrow();
    }

    private void AddPoints(int pointsToAdd)
    {
        mSkiballPoints += pointsToAdd;
    }
}

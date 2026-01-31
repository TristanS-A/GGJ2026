using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static IngredietSelector;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private GameObject m_IngredientOBJ;
    [SerializeField] private Transform m_Tray;
    [SerializeField] private TextMeshProUGUI mIngrNameText;
    [SerializeField] private TextMeshProUGUI mIngrDescritptionText;
    [SerializeField] private ScentIngredientHolder[] mScentToIngredients;
    [SerializeField] private IngredientDescriptionHolder[] mIngredientToDescription;
    private List<TrayIngredient> mCurrIngredients = new();

    private static IngredientManager instance;

    private int mIngredientChoicePoints = 0;
    private Scent mTargetScent = Scent.Winter;

    private Dictionary<Scent, List<IngredietSelector.IngredientType>> mScentToIngredientsMap = new();
    private Dictionary<IngredientType, IngredientDescription> mIngredientToDescriptionMap = new();

    public static IngredientManager Instance {  get { return instance; } }
    public List<TrayIngredient> TrayIngredients {  get { return mCurrIngredients; } }

    public int IngredientChoicePoints { get { return mIngredientChoicePoints; } }

    public enum Scent
    {
        Winter, Barnyard, Sweet, Floral, Beach, Kindred, Petrichor,
        Marine, Evergreen, TerraSilva, Citrus, Desert, SavorySpice, Timber, Smoky, Machina
    }

    [Serializable]
    public struct ScentIngredientHolder
    {
        public Scent scent;
        public List<IngredietSelector.IngredientType> ingredients;
    }

    [Serializable]
    public struct IngredientDescriptionHolder
    {
        public IngredientType type;
        public IngredientDescription description;
    }

    [Serializable]
    public struct IngredientDescription
    {
        public string name;
        public string description;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        BuildMaps();
    }

    private void BuildMaps()
    {
        for (int i = 0; i < mScentToIngredients.Length; i++)
        {
            mScentToIngredientsMap.Add(mScentToIngredients[i].scent, mScentToIngredients[i].ingredients);
        }

        for (int i = 0; i < mIngredientToDescription.Length; i++)
        {
            mIngredientToDescriptionMap.Add(mIngredientToDescription[i].type, mIngredientToDescription[i].description);
        }
    }

    public void SetTargetScent(Scent targetScent)
    {
        mTargetScent = targetScent;
    }

    public void AddIngredient(Sprite sprite, IngredietSelector.IngredientType ingredientType)
    {
        //If not already added then adds ingredient to tray
        GameObject newTrayIngredient = Instantiate(m_IngredientOBJ, m_Tray);
        TrayIngredient trayIngr = newTrayIngredient.GetComponentInChildren<TrayIngredient>();
        if (trayIngr != null)
        {
            mCurrIngredients.Add(trayIngr);
            trayIngr.SetImageAndType(sprite, ingredientType);
        }
    }

    public void RemoveIngredient(IngredietSelector.IngredientType ingredientType)
    {
        //Looks for if ingredient was already added and removes it from tray
        for (int i = 0; i < mCurrIngredients.Count; i++)
        {
            if (mCurrIngredients[i].IngredientType == ingredientType)
            {
                Destroy(mCurrIngredients[i].gameObject);
                mCurrIngredients.RemoveAt(i);
                return;
            }
        }
    }

    public bool CanAddIngredientToTray()
    {
        //Does not add more than 3 ingredients to tray
        return mCurrIngredients.Count < 3;
    }

    public void RegisterIngredientChoiceScore()
    {
        int score = 0;
        //Calculates the ingredient choice score
        foreach (TrayIngredient ingredient in mCurrIngredients)
        {
            //Adds to score if player chose a correct ingredient
            if (mScentToIngredientsMap[mTargetScent].Contains(ingredient.IngredientType))
            {
                score++;
            }
        }

        mIngredientChoicePoints = score;
    }

    public void SetIngredientDescription(IngredientType ingredientType)
    {
        mIngrNameText.text = mIngredientToDescriptionMap[ingredientType].name;
        mIngrDescritptionText.text = mIngredientToDescriptionMap[ingredientType].description;
    }
}

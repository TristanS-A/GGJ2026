using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TrayIngredient : MonoBehaviour
{
    [SerializeField] private Image mImage;
    private IngredietSelector.IngredientType mType;

    public IngredietSelector.IngredientType IngredientType
    {
        get { return mType; }
    }

    public void SetImageAndType(Sprite sprite, IngredietSelector.IngredientType ingredientType)
    {
        mImage.sprite = sprite;
        mType = ingredientType;
    }
}

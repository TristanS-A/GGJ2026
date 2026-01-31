using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IngredietSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private IngredientType mIngredientType;

    private bool mHoveredOn = false;
    private bool mSelected = false;
    private Image mImage;

    public enum IngredientType
    {
        AGED_GREASE,
        CORN_SPICE,
        GROUND_HAY,
        ABANDONED_DEW,
        UMBRELLA_LEAF,
        SMILING_FLOWER
    }

    private void Start()
    {
        mImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mHoveredOn = true;

        IngredientManager.Instance.SetIngredientDescription(mIngredientType);

        if (!mSelected)
        {
            mImage.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mHoveredOn = false;
        if (!mSelected)
        {
            mImage.color = Color.white;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mHoveredOn)
            {
                //Changes color if ingredient selected
                if (mSelected)
                {
                    mSelected = false;
                    mImage.color = new Color(1f, 1, 1f, 0.5f);
                    IngredientManager.Instance.RemoveIngredient(mIngredientType);
                }
                else if (IngredientManager.Instance.CanAddIngredientToTray())
                {
                    mSelected = true;
                    mImage.color = new Color(0.5f, 1, 0.5f, 0.5f);
                    IngredientManager.Instance.AddIngredient(mImage.sprite, mIngredientType);
                }
            }
        }
    }
}

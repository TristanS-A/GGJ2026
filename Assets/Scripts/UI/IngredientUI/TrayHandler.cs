using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrayHandler : MonoBehaviour
{
    [SerializeField] private Button mBrewButton;
    [SerializeField] private Button mSmellTestButton;

    // Start is called before the first frame update
    void Start()
    {
        mBrewButton.onClick.AddListener(Brew);
        mSmellTestButton.onClick.AddListener(SmellTest);
    }

    private void Brew()
    {
        EventSystem.SlideOutUI(UISlideOut.UIType.INGREDIENTS, false);
        EventSystem.SlideOutUI(UISlideOut.UIType.TRAY, false);

        IngredientManager.Instance.RegisterIngredientChoiceScore();

        EventSystem.StartSkiball(IngredientManager.Instance.TrayIngredients);
    }

    private void SmellTest()
    {

    }
}

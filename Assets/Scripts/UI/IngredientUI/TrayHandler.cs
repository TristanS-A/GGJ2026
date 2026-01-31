using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrayHandler : MonoBehaviour
{
    [SerializeField] private Button mBrewButton;
    [SerializeField] private Button mSmellTestButton;
    [SerializeField] private TextMeshProUGUI mScentDescription;

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
        IngredientManager.Scent closestScent = IngredientManager.Instance.GetClosestScentAndStrengthOnTray();

        mScentDescription.text = IngredientManager.Instance.GetScentDescription(closestScent);

        EventSystem.SlideOutUI(UISlideOut.UIType.SCENT_DESCRIPTION, true);
        StartCoroutine(Co_SlideScentDescriptionBack());
    }

    private IEnumerator Co_SlideScentDescriptionBack()
    {
        yield return new WaitForSeconds(5);
        EventSystem.SlideOutUI(UISlideOut.UIType.SCENT_DESCRIPTION, false);
    }
}

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
    [SerializeField] private OVRConfig mOmara;

    Coroutine mCurrCoroutine = null;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField, Range(0f, 1f)] private float clickVolume = 0.5f;
    private AudioSource mAudioSource;

    private void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
        if (mAudioSource == null)
        {
            mAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        EventSystem.OnTriggerCanBrew += SetBrewButtonEnabled;
    }

    private void OnDisable()
    {
        EventSystem.OnTriggerCanBrew -= SetBrewButtonEnabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        mBrewButton.onClick.AddListener(Brew);
        mSmellTestButton.onClick.AddListener(SmellTest);
        mBrewButton.gameObject.SetActive(false);
    }

    private void Brew()
    {
        if (clickSound != null) mAudioSource.PlayOneShot(clickSound, clickVolume);

        EventSystem.SlideOutUI(UISlideOut.UIType.INGREDIENTS, false);
        EventSystem.SlideOutUI(UISlideOut.UIType.TRAY, false);

        IngredientManager.Instance.RegisterIngredientChoiceScore();

        EventSystem.StartSkiball(IngredientManager.Instance.TrayIngredients);
    }

    private void SmellTest()
    {
        if (clickSound != null) mAudioSource.PlayOneShot(clickSound, clickVolume);

        IngredientManager.Scent closestScent = IngredientManager.Instance.GetClosestScentAndStrengthOnTray();

        mScentDescription.text = IngredientManager.Instance.GetScentDescription(closestScent);

        EventSystem.SlideOutUI(UISlideOut.UIType.SCENT_DESCRIPTION, true);

        if (mCurrCoroutine != null)
        {
            StopCoroutine(mCurrCoroutine);
        }

        mOmara.StartScent(closestScent);
        //mOmara.transform.position = Camera.main.transform.position;
        mCurrCoroutine = StartCoroutine(Co_SlideScentDescriptionBack());
    }

    private IEnumerator Co_SlideScentDescriptionBack()
    {
        yield return new WaitForSeconds(5);
        EventSystem.SlideOutUI(UISlideOut.UIType.SCENT_DESCRIPTION, false);
        //mOmara.transform.position = mOmara.transform.position + Vector3.up * -30;
        mOmara.StopScent();
    }

    private void SetBrewButtonEnabled(bool enabled)
    {
        mBrewButton.gameObject.SetActive(enabled);
    }
}

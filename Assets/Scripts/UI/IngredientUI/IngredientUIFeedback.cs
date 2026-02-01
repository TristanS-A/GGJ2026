using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientUIFeedback : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip errorSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float hoverVolume = 0.25f;
    [Range(0f, 1f)] public float clickVolume = 0.4f;
    [Range(0f, 1f)] public float errorVolume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound, hoverVolume);
        }
    }


    private static bool trayWasFull = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        int currentCount = IngredientManager.Instance.TrayIngredients.Count;
        bool isCurrentlyFull = currentCount >= 3;

        if (!isCurrentlyFull)
        {
            trayWasFull = false;
            if (clickSound != null) audioSource.PlayOneShot(clickSound, clickVolume);
        }
        else
        {
            if (!trayWasFull)
            {
                trayWasFull = true;
                if (clickSound != null) audioSource.PlayOneShot(clickSound, clickVolume);
            }
            else
            {
                if (errorSound != null) audioSource.PlayOneShot(errorSound, errorVolume);
            }
        }
    }

}

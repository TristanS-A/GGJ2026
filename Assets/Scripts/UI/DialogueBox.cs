using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typeSound;
    [SerializeField][Range(1, 5)] private int frequency = 2;
    private AudioClip currentTypeSound; // For different characters

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (textComponent.text == lines[index])
            //{
            //    NextLine();
            //}
            //else
            //{
            //    StopAllCoroutines();
            //    textComponent.text = lines[index];
            //}
        }
    }

    IEnumerator TypeLine(string line)
    {
        char[] chars = line.ToCharArray();

        string textToDisplay = "";
        bool updateLocked = false;
        int soundCounter = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            textToDisplay += chars[i];

            if (chars[i] == '<')
                updateLocked = true;
            else if (chars[i] == '>')
                updateLocked = false;

            if (!updateLocked)
            {
                textComponent.text = textToDisplay;

                if (currentTypeSound != null && !char.IsWhiteSpace(chars[i]))
                {
                    soundCounter++;
                    if (soundCounter % frequency == 0)
                    {
                        audioSource.pitch = Random.Range(0.85f, 1.15f);
                        audioSource.PlayOneShot(currentTypeSound);
                    }
                }

                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    public void WriteLine(string line, AudioClip voiceSound = null)
    {
        currentTypeSound = (voiceSound != null) ? voiceSound : typeSound;
        StartCoroutine(TypeLine(line));
    }


}


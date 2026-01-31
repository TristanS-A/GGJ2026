using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
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
            }

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void WriteLine(string line)
    {
        StartCoroutine(TypeLine(line));
    }


}


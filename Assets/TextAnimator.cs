using TMPro;
using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
    public TMP_Text titleText;
    public float letterDelay = 0.1f;

    void Start()
    {
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        string fullText = titleText.text;
        titleText.text = "";
        foreach (char c in fullText)
        {
            titleText.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }
}


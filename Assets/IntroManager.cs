using TMPro;
using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [Header("Intro Settings")]
    public TMP_Text introText;         // Assign your TextMeshPro object
    public float letterDelay = 0.1f;   // Delay between letters
    public float holdTime = 2f;        // Time to hold full text before fade

    [Header("FPS Settings")]
    public GameObject fpsController;   // Assign your FPS capsule (Starter Asset)

    [Header("Fade Settings")]
    public float fadeTime = 1f;        // Duration of fade-out
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Disable FPS player at start
        fpsController.SetActive(false);

        // Ensure CanvasGroup exists for fading
        canvasGroup = introText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = introText.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1f;

        // Start the intro coroutine
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Store full text and clear it
        string fullText = introText.text;
        introText.text = "";

        // Letter-by-letter reveal
        foreach (char c in fullText)
        {
            introText.text += c;
            yield return new WaitForSeconds(letterDelay);
        }

        // Hold full text
        yield return new WaitForSeconds(holdTime);

        // Fade out text
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
            yield return null;
        }

        // Hide text
        introText.gameObject.SetActive(false);

        // Enable FPS player and its camera
        fpsController.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    public Image fadeImage;
    public GameObject audioMenuPanel; 
    public float fadeDuration = 1f; 

    private bool isAudioMenuVisible = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (audioMenuPanel != null)
        {
            audioMenuPanel.SetActive(false);
        }
    }

    public void ToggleAudioMenu()
    {
        if (audioMenuPanel != null)
        {
            isAudioMenuVisible = !isAudioMenuVisible;
            audioMenuPanel.SetActive(isAudioMenuVisible);
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutIn(sceneName));
    }

    private IEnumerator FadeOutIn(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        yield return null;
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        Color currentColor = fadeImage.color;
        float startAlpha = currentColor.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            currentColor.a = alpha;
            fadeImage.color = currentColor;
            yield return null;
        }

        currentColor.a = targetAlpha;
        fadeImage.color = currentColor;
    }
}

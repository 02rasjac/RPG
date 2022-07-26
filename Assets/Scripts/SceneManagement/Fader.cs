using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 1f;

        CanvasGroup canvasGroup;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return StartCoroutine(FadeOut());
            yield return StartCoroutine(FadeIn());
        }

        IEnumerator FadeOut()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime / fadeOutTime;
                yield return null;
            }
        }

        IEnumerator FadeIn()
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeInTime;
                yield return null;
            }
        }
    }
}
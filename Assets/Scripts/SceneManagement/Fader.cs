using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float waitTime = 1f;
        public float WaitTime { get { return waitTime; } }

        CanvasGroup canvasGroup;
        Coroutine activeFade;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutInstant()
        {
            canvasGroup.alpha = 1f;
        }

        public IEnumerator FadeOut()
        {
            if (activeFade != null) StopCoroutine(activeFade);
            activeFade = StartCoroutine(FadeOutRoutine());
            yield return activeFade;
        }

        public IEnumerator FadeIn()
        {
            if (activeFade != null) StopCoroutine(activeFade);
            activeFade = StartCoroutine(FadeInRoutine());
            yield return activeFade;
        }

        IEnumerator FadeOutRoutine()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += Time.deltaTime / fadeOutTime;
                yield return null;
            }
        }

        IEnumerator FadeInRoutine()
        {
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeInTime;
                yield return null;
            }
        }
    }
}
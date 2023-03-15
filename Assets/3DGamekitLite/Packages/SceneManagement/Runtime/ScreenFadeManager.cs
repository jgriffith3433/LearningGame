using Pixelplacement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit3D
{
    public class ScreenFadeManager : Singleton<ScreenFadeManager>
    {
        public enum FadeType
        {
            Black, Loading, GameOver,
        }

        public bool IsFading
        {
            get { return m_IsFading; }
        }

        public CanvasGroup faderCanvasGroup;
        public CanvasGroup loadingCanvasGroup;
        public CanvasGroup gameOverCanvasGroup;
        public float fadeDuration = 1f;

        protected bool m_IsFading;

        const int k_MaxSortingLayer = 32767;

        protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup)
        {
            m_IsFading = true;
            canvasGroup.blocksRaycasts = true;
            float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
            while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                    fadeSpeed * Time.deltaTime);
                yield return null;
            }
            canvasGroup.alpha = finalAlpha;
            m_IsFading = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void SetAlpha(float alpha)
        {
            faderCanvasGroup.alpha = alpha;
        }

        public IEnumerator FadeSceneIn()
        {
            CanvasGroup canvasGroup;
            if (faderCanvasGroup.alpha > 0.1f)
                canvasGroup = faderCanvasGroup;
            else if (gameOverCanvasGroup.alpha > 0.1f)
                canvasGroup = gameOverCanvasGroup;
            else
                canvasGroup = loadingCanvasGroup;

            yield return StartCoroutine(Fade(0f, canvasGroup));

            canvasGroup.gameObject.SetActive(false);
        }

        public IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black)
        {
            CanvasGroup canvasGroup;
            switch (fadeType)
            {
                case FadeType.Black:
                    canvasGroup = faderCanvasGroup;
                    break;
                case FadeType.GameOver:
                    canvasGroup = gameOverCanvasGroup;
                    break;
                default:
                    canvasGroup = loadingCanvasGroup;
                    break;
            }

            canvasGroup.gameObject.SetActive(true);

            yield return StartCoroutine(Fade(1f, canvasGroup));
        }
    }
}
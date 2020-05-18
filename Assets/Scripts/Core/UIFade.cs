using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class UIFade : MonoBehaviour
    {

        public static UIFade instance;

        [SerializeField] private Image fadeScreen;
        [SerializeField] private float fadeSpeed;

        [SerializeField] private bool shouldFadeToBlack;
        [SerializeField] private bool shouldFadeFromBlack;

        private void Start()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (shouldFadeToBlack)
            {
                var color = fadeScreen.color;
                fadeScreen.color = new Color(color.r, color.g, color.b,
                    Mathf.MoveTowards(color.a, 1f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 1f)
                {
                    shouldFadeToBlack = false;
                }
            }

            if (!shouldFadeFromBlack) return;
            {
                var color = fadeScreen.color;
                fadeScreen.color = new Color(color.r, color.g, color.b,
                    Mathf.MoveTowards(color.a, 0f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 0f)
                {
                    shouldFadeFromBlack = false;
                }
            }
        }

        public void FadeToBlack()
        {
            shouldFadeToBlack = true;
            shouldFadeFromBlack = false;
        }

        public void FadeFromBlack()
        {
            shouldFadeToBlack = false;
            shouldFadeFromBlack = true;
        }
    }
}

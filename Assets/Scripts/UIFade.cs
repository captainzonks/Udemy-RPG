using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    public static UIFade instance;

    public Image fadeScreen;
    public float fadeSpeed;

    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        if (shouldFadeToBlack)
        {
            var color = fadeScreen.color;
            fadeScreen.color = new Color(color.r, color.g, color.b, Mathf.MoveTowards(color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (!shouldFadeFromBlack) return;
        {
            var color = fadeScreen.color;
            fadeScreen.color = new Color(color.r, color.g, color.b, Mathf.MoveTowards(color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
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

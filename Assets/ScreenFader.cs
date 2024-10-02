using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    [SerializeField] Image fadeImage;
    [SerializeField] Color fadeColor = Color.black;
    [SerializeField] float fadeTime = 1;
    [SerializeField] bool fadeFromColorOnStart = false;
    bool fading = false;
    bool doneFadingToColor = false;

    void Start(){
        if(fadeFromColorOnStart){
            FadeFromColor();
        }
    }

    public void FadeToColor(){ //clear to opaque
        if(fading){
            return;
        }
        fading = true;
        StartCoroutine(FadeToColorRoutine());
        IEnumerator FadeToColorRoutine(){
            float t = 0;
            while(t<fadeTime){
                yield return null;
                t+=Time.deltaTime;
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, t/fadeTime);
            }
            fadeImage.color = fadeColor;
            fading = false;
            doneFadingToColor = true;
            yield return null;
        }
    }

    public bool DoneFadingToColor(){
        return doneFadingToColor;
    }

    public void FadeFromColor(){ //opaque to clear
        if (fading)
        {
            return;
        }
        fading = true;
        fadeImage.color = fadeColor;
        StartCoroutine(FadeFromColorRoutine());
        IEnumerator FadeFromColorRoutine()
        {
            float t = 0;
            while (t < fadeTime)
            {
                yield return null;
                t += Time.deltaTime;
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f-(t / fadeTime));
            }
            fadeImage.color = Color.clear;
            fading = false;
            yield return null;
        }
    }

}

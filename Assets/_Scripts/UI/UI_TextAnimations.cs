using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class UI_TextAnimations : MonoBehaviour
{
    private TMP_Text text;
    public float colorChangeDuration;
    void OnEnable()
    {
        text = GetComponent<TMP_Text>();
    }

    public void ChangeColor(Color start, Color goal, bool resetToOriginalAfter = false,  Action endCallback = null)
    {
        StartCoroutine(LerpColor(start, goal, resetToOriginalAfter, endCallback));
    }

    public void ChangeColor( Color goal,bool resetToOriginalAfter = false, Action endCallback = null)
    {
        StartCoroutine(LerpColor(text.color, goal, resetToOriginalAfter, endCallback));
    }
    IEnumerator LerpColor(Color start, Color goal, bool resetToOriginalAfter, Action endCallback = null)
    {
        float t = 0;

        while (t <= colorChangeDuration)
        {
            text.color = Color.Lerp(start, goal, t / colorChangeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        text.color = goal;
        
        if (resetToOriginalAfter)
            text.color = start;

        endCallback?.Invoke();
    }
}

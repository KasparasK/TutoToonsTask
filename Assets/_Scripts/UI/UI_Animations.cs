using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Animations : MonoBehaviour
{
    private RectTransform rect;
    public float scaleDuration;
    void OnEnable()
    {
        rect = GetComponent<RectTransform>();
    }

    #region MegaPop
    public void MegaPop(Action callback = null)
    {
        StartCoroutine(MegaPopControler(callback));
    }
    IEnumerator MegaPopControler(Action callback = null)
    {
        Vector3 start = Vector3.one;
        Vector3 goal = new Vector3(0.8f, 0.8f, 1);
        yield return StartCoroutine(ChangeScale(start, goal, 0.15f));
        start = goal;
        goal = new Vector3(1.2f, 1.2f, 1);
        yield return StartCoroutine(ChangeScale(start, goal, 0.15f));

        start = goal;
        goal = new Vector3(0.90f, 0.90f, 1);
        yield return StartCoroutine(ChangeScale(start, goal, 0.1f));

        start = goal;
        goal = Vector3.one;
        yield return StartCoroutine(ChangeScale(start, goal, 0.1f));

        start = goal;
        goal = new Vector3(0.95f, 0.95f, 1);
        yield return StartCoroutine(ChangeScale(start, goal, 0.1f));

        start = goal;
        goal = Vector3.one;
        yield return StartCoroutine(ChangeScale(start, goal, 0.1f));

        callback?.Invoke();
    }

    #endregion
    
    public void Scale(Vector3 start, Vector3 goal, Action callback = null)
    {
        rect.localScale = start;

        StartCoroutine(ChangeScale(start, goal, scaleDuration, callback));
    }
    public void ScaleWithPop(Vector3 start, Vector3 goal, Action callback = null)
    {
        rect.localScale = start;
        StartCoroutine(PopScale(start, goal, callback));
    }
    public void ScaleWithPop(Action callback = null) //0 to 1
    {
        rect.localScale = Vector3.zero;
        StartCoroutine(PopScale(Vector3.zero, Vector3.one, callback));
    }

    public void Pop(Action callback = null)
    {
        StartCoroutine(
            ChangeScale(Vector3.one, new Vector3(1.2f, 1.2f, 1), scaleDuration/2,
            () => StartCoroutine( ChangeScale(new Vector3(1.2f, 1.2f, 1), Vector3.one, scaleDuration/2, callback))));
    }

    IEnumerator PopScale(Vector3 start, Vector3 goal, Action callback = null)
    {
        float sizeUpDuration = scaleDuration * 0.8f;
        Vector3 scalePop = new Vector3(0.2f, 0.2f, 0);
        goal += scalePop;
        yield return StartCoroutine(ChangeScale(start, goal, sizeUpDuration));
        goal -= scalePop;

        StartCoroutine(ChangeScale(rect.localScale, goal,scaleDuration-sizeUpDuration, callback));

    }

    IEnumerator ChangeScale(Vector3 start, Vector3 goal, float duration, Action callback = null)
    {
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float curvePercent = Mathf.SmoothStep(0, 1, t / duration);
            rect.localScale = Vector3.Lerp(start, goal, curvePercent);
            yield return null;
        }
        rect.localScale = goal;

        callback?.Invoke();
    }
}

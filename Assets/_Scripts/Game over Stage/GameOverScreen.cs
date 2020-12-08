using System;
using System.Collections;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public UI_Animations uiAnimations;
    public StageManager stageManager;

    public void Show()
    {
        gameObject.SetActive(true);
        uiAnimations.ScaleWithPop(WaitUntilLevelSelect);
    }

    public void Hide()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);

    }

    void WaitUntilLevelSelect()
    {
        StartCoroutine(WaitBeforeAction(2f, () => stageManager.SetStage(0)));
    }
    IEnumerator WaitBeforeAction(float waitTime, Action afterWait = null)
    {
        float t = 0;

        while (t <= waitTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        afterWait?.Invoke();
    }
}

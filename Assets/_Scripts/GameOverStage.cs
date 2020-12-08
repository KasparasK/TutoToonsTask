using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStage : MonoBehaviour , IStage
{
    public UI_Animations uiAnimations;
    public StageManager stageManager;
    public GameObject gameoverScreen;
    public void Initialize()
    {
        gameoverScreen.SetActive(true);
        uiAnimations.ScaleWithPop(WaitUntilLevelSelect);
    }
    public void Finish()
    {
        gameoverScreen.SetActive(false);
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

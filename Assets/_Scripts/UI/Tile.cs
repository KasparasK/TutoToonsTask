using System;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TMP_Text levelNumberTxt;
    public Action<int> onClick;
    private int levelNumber;
    public void Initialize(Action<int> onClick, int levelNumber)
    {
        this.levelNumber = levelNumber;
        this.onClick = onClick;

        levelNumberTxt.text = (levelNumber+1).ToString();
    }

    public void OnClick()
    {
        onClick?.Invoke(levelNumber);
    }
}

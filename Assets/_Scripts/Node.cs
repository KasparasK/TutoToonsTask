using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public TMP_Text numTxt;
    public int num;
    private Action<Node> onClick;
    public void Initialize(int num, Action<Node> onClick)
    {
        this.onClick = onClick;
        this.num = num;
        numTxt.text = num.ToString();
    }

    void OnMouseDown()
    {
        onClick?.Invoke(this);
    }
}

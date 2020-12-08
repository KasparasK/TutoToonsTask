using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Node : MonoBehaviour
{
    public TMP_Text numTxt;
    public SpriteRenderer spriteRenderer;
    public UI_TextAnimations uiTextAnimations;

    public int num;
    private Action<Node> onClick;

    public Sprite notConnected;
    public Sprite connected;

    private bool isConnected;
    public void Initialize(int num, Action<Node> onClick)
    {

        this.onClick = onClick;
        this.num = num;
        numTxt.text = num.ToString();
        spriteRenderer.sprite = notConnected;
        isConnected = false;
    }

    void OnMouseDown()
    {
        if(!isConnected)
            onClick?.Invoke(this);
    }

    public void OnConnected()
    {
        spriteRenderer.sprite = connected;
        uiTextAnimations.ChangeColor(new Color(0, 0, 0, 0));
        isConnected = true;
    }

    public void Select()
    {
        if(!isConnected)
            spriteRenderer.color = Color.red;
    }
    public void Deselect()
    {
        spriteRenderer.color = Color.white;

    }

}

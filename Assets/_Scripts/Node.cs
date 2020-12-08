using System;
using TMPro;
using UnityEngine;

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
}

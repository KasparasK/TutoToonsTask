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

    public void Initialize(int num, Action<Node> onClick)
    {
        this.onClick = onClick;
        this.num = num;
        numTxt.text = num.ToString();

        spriteRenderer.sprite = notConnected;

    }

    void OnMouseDown()
    { 
        onClick?.Invoke(this);
    }

    public void OnConnected()
    {
        spriteRenderer.sprite = connected;
        uiTextAnimations.ChangeColor(new Color(0, 0, 0, 0));

    }
}

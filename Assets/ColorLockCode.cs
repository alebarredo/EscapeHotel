using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLockCode : MonoBehaviour
{
    public string roomLock;

    public Color[] colors = { new Color(80, 0, 255, 1f),
                              new Color(255, 255, 0, 1f),
                              new Color(0, 255, 255, 1f),
                              new Color(255, 0, 255, 1f),
                              new Color(0, 255, 30, 1f),
                              new Color(255, 0, 60, 1f) };

    public Image code1;
    public Image code2;
    public Image code3;

    // Start is called before the first frame update
    void Start()
    {
        SetRoomCode();
    }

    void SetRoomCode()
    {
        ColorLock colorLock = GameObject.FindGameObjectWithTag(roomLock).GetComponent<ColorLock>();

        code1.color = colors[colorLock.goodDigit_1];
        code2.color = colors[colorLock.goodDigit_2];
        code3.color = colors[colorLock.goodDigit_3];
    }
}

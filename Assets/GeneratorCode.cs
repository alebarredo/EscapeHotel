using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorCode : MonoBehaviour
{

    public Color codeColor;

    [Space]
    public Image[] circles;

    // Update is called once per frame
    [ContextMenu("Set Code")]
    public void SetCode(int code1, int code2, int code3)
    {
        circles[code1].color = codeColor;
        circles[code2].color = codeColor;
        circles[code3].color = codeColor;
    }
}

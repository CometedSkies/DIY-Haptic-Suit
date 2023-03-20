using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VestVisual : MonoBehaviour
{
    [Header("Variables")]
    public Color On;
    public Color Off;

    public Haptics haptics;

    [Header("Front 6 Array")]
    public Image F1;
    public Image F2;
    public Image F3;
    public Image F4;
    public Image F5;
    public Image F6;

    [Header("Back 6 Array")]
    public Image B1;
    public Image B2;
    public Image B3;
    public Image B4;
    public Image B5;
    public Image B6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Assign(haptics.F1, F1);
        Assign(haptics.F2, F2);
        Assign(haptics.F3, F3);
        Assign(haptics.F4, F4);
        Assign(haptics.F5, F5);
        Assign(haptics.F6, F6);

        Assign(haptics.B1, B1);
        Assign(haptics.B2, B2);
        Assign(haptics.B3, B3);
        Assign(haptics.B4, B4);
        Assign(haptics.B5, B5);
        Assign(haptics.B6, B6);
    }

    public void Assign(bool V, Image I)
    {
        if (V)
        {
            I.color = On;
        }
        else
        {
            I.color = Off;
        }
    }
}

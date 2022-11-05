using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Text txtHp;
    public static CanvasController instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddTextHp(int vld)
    {
        txtHp.text = "HP: " + vld.ToString();
    }
}

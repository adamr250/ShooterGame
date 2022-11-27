using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bomb : MonoBehaviour
{
    public static int bombCount = 2;
    public Text bombText;

    private void Start()
    {
        bombText.text = "0";
        bombCount = 0;
    }
    public void bombTextDisplay(int x)
    {
        bombCount += x;
        bombText.text = bombCount.ToString();

    }
}

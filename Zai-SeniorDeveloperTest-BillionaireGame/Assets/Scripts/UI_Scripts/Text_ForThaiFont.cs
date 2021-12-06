using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Text_ForThaiFont : MonoBehaviour
{
    private Text Thai_text;

    public void Start()
    {
        AdjustText();
    }

    public void AdjustText() 
    {
        Thai_text = this.GetComponent<Text>();
        Thai_text.text = ThaiFontAdjuster.Adjust(Thai_text.text);
    }

}

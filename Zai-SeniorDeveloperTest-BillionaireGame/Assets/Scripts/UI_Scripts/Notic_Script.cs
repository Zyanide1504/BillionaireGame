using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notic_Script : MonoBehaviour
{
    public Animator anim;
    public Text notic_Text;

    public void ShowNotic(string text) 
    {
        this.gameObject.SetActive(true);
        notic_Text.text = text;
        anim.SetTrigger("ShowNotic");
    
    }
}

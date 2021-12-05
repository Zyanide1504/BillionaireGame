using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Card Info")]
    public string card_Name;
    public string card_Description;

    [Header("Card Front & Back")]
    public GameObject card_Front;
    public GameObject card_Back;

    bool cardBackIsActive;

    

    Vector3 card_rotation;


    IEnumerator CalculateFlip() 
    {
    
    }



}

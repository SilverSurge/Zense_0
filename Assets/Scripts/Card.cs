using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")] 
public class Card : ScriptableObject
{
    public string card_name;
    public string description;

    public Sprite artwork;
    public int cost;
    public int attack;
    public Color32 associated_color;
    
}

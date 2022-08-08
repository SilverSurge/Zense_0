using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Card_Manager : MonoBehaviour
{

    [SerializeField] List<Card> cards;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text card_name;
    [SerializeField] Image card_button_image;
    [SerializeField] string card_identity;
    [SerializeField] PlayerScript script;
    [SerializeField] Slider ManaBar, HealthBar, EnemyBar;
    [SerializeField] TMP_Text not_enough;
    int card_number;
    bool isSelected = false;
    

    void Start()
    {
        HealthBar.maxValue = script.player_health;
        ManaBar.maxValue = script.player_mana;
        EnemyBar.maxValue = script.enemy_health;
        assign_card();
        update_sliders();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isSelected == true)
        {
            Debug.Log("pressed space");
            use_card();
            assign_card();            
        }
    }
    void assign_card()
    {
        int n = cards.Count;
        card_number = Random.Range(0, n - 1);
        isSelected = false;
        Debug.Log(card_number+" "+isSelected);
        card_button_image.sprite = cards[card_number].artwork;
        cost.text = (cards[card_number].cost).ToString();
        
    }
    void update_sliders()
    {
        ManaBar.value = script.player_mana;
        HealthBar.value = script.player_health;
        EnemyBar.value = script.enemy_health;
    }
    void use_card()
    {
        Debug.Log("Card Used");
        if (cards[card_number].card_name == "Grim")
        {
            if (script.is_using_mana)
            {
                if (script.player_mana >= cards[card_number].cost)
                {
                    script.enemy_health /= 2;
                    not_enough_resource(false);
                    script.player_mana -= cards[card_number].cost;
                }
                else
                    not_enough_resource(true);
            }
            else
            {
                if (script.player_health >= (cards[card_number].cost * 2))
                {
                    script.enemy_health /= 2;
                    not_enough_resource(false);
                    script.player_health -= (cards[card_number].cost * 2);
                }
                else
                    not_enough_resource(true);
            }
                
        }
        else if (cards[card_number].card_name == "ManaJar")
        {
            script.player_mana += 80;
        }
        else if (cards[card_number].name == "Potion")
        {
            if (script.is_using_mana)
            {
                if (script.player_mana >= cards[card_number].cost)
                {
                    not_enough_resource(false);
                    script.player_mana += 80;
                }
                else
                    not_enough_resource(true);
            }
        }
        else
        {
            if (script.is_using_mana)
            {
                if (script.player_mana >= cards[card_number].cost)
                {
                    script.enemy_health -= cards[card_number].attack ;
                    not_enough_resource(false);
                    script.player_mana -= cards[card_number].cost;
                }
                else
                    not_enough_resource(true);
            }
            else
            {
                if (script.player_health >= (cards[card_number].cost * 2))
                {
                    script.enemy_health -= cards[card_number].attack;
                    not_enough_resource(false);
                    script.player_health -= (cards[card_number].cost * 2);
                }
                else
                    not_enough_resource(true);
            }
        }
        
        update_sliders();
    }
    public void select_card()
    {
        isSelected = true;
        description.text = cards[card_number].description;
        card_name.text = cards[card_number].card_name;
    }
    public void deselect_card()
    {
        isSelected = false;
        Debug.Log("Card Deselected:"+isSelected+card_identity);
    }

    private void not_enough_resource(bool para)
    {
        if (para)
            not_enough.text = "Not Enough Resources";
        else
            not_enough.text = "";
    }
    
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Manager_1 : MonoBehaviour
{
    //----------------SerializeFields------------------//
    [SerializeField] List<Card> cards;
    [SerializeField] PlayerScript player;
    [SerializeField] TMP_Text card_name;
    [SerializeField] TMP_Text not_en_reso;
    [SerializeField] Image card_button_image;
    [SerializeField] Slider player_health_slider;
    [SerializeField] Slider player_mana_slider;
    [SerializeField] Slider enemy_health_slider;
    [SerializeField] Color32 alpha_zero;
    [SerializeField] Color32 desected_color;
    [SerializeField] Material glow_material;
    //----------------Public Variables-----------------//
    
    
    public bool is_selected;
    //----------------Private Variables----------------//
    int card_index = -1;
    int initial_player_health;
    int initial_player_mana;
    [SerializeField]Animator my_animator;
    

    void Start()
    {
        get_card();

        //my_animator = GetComponent<Animator>();
        if (my_animator != null)
            Debug.Log("Animator found");
        else
            Debug.Log("Animator not found");
        player_health_slider.maxValue = player.player_health;
        player_mana_slider.maxValue = player.player_mana;
        enemy_health_slider.maxValue = player.enemy_health;
        initial_player_health = player.player_health;
        initial_player_mana = player.player_mana;
        update_sliders();

    }

    
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space) && is_selected)
        {
            use_card();
        }
        check_if_dead();
    }

    public void get_card()
    {
        Debug.Log("Getting a card");
        int n = cards.Count;
        int idx = Random.Range(0, n - 1);
        while (idx == card_index)
            idx = Random.Range(0, n - 1);
        card_index = idx;
        is_selected = false;
        Debug.Log("New Card Assigned");
        // changing the sprite of the image
        card_button_image.sprite = cards[card_index].artwork;
        // changing the name of the card
        card_name.text = cards[card_index].card_name;
        // deselect is initial setting
        deselect_card();
    }

    void use_card()
    {
        bool is_card_used = false;
        Debug.Log("In use_card method");
        if (is_selected == false) { return; }
        if (cards[card_index].card_name == "HealthPotion")
        {
            Debug.Log("USING HEALTH POTION  #######");
            int tmp = player.player_health + 75;
            player.player_health = tmp > initial_player_health?initial_player_health:tmp;
            Debug.Log(cards[card_index].card_name);
            is_card_used = true;
   
        }
        else if (cards[card_index].name == "ManaPotion")
        {
            Debug.Log("USING MANA POTION  #######");
            if (player.is_using_mana)
            {
                Debug.Log("Need to use health");
            }
            else
            {
                player.player_health -= 20;
                int tmp = player.player_mana + 30;
                player.player_mana = tmp > initial_player_mana ? initial_player_mana : tmp;
                Debug.Log(cards[card_index].card_name);
                is_card_used = true;
            }
        }
        else if (cards[card_index].card_name == "Death")
        {
            Debug.Log("USING DEATH CARD  #######");
            if (player.is_using_mana)
            {
                if(player.player_mana >= cards[card_index].cost)
                {
                    int temp = 60 > (player.enemy_health / 2) ? 60 : (player.enemy_health) / 2;
                    player.enemy_health -= temp;
                    player.player_mana -= cards[card_index].cost;
                    Debug.Log(cards[card_index].card_name);
                    is_card_used = true;
                }
                else
                {
                    Debug.Log("Not Enough Mana: death");
                    not_en_reso.color = Color.white;
                    Invoke("not_enough_resources", 2f);
                }
            }
            else
            {
                if (player.player_health >= cards[card_index].cost*2)
                {
                    int temp = 60 > (player.enemy_health / 2) ? 60 : (player.enemy_health) / 2;
                    player.enemy_health -= temp;
                    player.player_health -= cards[card_index].cost*2;
                    Debug.Log(cards[card_index].card_name);
                    is_card_used = true;
                }
                else
                {
                    Debug.Log("Not Enough Health: death");
                    not_en_reso.color = Color.white;
                    Invoke("not_enough_resources", 2f);
                }
            }
            
        }
        else
        {
            if (player.is_using_mana)
            {
                if (player.player_mana >= cards[card_index].cost)
                {
                    player.enemy_health -= cards[card_index].attack;
                    player.player_mana -= cards[card_index].cost;
                    Debug.Log(cards[card_index].card_name);
                    is_card_used = true;
                }
                else
                {
                    Debug.Log("Not Enough Mana: Not Death");
                    not_en_reso.color = Color.white;
                    Invoke("not_enough_resources", 2f);
                }
            }
            else
            {
                if (player.player_health >= cards[card_index].cost * 2)
                {
                    player.enemy_health -= cards[card_index].attack;
                    player.player_health -= cards[card_index].cost * 2;
                    Debug.Log(cards[card_index].card_name);
                    is_card_used = true;
                }
                else
                {
                    Invoke("not_enough_resources", 2f);
                    not_en_reso.color = Color.white;
                    Debug.Log("Not Enough Health: Not Death");
                }
            }

        }
        if(is_card_used)
        {
            // update the sliders after use
            update_sliders();
            // assign a new card after use
            get_card();
        }
       
    }

    public void update_sliders()
    {
        player_health_slider.value = player.player_health;
        player_mana_slider.value = player.player_mana;
        enemy_health_slider.value = player.enemy_health;
    }

    void check_if_dead()
    {
        if(player.enemy_health <= 0)
        {
            player.is_enemy_alive = false;
            Debug.Log("Enemy Dead Hurray");
            GetComponent<sceneManager>().load_victory_screen();
        }
        else if(player.player_health <= 0)
        {
            player.is_player_alive = false;
            Debug.Log("Oh no!!,Player is Dead");
        }
    }

    public void select_card()
    {
        is_selected = true;
        glow_material.color = cards[card_index].associated_color;
        my_animator.SetBool("Deselect", false);
        my_animator.SetTrigger("Selected");
    }
    public void deselect_card()
    {
        is_selected = false;
        my_animator.SetBool("Deselect", true);
    }

    public void change_resources()
    {
        // using the xor operator will flip the sign
        player.is_using_mana = player.is_using_mana ^ true;
    }

    private void not_enough_resources()
    {
        not_en_reso.color = alpha_zero;
    }
}

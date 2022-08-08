using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerScript : MonoBehaviour
{
    public int player_mana, player_health, enemy_health;
    public bool is_using_mana = true;
    public bool is_player_alive = true;
    public bool is_enemy_alive = true;
    [SerializeField] Image resource_image;
    [SerializeField] Sprite heart_img, mana_img;
    void Start()
    {
        player_health = 300;
        player_mana = 300;
        enemy_health = 500;
        is_using_mana = true;
        is_player_alive = true;
        is_enemy_alive = true;
        update_sprite();
    }
    public void flip_resource()
    {
        is_using_mana = is_using_mana ^ true;
        // this will flip the bool
        update_sprite();
    }
    private void update_sprite()
    {
        if (is_using_mana)
            resource_image.sprite = mana_img;
        else
            resource_image.sprite = heart_img;
    }
    public void reshuffle_damage()
    {
        player_health /= 2;
    }
}

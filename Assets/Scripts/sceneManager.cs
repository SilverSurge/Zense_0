using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    //-------------------SerializeFields-----------//
    [SerializeField] int main_menu_index;
    [SerializeField] int victory_screen_index;
    [SerializeField] int defeat_screen_index;
    [SerializeField] PlayerScript player;

    private void Update()
    {
        if (!player.is_enemy_alive)
            SceneManager.LoadScene(victory_screen_index);
        else if (!player.is_player_alive)
            SceneManager.LoadScene(defeat_screen_index);
    }
    public void load_main_menu()
    {
        SceneManager.LoadScene(main_menu_index);
    }
    public void load_victory_screen()
    {
        SceneManager.LoadScene(victory_screen_index);
    }

    public void load_defeat_screen()
    {
        SceneManager.LoadScene(defeat_screen_index);
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RuleScene : MonoBehaviour
{
    [SerializeField] int main_menu_scene_index;
    [SerializeField] AudioSource my_audio_source;
    [SerializeField] float play_delay = 0f;


    public void load_main_menu()
    {
        my_audio_source.PlayDelayed(play_delay);
        SceneManager.LoadScene(main_menu_scene_index);
    }
}

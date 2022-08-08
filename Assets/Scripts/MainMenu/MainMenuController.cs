using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] int game_scene_index;
    [SerializeField] int rule_scene_index;
    [SerializeField] List<Animator> animators;
    [SerializeField] float audio_delay = 0f;
    int index = 0;
    AudioSource my_audio_source;
    void Start()
    {
        my_audio_source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index = (index + 1) % 3;
            animate_button(index);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            index--;
            index += index < 0 ? 3 : 0;
            animate_button(index);
        }
            
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter Pressed");
            if (index == 0)
                load_new_game();
            else if (index == 1)
                exit_game();
            else
                load_rules();

        }

    }
    private void animate_button(int idx)
    {
        if(idx == 0)
        {
            animators[0].SetBool("Select", true);
            animators[1].SetBool("Select", false);
            animators[2].SetBool("Select", false);
        }
        else if (idx == 1)
        {
            animators[0].SetBool("Select", false);
            animators[1].SetBool("Select", true);
            animators[2].SetBool("Select", false);
        }
        else if(idx == 2)
        {
            animators[0].SetBool("Select", false);
            animators[1].SetBool("Select", false);
            animators[2].SetBool("Select", true);
        }
        // my_audio_source.Play();
        my_audio_source.PlayDelayed(audio_delay);
    }
    public void select_index(int ind)
    {
        index = ind;
        animate_button(index);
    }
    private void exit_game()
    {
        // the code to close the game
        Application.Quit();
    }
    private void load_new_game()
    {
        SceneManager.LoadScene(game_scene_index);
    }

    private void load_rules()
    {
        SceneManager.LoadScene(rule_scene_index);
    }
}

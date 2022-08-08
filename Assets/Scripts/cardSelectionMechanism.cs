using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cardSelectionMechanism : MonoBehaviour
{
    [SerializeField] List<Card_Manager_1> card_buttons;
    [SerializeField] List<Button> button_components;
    [SerializeField] Button reshuffle_button;
    [SerializeField] Button resource_swap_button;
    [SerializeField] AudioSource my_audio_source;
    [SerializeField] float audio_delay = 0f;

    private int index = 0;
    private void Start()
    {
        index = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            index_calculator(0);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            index_calculator(1);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            index_calculator(2);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            index_calculator(3);
        else if (Input.GetKeyDown(KeyCode.R))
            reshuffle_button.onClick.Invoke();
        else if (Input.GetKeyDown(KeyCode.S))
            resource_swap_button.onClick.Invoke();

    }

    void index_calculator(int direction)
    {
        // directions
        // 0->up
        // 1->down
        // 2->right
        // 3->left
        if (direction == 0)
        {
            if (index == 0)
                index = 2;
            else if (index == 1)
                index = 3;
            else if (index == 2)
                index = 0;
            else
                index = 1;
        }
        else if (direction == 1)
        {
            if (index == 0)
                index = 2;
            else if (index == 1)
                index = 3;
            else if (index == 2)
                index = 0;
            else
                index = 1;
        }
        else if (direction == 2)
            index = (index + 1) % 4;
        else
        {
            index--;
            index += index < 0 ? 4 : 0;
        }
        button_components[index].onClick.Invoke();
    }

    public void set_index_value(int ind)
    {
        index = ind;
    }
    public void play_sound_effect()
    {
        my_audio_source.PlayDelayed(audio_delay);
    }
    
}

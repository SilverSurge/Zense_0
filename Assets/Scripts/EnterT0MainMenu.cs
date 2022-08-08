using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterT0MainMenu : MonoBehaviour
{
    [SerializeField] int main_menu_scene_index;   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene(main_menu_scene_index);
    }
}

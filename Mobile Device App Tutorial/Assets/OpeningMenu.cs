using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningMenu : MonoBehaviour
{
    public void MainMenuTransition()
    {
        SceneManager.LoadScene("Start Menu");
    }
}

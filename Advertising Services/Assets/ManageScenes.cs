using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void GoToPlayServices()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void IAPScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToPlayServices()
    {
        SceneManager.LoadScene("PlayServices");
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

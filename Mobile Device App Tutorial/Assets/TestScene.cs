﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene : MonoBehaviour
{
    public void TestingScene()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader
{
    
    public static void loadIt(string SceneName){
        SceneManager.LoadScene(SceneName);
    }
}

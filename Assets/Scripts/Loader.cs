using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    private static Scene targetScene;


    public enum Scene {
        MainMenu,
        Loading,
        GameScene,
    }


    public static void LoadScene(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
    
}

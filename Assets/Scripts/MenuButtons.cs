using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void play() {
        SceneManager.LoadSceneAsync("World");
    }

    public void help() {

    }

    public void exit() {
        Application.Quit();
    }
}

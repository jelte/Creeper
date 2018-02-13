using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtonController : MonoBehaviour {

    public void Next()
    {
        //TODO:Load this scenes
    }

    public void Rerty()
    {
        //TODO: reload this scenes
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {
    public void PlayMap1(){
        SceneManager.LoadScene("Flat");
    }
    public void PlayMap2()
    {
        SceneManager.LoadScene("Island");
    }
    public void PlayMap3()
    {
        SceneManager.LoadScene("Bowl");
    }
    public void PlayMap5()
    {
        SceneManager.LoadScene("MovingIslands");
    }
    public void PlayMap6()
    {
        SceneManager.LoadScene("RainingBombs");
    }
}

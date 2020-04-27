using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GamaScene");
    }

    public void TestScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckTest : MonoBehaviour
{
    //싱글톤 스크립트
    public List<GameObject> playerDeck;

    public static DeckTest instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            for (int i = 0; i < 5; i++)
            {
                string go = GameObject.Find("triggermanager").GetComponent<triggermanager>().lista[i].name;
                playerDeck[i] = GameObject.Find("triggermanager").GetComponent<triggermanager>().lista[i].gameObject;
            }
        }
        
    }
}

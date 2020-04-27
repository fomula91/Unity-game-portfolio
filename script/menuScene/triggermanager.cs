using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggermanager : MonoBehaviour
{
    // 0 = ar, 1 = smg, 2 = hg, 3 = rf, 4 = mg, 5 sg
    public List<GameObject> lista;
    public Sprite[] sprite;
    public GameObject[] deck;
    public GameObject findname;
    public GameObject deckSetPanel;
    public GameObject defaultPanel;
    public Mask panelMask;
    void Start()
    {
        LoadScript();
    }
    void LoadScript()
    {
        deck = new GameObject[5];
        sprite = Resources.LoadAll<Sprite>("Image/Icon");
        findname = GameObject.Find("PlayDeck").gameObject;
        panelMask = FindObjectOfType<Mask>();
        deckSetPanel = GameObject.Find("deckSetPanel");
        defaultPanel = GameObject.Find("defaultPanel");
        deckSetPanel.SetActive(false);
        updateDeck(findname);
    }
    void updateDeck(GameObject go)
    {
        for (int i = 0; i < 5; i++)
        {
            deck[i] = go.transform.GetChild(i).gameObject;
            string name = go.transform.GetChild(i).GetChild(2).GetComponent<Text>().text;
            lista.Add(Resources.Load<GameObject>($"Obj/Ingame/{name}"));
        }
    }
    void updateDeck()
    {
        for (int i = 0; i < 5; i++)
        {
            deck[i] = findname.transform.GetChild(i).gameObject;
            string name = findname.transform.GetChild(i).GetChild(2).GetComponent<Text>().text;
            lista[i] = (Resources.Load<GameObject>($"Obj/Ingame/{name}"));
        }
    }
    public bool dragImage(Transform tr)
    {
        //해당 오브젝트의 이름이 자신과 같은 이름인지 true/false로 리턴
        for(int i = 0; i < lista.Count; i++)
        {
            if (Vector2.Distance(deck[i].transform.position, tr.transform.position) < 40f && deck[i].transform.GetChild(2).GetComponent<Text>().text == tr.name)
            {
                return false;
            }
            if(Vector2.Distance(deck[i].transform.position, tr.transform.position) < 40f && deck[i].transform.GetChild(2).GetComponent<Text>().text != tr.name)
            {
                for(int j = 0; j < deck.Length; j++)
                {
                    if(deck[j].transform.GetChild(2).GetComponent<Text>().text == tr.name)
                    {
                        Sprite tempImg = deck[j].transform.GetChild(0).GetComponent<Image>().sprite;
                        Sprite tempIcon = deck[j].transform.GetChild(1).GetComponent<Image>().sprite;
                        string tempName = deck[j].transform.GetChild(2).GetComponent<Text>().text;
                        deck[j].transform.GetChild(0).GetComponent<Image>().sprite = deck[i].transform.GetChild(0).GetComponent<Image>().sprite;
                        deck[j].transform.GetChild(1).GetComponent<Image>().sprite = deck[i].transform.GetChild(1).GetComponent<Image>().sprite;
                        deck[j].transform.GetChild(2).GetComponent<Text>().text = deck[i].transform.GetChild(2).GetComponent<Text>().text;
                        deck[i].transform.GetChild(0).GetComponent<Image>().sprite = tempImg;
                        deck[i].transform.GetChild(1).GetComponent<Image>().sprite = tempIcon;
                        deck[i].transform.GetChild(2).GetComponent<Text>().text = tempName;
                        return false;
                    }
                }
                deck[i].transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Image/Icon/{tr.name}");
                deck[i].transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Image/UI/Icon/{tr.GetComponent<diceIndex>().indexNumbe}");
                deck[i].transform.GetChild(2).GetComponent<Text>().text = tr.name;
                return true;
            }
        }
            return false;
    }

    public void ChangeButton()
    {
        if(defaultPanel.activeSelf == true)
        {
            defaultPanel.SetActive(false);
            deckSetPanel.SetActive(true);
        }
        else if(deckSetPanel.activeSelf == true)
        {
            deckSetPanel.SetActive(false);
            defaultPanel.SetActive(true);
        }
    }

    void Update()
    {
        updateDeck();
    }
    
}

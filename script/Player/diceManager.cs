using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]

public class diceManager : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> playerDeck;
    public List<GameObject> dicePosition;
    public List<GameObject> diceChoose;
    public GameObject enemyManager;
    public GameObject SP;
    public GameObject LifeManager;
    public int SpSet;
    public int InitSp;
    public int Life = 3;

    int randDicePosition;
    int randemDeckNum;
    void Awake()
    {
        //함수가 시작될때 플레이어의 sp를 초기화한다
        SpSetting();
        //연결되있는 관련 스크립트를 불러온다.
        LoadScript();
        //초기 플레이어의 덱 정보를 싱글톤에 저장한다.
        PlayerDeckSet();
    }
    void SpSetting()
    {
        SpSet = 100;
        InitSp = 10;
    }
    void LoadScript()
    {
        enemyManager = GameObject.Find("enemyManager");
        LifeManager = GameObject.Find("LifeManager");
        SP = GameObject.Find("SpManager");
        bullet = Resources.Load<GameObject>("Obj/bullet");
    }
    void PlayerDeckSet()
    {
        for (int i = 0; i < DeckTest.instance.playerDeck.Count; i++)
        {
            //playerDeck.Add(Resources.Load<GameObject>($"Obj/Ingame/79type_SMG"));
            playerDeck.Add(Resources.Load<GameObject>($"Obj/Ingame/{DeckTest.instance.playerDeck[i].name}"));
        }

    }
    void Update()
    {
        //게임 중 플레이어의 자원을 실시간으로 검사한다.
        SpCheck();
        //게임 중 플레이어의 남은 목숨을 실시간으로 검사한다.
        lifeCheck();
        //적 오브젝트가 화면상에 존재하는지 혹은 플레이어의 목숨이 0가 됬는지 판단후 메뉴화면으로 씬을 전환한다
        SceneCheck();
    }
    void SpCheck()
    {
        SP.transform.GetChild(0).GetComponentInChildren<Text>().text = InitSp.ToString();
        SP.transform.GetChild(1).GetComponentInChildren<Text>().text = SpSet.ToString();
    }
    void SceneCheck()
    {
        if (enemyManager.GetComponent<enemy>().monsterNumber <= 0 && enemyManager.GetComponent<enemy>().enemyList.Count <= 0)
        {
            SceneManager.LoadScene("MenuScene");
        }

    }
    void lifeCheck()
    {
        switch (Life)
        {
            case 2:
                LifeManager.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/emoji2");
                break;
            case 1:
                LifeManager.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/emoji2");
                break;
            case 0:
                LifeManager.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/UI/emoji2");
                SceneManager.LoadScene("MenuScene");
                break;
        }
    }
    

    public void InitDice()
    {
        //버튼을 클릭했을때 임위의 위치에 플레이어의 캐릭터를 배치한다.
        if (dicePosition.Count <= 0 || SpSet < InitSp) return;
        SpSet -= InitSp;
        InitSp += 10;

        //플레이어 위치 및 플레이어 덱 인덱스 값을 무작위로 추출
        randDicePosition = Random.Range(0, dicePosition.Count);
        randemDeckNum = Random.Range(0, playerDeck.Count);

        //플레이어 오브젝트 생성
        GameObject go = Instantiate(playerDeck[randemDeckNum]);

        //플레이어 오브젝트위 위치 설정
        go.GetComponent<diceInfo>().PositionName = int.Parse(dicePosition[randDicePosition].name);
        go.transform.position = dicePosition[randDicePosition].transform.position;

        //하이라키 상 플레이어 캐릭터 이름 적용 ('캐릭터 이름' => '포지션 위치')
        go.name = $"{playerDeck[randemDeckNum].name} => {dicePosition[randDicePosition].transform.position.x} : {dicePosition[randDicePosition].transform.position.y}";
        //go.name = dicePosition[randDicePosition].transform.position.x + " : " + dicePosition[randDicePosition].transform.position.y;

        //선택된 위치를 리스트에 저장
        diceChoose.Add(dicePosition[randDicePosition]);

        //기존의 위치 리스트에 선택된 위치를 제거
        dicePosition.RemoveAt(randDicePosition);

        //캐릭터 오브젝트 자식에 발사체 생성
        Initbullet(go);
    }
    public void Initbullet(GameObject dice)
    {
        GameObject go = Instantiate(bullet);
        go.transform.SetParent(dice.transform);
        dice.GetComponent<diceInfo>().bulletList.Add(go);
        go.transform.position = dice.transform.position;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class diceMove : MonoBehaviour
{
    public Vector2 defaultPosition;
    public GameObject manager;
    public GameObject bullet;
    public List<GameObject> list;
    bool is_corrent;

   
    void Start()
    {
        LoadScript();
    }
    void LoadScript()
    {
        defaultPosition = transform.position;
        bullet = Resources.Load<GameObject>("Obj/bullet");
        manager = GameObject.Find("manager");
        list = manager.GetComponent<diceManager>().diceChoose;
    }
    private void OnMouseUp()
    {
        is_corrent = false;
        transform.position = defaultPosition;
    }
    private void OnMouseDown()
    {
        is_corrent = true;
    }

    void DestroyDice()
    {
        
        for (int i = 0; i < list.Count; i++)
        {
            if (gameObject.GetComponent<diceInfo>().PositionName.ToString() == list[i].name)
            {
                Destroy(gameObject);
                manager.GetComponent<diceManager>().dicePosition.Add(list[i]);
                list.RemoveAt(i);
            }
        }
    }
    bool isDiceIndex(GameObject go, Collider target)
    {
        if (go.GetComponent<diceInfo>().diceIndex == target.GetComponent<diceInfo>().diceIndex)
        {
            return true;
        }
        else return false;
    }
    void ReInitBullet(GameObject dice, int level)
    {
        //레벨업시 발사체를 새로 추가하는 함수
        switch (level)
        {
            case 1:
                for(int i = 0; i < 2; i++)
                {
                    GameObject go = Instantiate(bullet);
                    dice.GetComponent<diceInfo>().bulletList.Add(go);
                    dice.GetComponent<diceInfo>().bulletList[i].name = "bullet " + i;
                    go.transform.SetParent(dice.transform);
                    
                }
                dice.GetComponent<diceInfo>().bulletList[0].transform.position = dice.transform.position;
                dice.GetComponent<diceInfo>().bulletList[1].transform.position = dice.transform.position + new Vector3(-0.25f, -0.25f, 0);
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    GameObject go = Instantiate(bullet);
                    dice.GetComponent<diceInfo>().bulletList.Add(go);
                    dice.GetComponent<diceInfo>().bulletList[i].name = "bullet " + i;
                    go.transform.SetParent(dice.transform);

                }
                dice.GetComponent<diceInfo>().bulletList[0].transform.position = dice.transform.position;
                dice.GetComponent<diceInfo>().bulletList[1].transform.position = dice.transform.position + new Vector3(-0.25f, -0.25f, 0);
                dice.GetComponent<diceInfo>().bulletList[2].transform.position = dice.transform.position + new Vector3(0.25f, 0.25f, 0);
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    GameObject go = Instantiate(bullet);
                    dice.GetComponent<diceInfo>().bulletList.Add(go);
                    dice.GetComponent<diceInfo>().bulletList[i].name = "bullet " + i;
                    go.transform.SetParent(dice.transform);

                }
                dice.GetComponent<diceInfo>().bulletList[0].transform.position = dice.transform.position;
                dice.GetComponent<diceInfo>().bulletList[1].transform.position = dice.transform.position + new Vector3(-0.25f, -0.25f, 0);
                dice.GetComponent<diceInfo>().bulletList[2].transform.position = dice.transform.position + new Vector3(0.25f, 0.25f, 0);
                dice.GetComponent<diceInfo>().bulletList[3].transform.position = dice.transform.position + new Vector3(-0.25f, 0.25f, 0);
                break;
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    GameObject go = Instantiate(bullet);
                    dice.GetComponent<diceInfo>().bulletList.Add(go);
                    dice.GetComponent<diceInfo>().bulletList[i].name = "bullet " + i;
                    go.transform.SetParent(dice.transform);

                }
                dice.GetComponent<diceInfo>().bulletList[0].transform.position = dice.transform.position;
                dice.GetComponent<diceInfo>().bulletList[1].transform.position = dice.transform.position + new Vector3(-0.25f, -0.25f, 0);
                dice.GetComponent<diceInfo>().bulletList[2].transform.position = dice.transform.position + new Vector3(0.25f, 0.25f, 0);
                dice.GetComponent<diceInfo>().bulletList[3].transform.position = dice.transform.position + new Vector3(-0.25f, 0.25f, 0);
                dice.GetComponent<diceInfo>().bulletList[4].transform.position = dice.transform.position + new Vector3(0.25f, -0.25f, 0);
                break;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (is_corrent && Input.GetMouseButtonUp(0))
        {
            //캐릭터가 같은 장소와 같은 레벨일 경우를 검사하여 부딪친 두개의 오브젝트를 지우고 새로운 오브젝트를 그 자리에 생성한다.
            if (Vector2.Distance(transform.position, other.transform.position) < 1f && other.CompareTag("Dice") && isDiceIndex(gameObject,other) == true && GetComponent<diceInfo>().diceLevel == other.GetComponent<diceInfo>().diceLevel)
            {
                int level = other.GetComponent<diceInfo>().diceLevel;
                //일정 레벨을 초과한 경우 합치는걸 방지함
                if (level >= 5) return;
                //다이스를 지우는 함수
                DestroyDice();
                //레벨정보를계승하여 새로운 캐릭터 오브젝트를 생성하는 함수
                ResetDice(other, level);
            }
        }
    }
    
    void ResetDice(Collider other, int level)
    {
        int r = Random.Range(0, manager.GetComponent<diceManager>().playerDeck.Count);
        GameObject go = Instantiate(manager.GetComponent<diceManager>().playerDeck[r]);
        go.GetComponent<diceInfo>().PositionName = other.GetComponent<diceInfo>().PositionName;
        go.GetComponent<diceInfo>().diceLevel = other.GetComponent<diceInfo>().diceLevel;
        go.transform.position = other.transform.position;
        go.name = other.gameObject.name;
        Destroy(other.gameObject);
        go.name = $"{manager.GetComponent<diceManager>().playerDeck[r].name} => {go.transform.position.x} : {go.transform.position.y}";
        go.GetComponent<diceInfo>().diceLevel++;
        ReInitBullet(go, level);
    }
    void OnMouseDrag()
    {
        //다이스를 선택하고 터치포인트 위치를 따라가는 코드
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = objPosition;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //적 오브젝트 관리 스크립트
    private GameObject Road;
    public List<GameObject> enemyList;
    public List<GameObject> enemySet = new List<GameObject>();
    private GameObject[] enemyPrefab;
    private int number;
    public int monsterNumber = 100;
    private WaitForSeconds ws = new WaitForSeconds(1.5f);
    void InitEnemy()
    {
        //적 오브젝트의 리스트에서 한개의 적 오브젝트를 랜덤하게 선택한다.
        int random = Random.Range(0, enemySet.Count);
        number++;
        // 적 오브젝트를 생성후 정해준 루트 시작점에 위치시키고 적 오브젝트 리스트에 추가한다.
        GameObject go = Instantiate(enemySet[random]);
        go.name = number.ToString();
        go.transform.position = Road.transform.GetChild(0).position;
        enemyList.Add(go);
    }
    void Start()
    {
        LoadScript();
        StartCoroutine(set());
    }
    void LoadScript()
    {
        Road = GameObject.Find("RoadManager");
        enemyPrefab = Resources.LoadAll<GameObject>("Obj/Ingame/enemylist");
        //Debug.Log(enemyPrefab.Length);
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            enemySet.Add(enemyPrefab[i]);
        }
    }
    IEnumerator set()
    {
        while (true)
        {
            InitEnemy();
            monsterNumber--;
            if (monsterNumber <= 0)
            {
                //1웨이브가 끝나면 코루틴을 종료한다.
                StopAllCoroutines();
            }
            yield return ws;
        }
    }
}

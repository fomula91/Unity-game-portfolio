using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //발사체 오브젝트의 스크립트
    GameObject enemyManager;
    public GameObject target;
    public List<GameObject> enemyList;
    private Vector3 defaultPos;
    private GameObject road;
    private int bulletAtk;
    private float bulletSpeed;

    //발사체가 활성화 되어있는가?
    bool isbulletActive = true;
    private void Awake()
    {
        LoadScript();
    }
    void LoadScript()
    {
        road = GameObject.Find("RoadManager");
        enemyManager = GameObject.Find("enemyManager");
        // enemyManager에서 리스트를 가져옴
        enemyList = enemyManager.GetComponent<enemy>().enemyList;
        //발렛 오브젝트 생성시 타겟 초기 설정
        if (enemyManager.GetComponent<enemy>().enemyList.Count > 0)
        {
            target = enemyManager.GetComponent<enemy>().enemyList[0].gameObject;
        }
    }
    private void Start()
    {
        //자기 자신의 위치를 기억한다
        defaultPos = transform.position;
        //자기 부모오브젝트의 캐릭터 정보를 가져와 저장한다
        bulletSpeed = transform.GetComponentInParent<diceInfo>().atkSpeed;
        bulletAtk = transform.GetComponentInParent<diceInfo>().atk;

    }
    void Update()
    {
        //발사체가 타겟을 찾았는가 확인하는 함수
        BulletCheck();
        //발사체가 타겟으로 이동하는 함수
        BulletMove();
    }
    void Diration()
    {
        //발사체의 이동경로의 우선순위를 계산하는 함수
        //임시 저장할 리스트
        List<GameObject> tempEnemyListTarget = new List<GameObject>();
        int count = 0;
        
        //적 오브젝트가 향하는 방향을 순서로 임시 리스트에 저장
        for (int i = 0; i < enemyList.Count; i++)
        {
            count = enemyList[i].GetComponent<enemyMove>().GetNum;
            //Debug.Log("몬스터가 가는 방향 : "+count);
            if (count == 3)
            {
                if (enemyList[i] != null)
                {
                    tempEnemyListTarget.Add(enemyList[i]);
                    continue;
                }

            }
            else if (count == 2)
            {
                if (enemyList[i] != null)
                {
                    tempEnemyListTarget.Add(enemyList[i]);
                    continue;
                }

            }
            else if (count == 1)
            {
                if (enemyList[i] != null)
                {
                    tempEnemyListTarget.Add(enemyList[i]);
                    continue;
                }

            }
        }

        target = GetTarget(tempEnemyListTarget);
        tempEnemyListTarget.Clear();
        //enemy_list.Clear();

    }

    GameObject GetTarget(List<GameObject> obj_list)
    {
        //리스트 내에 가장 앞서는 적 오브젝트를 추적하여 타겟으로 설정하는 함수
        if (obj_list.Count < 0 || obj_list == null) return  null;
        int count = 0;
        int targetNum=0;
        GameObject target_pos;
        for (int i= 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].GetComponent<enemyMove>().GetNum >= 3)
            {
                targetNum = enemyList[i].GetComponent<enemyMove>().GetNum;
            }
        }
        if (targetNum == 3)
        {
            target_pos = road.transform.GetChild(3).gameObject;
        }
        else if(targetNum == 2)
        {
            target_pos = road.transform.GetChild(2).gameObject;
        }
        else
        {
            target_pos = obj_list[0].GetComponent<enemyMove>().GetTarget;
        }

        //타겟 오브젝트와 리스트 오브젝트를 비교하여 어떤것을 우선해야하는 지 판단하는 함수
        float distance = Vector3.Distance(target_pos.transform.position, obj_list[0].transform.position);
        for (int i = 0; i < obj_list.Count; i++)
        {
            if (distance > Vector3.Distance(target_pos.transform.position, obj_list[i].transform.position))
            {
                distance = Vector3.Distance(target_pos.transform.position, obj_list[i].transform.position);
                count = i;
            }
        }

        return obj_list[count];
    }
    
    
    void BulletCheck()
    {
        //발사체 오브젝트가 적이 있을때와 적이 없을때의 상태를 결정하는 함수
        //발사체 오브젝트가 활성화 되있고 적 리스트가 없을 때
        if (isbulletActive == true && enemyList.Count <= 0)
        {
            isbulletActive = false;
            gameObject.SetActive(false);
            transform.position = defaultPos;
            target = null;
        }
        //발사체가 비활성화 되어있고 적 리스트가 존재할 시
        if (isbulletActive == false && enemyList.Count > 0)
        {
            isbulletActive = true;
            Diration();
        }
        //발사체가 활성화 되있고 타겟 설정이 null이 아닌경우
        if(isbulletActive == true && target != null)
        {
            if (isbulletActive == true && target.GetComponent<enemyInfo>().HP <= 0)
            {
                // 발사체가 활성화되있고 타겟의 HP가 0보다 작은 경우
                target = null;
                Diration();
            }
            BulletMove();
        }
    }
    //발사체 오브젝트의 이동 함수
    void BulletMove()
    {
        if (target == null) return;
        if (isbulletActive == true && Vector3.Distance(transform.position, target.transform.position) < 0.2f)
        {
            isbulletActive = false;
            gameObject.SetActive(false);
            transform.position = defaultPos;
            target.GetComponent<enemyInfo>().HP -= bulletAtk;

        }
        else if (isbulletActive == true && Vector2.Distance(transform.position, target.transform.position) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * bulletSpeed);
        }
    }
}

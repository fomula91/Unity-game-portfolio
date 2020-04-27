using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;
public class enemyMove : MonoBehaviour
{
    public GameObject target;
    public GameObject manager;
    public GameObject spManager;
    public enemyInfo info;
    int num = 1;
    public SkeletonAnimation enemySA;
    private string cur_animation = "";
    bool isdie;

    public GameObject GetTarget
    {
        //적 오브젝트의 거리를 비교할때 기준이 되는 루트 포인트
        get { return target.transform.GetChild(2).gameObject; }
    }

    public int GetNum
    {
        get { return num; }
    }

    void Start()
    {
        LoadScript();
    }
    void LoadScript()
    {
        isdie = false;
        enemySA = gameObject.GetComponent<SkeletonAnimation>();
        spManager = GameObject.Find("manager");
        info = gameObject.GetComponent<enemyInfo>();
        target = GameObject.Find("RoadManager");
        manager = GameObject.Find("enemyManager");
        enemySA.skeleton.ScaleX = -Mathf.Abs(enemySA.skeleton.ScaleX);
    }
    void Update()
    {
        if(isdie == false)
        {
            HpCheck();
            moveTarget();
        }
        
    }
    void HpCheck()
    {
        //적 오브젝트의 체력을 검사하는 함수
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = info.HP.ToString();
        if (info.HP <= 0)
        {
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = 0.ToString();
            manager.GetComponent<enemy>().enemyList.Remove(transform.gameObject);
            isdie = true;
            spManager.GetComponent<diceManager>().SpSet = spManager.GetComponent<diceManager>().SpSet + 10;
            enemyAnimation("die", false, 1);
            Destroy(gameObject, 1f);
            
        }
    }
    void enemyAnimation(string name, bool loop, float speed)
    {
        //적 오브젝트의 애니메이션 호출 함수
        if (name == cur_animation)
        {
            return;
        }
        else
        {
            enemySA.state.SetAnimation(0, name, loop).TimeScale = speed;
            cur_animation = name;
        }
    }
    private void moveTarget()
    {
        //적 오브젝트가 목표지점까지 이동하는 함수
        if(isdie == true) return;
        if(isdie == false)
        {
            if (transform.position == target.transform.GetChild(3).position)
            {
                manager.GetComponent<enemy>().enemyList.Remove(transform.gameObject);
                spManager.GetComponent<diceManager>().Life -= 1;
                Destroy(transform.gameObject);
            }

            else if (Vector3.Distance(transform.position, target.transform.GetChild(num).position) == 0)
            {
                
                if (num == 2) { enemySA.skeleton.ScaleX = Mathf.Abs(enemySA.skeleton.ScaleX); }
                transform.position = target.transform.GetChild(num).position;
                num++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.GetChild(num).position, Time.deltaTime * info.SPEED);
            }
        }
        


    }
}

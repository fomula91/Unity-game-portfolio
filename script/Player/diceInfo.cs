using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class diceInfo : MonoBehaviour
{
    public List<GameObject> bulletList;
    public int PositionName;
    public int diceLevel = 1;
    public int diceIndex;
    // 0 = ar, 1 = smg, 2 = hg, 3 = rf, 4 = mg, 5 sg
    public int atk;
    public float atkSpeed;
    [System.NonSerialized]
    public int bullets;
    public float bulletDelay;
    
    public GameObject enemy;
    public GameObject road;
    public SkeletonAnimation player;
    private string cur_animation = "";

    private void Start()
    {
        LoadScript();
        //총알 딜레이 코루틴
        StartCoroutine(bulletDalay(bulletDelay));
    }
    void LoadScript()
    {
        road = GameObject.Find("RoadManager");
        player = gameObject.GetComponent<SkeletonAnimation>();
        enemy = GameObject.Find("enemyManager");
    }
    IEnumerator bulletDalay(float delay)
    {
        WaitForSeconds ws = new WaitForSeconds(delay);
        while (true)
        {
            yield return ws;
            isBulletFalse();
        }
    }
    private void Update()
    {
        AnimationCheck();
        //다이스가 겹쳐질시 다이스에 표시되는 숫자를 변경
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = diceLevel.ToString();
    }
    void DiceAnimation(string name, bool loop, float speed)
    {
        //플레이어 캐릭터의 애니메이션을 호출하는 함수
        if (name == cur_animation)
        {
            return;
        }
        else
        {
            player.state.SetAnimation(0, name, loop).TimeScale = speed;
            cur_animation = name;
        }
    }

    void AnimationCheck()
    {
        //타겟과 적 리스트의 존재 여부에 따라 애니메이션을 변경
        GameObject target = gameObject.transform.GetChild(1).GetComponent<bullet>().target;
        if(target == null && enemy.GetComponent<enemy>().enemyList.Count == 0)
        {
            DiceAnimation("victory", true, 1.25f);
            player.state.AddAnimation(0, "victoryloop", true, 0f);
        }
        if (target != null && Vector2.Distance(target.transform.position, road.transform.GetChild(2).transform.position) > 5f)
        {
            player.skeleton.ScaleX = -Mathf.Abs(player.skeleton.ScaleX);
            DiceAnimation("attack", true, 1f);
        }
        if (target != null && Vector2.Distance(target.transform.position, road.transform.GetChild(2).transform.position) < 5f || target != null && Vector2.Distance(target.transform.position, road.transform.GetChild(3).transform.position) < 8f)
        {
            player.skeleton.ScaleX = Mathf.Abs(player.skeleton.ScaleX);
            DiceAnimation("attack", true, 1f);
        }
    }
    void isBulletFalse()
    {
        //적 오브젝트의 존재에 따라 발사체 오브젝트를 활성/비활성화
        int count = enemy.GetComponent<enemy>().enemyList.Count;
        if (bulletList.Count > 0 && count > 0)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].gameObject.activeSelf == false)
                {
                    bulletList[i].gameObject.SetActive(true);
                }
            }
        }
        else if(bulletList.Count > 0 && count <= 0)
        {
            for(int i = 0; i < bulletList.Count; i++)
            {
                if(bulletList[i].gameObject.activeSelf == true)
                {
                    bulletList[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

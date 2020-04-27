using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chacteritem : MonoBehaviour
{
    private triggermanager manager;
    Vector2 pos;
    void Start()
    {
        manager = GameObject.Find("triggermanager").GetComponent<triggermanager>();
        pos = transform.position;
    }

    public void diceDrag()
    {
        //드래그 함수를 호출중일때 마스크를 일시적으로 비활성화 한다
        if (manager.panelMask.enabled) manager.panelMask.enabled = false;
        //transform.SetSiblingIndex(100);
        //해당 아이콘의 위치를 자신의 마우스위치(터치중인 위치)로 옮긴다(드래그 구현)
        transform.position = Input.mousePosition;
    }

    public void dragUp()
    {
        //드래그 입력이 끝날때 해당위치에 오브젝트가 있는 지 검사한후 비활성화된 마스크를 다시 활성화
        if (manager.dragImage(transform))
        {
            transform.position = pos;
            manager.panelMask.enabled = true;
        }
        else
        {
            transform.position = pos;
            manager.panelMask.enabled = true;
        }
    }

}

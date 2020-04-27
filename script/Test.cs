//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Spine.Unity;
//using UnityEngine.EventSystems;


//public class Test : MonoBehaviour,IDragHandler, IEndDragHandler,IDropHandler
//{
//    public Vector2 defualtPos;
//    bool isCurrent;
//    public List<GameObject> deckUI;
//    public RectTransform rect;
//    //public SkeletonAnimation anim;
//    //private string cur_animation = "";

//    private void Awake()
//    {
//        rect = GetComponent<RectTransform>();
//        defualtPos = rect.anchoredPosition;
//        //anim = transform.GetChild(0).GetComponent<SkeletonAnimation>();
//        deckUI = GameObject.Find("MenuUI").GetComponent<MenuDeckTest>().UIList;
//    }
//    private void Start()
//    {
//        //DiceAnimation("wait", true, 1f);
        
//    }
//    //void DiceAnimation(string name, bool loop, float speed)
//    //{
//    //    if (name == cur_animation)
//    //    {
//    //        return;
//    //    }
//    //    else
//    //    {
//    //        anim.state.SetAnimation(0, name, loop).TimeScale = speed;
//    //        cur_animation = name;
//    //    }
//    //}
//    private void OnMouseUp()
//    {
//        Debug.Log("up");
//        isCurrent = false;
//        transform.position = defualtPos;
//        //DiceAnimation("wait", true, 1f);
//    }
//    private void OnMouseDown()
//    {
//        Debug.Log("down");
//        isCurrent = true;
//        //DiceAnimation("move", true, 1f);
//    }
//    private void OnMouseDrag()
//    {
//        Debug.Log("Mouse Drag");
//        Vector2 objPosition = Input.mousePosition;
//        rect.position = objPosition;
//    }
//    private void OnTriggerStay(Collider other)
//    {
//        if(isCurrent && Input.GetMouseButtonUp(0))
//        {
            
//            if (Vector2.Distance(transform.position, other.transform.position) <= 0.8f && gameObject.name == other.gameObject.name) return;
//            if (Vector2.Distance(transform.position, other.transform.position) <= 0.8f && gameObject.name != other.gameObject.name)
//            {
//                for (int i = 0; i < deckUI.Count; i++)
//                {
//                    if (gameObject.name == deckUI[i].name)
//                    {
//                        int index = 0;
//                        for (int j = 0; j < deckUI.Count; j++)
//                        {
//                            if (other.gameObject.name == deckUI[j].gameObject.name)
//                            {

//                                index = j;
//                            }
//                        }

//                        GameObject temp = deckUI[i].gameObject;
//                        Vector2 tempVec = deckUI[i].transform.position;
//                        Vector2 tempVec2 = other.transform.position;

//                        deckUI[i] = deckUI[index];

//                        deckUI[i].transform.position = tempVec;
//                        deckUI[i].GetComponent<Test>().defualtPos = tempVec;
//                        deckUI[index] = temp;
//                        deckUI[index].transform.position = tempVec2;
//                        deckUI[index].GetComponent<Test>().defualtPos = tempVec2;

//                        break;
//                    }

//                    else if(other.gameObject.name == deckUI[i].name)
//                    {
//                        int index = 0;
//                        for(int j = 0; j < deckUI.Count; j++)
//                        {
//                            if(gameObject.name == deckUI[j].name)
//                            {
//                                index = j;
//                                break;
//                            }
//                        }
//                        if(gameObject.name == deckUI[index].name)
//                        {
//                            GameObject temp = deckUI[i].gameObject;
//                            Vector2 tempVec = deckUI[i].transform.position;
//                            Vector2 tempVec2 = deckUI[index].transform.position;
//                            deckUI[i] = deckUI[index].gameObject;
//                            deckUI[i].transform.position = tempVec;
//                            deckUI[i].GetComponent<Test>().defualtPos = tempVec;
//                            deckUI[index] = temp;
//                            deckUI[index].transform.position = tempVec2;
//                            deckUI[index].GetComponent<Test>().defualtPos = tempVec2;
//                            break;
//                        }
//                        else
//                        {
//                            GameObject go = Instantiate(gameObject);
//                            go.name = gameObject.name;
//                            go.transform.position = other.transform.position;
//                            //DeckTest.instance.playerDeck.RemoveAt(i);
//                            Destroy(deckUI[i].gameObject);
//                            deckUI[i] = go;
//                            //DeckTest.instance.playerDeck.Insert(i, Resources.Load<GameObject>($"Obj/{go.name}"));
//                            break;
//                        }
                        

//                    }
                    
//                }
//            }
            
//        }
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        Debug.Log("Mouse Drag");
//        Vector2 objPosition = Input.mousePosition;
//        rect.position = objPosition;
//    }
//    public void OnEndDrag(PointerEventData eventData)
//    {
//        isCurrent = false;
//        rect.anchoredPosition = defualtPos;
//    }

//    public void OnDrop(PointerEventData eventData)
//    {
//        Debug.Log(eventData);
//    }
//}

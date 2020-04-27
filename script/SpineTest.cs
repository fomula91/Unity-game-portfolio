using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineTest : MonoBehaviour
{
    private string cur_animation = "";
    public SkeletonAnimation player;
    void Start()
    {
        SetAnimation("move", true, 1);
    }

    void SetAnimation(string name, bool loop, float speed)
    {
        if(name == cur_animation)
        {
            return;
        }
        else
        {
            player.state.SetAnimation(0, name, loop).TimeScale = speed;
            cur_animation = name;
        }
    }
}

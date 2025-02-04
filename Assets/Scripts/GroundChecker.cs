using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("エフェクトがついた床を判定するか")] public bool checkPlatformGroud = true;

    private bool isGround = false;
    private bool onGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    public bool OnGround()
    {
        return onGround;
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isGroundEnter = true;
            onGround = true;
        }
        else if (checkPlatformGroud && (col.tag == "Surinuke" || col.tag == "Movefloor"))
        {
            isGroundEnter = true;
            onGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isGroundStay = true;
            onGround = false;
        }
        else if (checkPlatformGroud && (col.tag == "Surinuke" || col.tag == "Movefloor")) 
        {
            isGroundStay = true;
            onGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isGroundExit = true;
            onGround = false;
        }
        else if (checkPlatformGroud && (col.tag == "Surinuke" || col.tag == "Movefloor"))
        {
            isGroundExit = true;
            onGround = false;
        }
    }
}

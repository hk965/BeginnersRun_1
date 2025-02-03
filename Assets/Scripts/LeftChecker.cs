using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftChecker : MonoBehaviour
{
    private bool isLeft = false;
    private bool isLeftEnter, isLeftStay, isLeftExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsLeft()
    {
        if (isLeftEnter || isLeftStay)
        {
            isLeft = true;
        }
        else if (isLeftExit)
        {
            isLeft = false;
        }
        

        isLeftEnter = false;
        isLeftStay = false;
        isLeftExit = false;
        return isLeft;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy") 
        {
            isLeftEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isLeftStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isLeftExit = true;
        }
    }
}

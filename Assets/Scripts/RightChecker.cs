using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightChecker : MonoBehaviour
{
    private bool isRight = false;
    private bool isRightEnter, isRightStay, isRightExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsRight()
    {
        if (isRightEnter || isRightStay)
        {
            isRight = true;
        }
        else if (isRightExit)
        {
            isRight = false;
        }
        

        isRightEnter = false;
        isRightStay = false;
        isRightExit = false;
        
        return isRight;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isRightEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isRightStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground" || col.tag == "Enemy")
        {
            isRightExit = true;
        }
    }
}

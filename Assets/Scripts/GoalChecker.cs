using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    private bool isGoal = false;
    private bool isGoalEnter, isGoalStay, isGoalExit;


    public bool IsGoal()
    {
        if (isGoalEnter /*|| isGoalStay*/)
        {
            isGoal = true;
        }
        /*else if (isGoalExit)
        {
            isGoal = false;
        }*/

        isGoalEnter = false;
        /*isGoalStay = false;
        isGoalExit = false;*/
        return isGoal;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isGoalEnter = true;
        }
    }

    /*private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isGoalStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isGoalExit = true;
        }
    }*/
}

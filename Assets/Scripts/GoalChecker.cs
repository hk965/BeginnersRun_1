using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    private bool isGoal = false;
    private bool isGoalEnter;


    public bool IsGoal()
    {
        if (isGoalEnter)
        {
            isGoal = true;
        }
       
        isGoalEnter = false;

        return isGoal;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isGoalEnter = true;
        }
    }

    
}

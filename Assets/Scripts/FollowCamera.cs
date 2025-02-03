using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject playerObj;
    Transform playerTransform;
    void Start()
    {
        playerTransform = playerObj.transform;
    }
    void LateUpdate()
    {
        MoveCamera();
    }
    void MoveCamera()
    {
        transform.position = new Vector3(playerTransform.position.x + 15.570001f, playerTransform.position.y + 5.53892f, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChelsMovment : MonoBehaviour
{

    [SerializeField] float speed;
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        FlipEnemyFacing();
    }
    private void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}

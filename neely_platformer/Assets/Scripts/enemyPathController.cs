using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public float movementSpeed = 5;
    public float knockbackModifier = 2;

    public bool pathFollower = true;


    // Start is called before the first frame update
    void Start()
    {
        // Set our script variables equal to the components and objects that exist in the game world so we can modify them
        myRB = GetComponent<Rigidbody2D>();
        velocity.y = -1 * movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pathFollower)
        {
            if (collision.gameObject.name.Contains("topTrigger"))
            {
                velocity.y = -1 * movementSpeed;
            }

            else if (collision.gameObject.name.Contains("bottomTrigger"))
            {
                velocity.y = 1 * movementSpeed;
            }
        }

        else if (!pathFollower)
        { 
            if (collision.gameObject.name.Contains("rightTrigger"))
            {
                velocity.y = 1 * movementSpeed;
                velocity.x = 0;
            }

            else if (collision.gameObject.name.Contains("leftTrigger"))
            {
                velocity.y = -1 * movementSpeed;
                velocity.x = 0;
            }

            else if (collision.gameObject.name.Contains("topTrigger"))
            {
                velocity.x = -1 * movementSpeed;
                velocity.y = 0;
            }

            else if (collision.gameObject.name.Contains("bottomTrigger"))
            {
                velocity.x = 1 * movementSpeed;
                velocity.y = 0;
            }
        }
    }
}            

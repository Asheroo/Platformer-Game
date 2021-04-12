using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Vector2 velocity;
    public GameObject playerTarget;
    public float movementSpeed = 5;
    public float knockbackModifier = 2;
    public bool isFollowing = false;
    public bool shield = true;

    //Shield Mechanic
    public Color unshieldedColor = new Color(0, 0, 365);
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Set our script variables equal to the components and objects that exist in the game world so we can modify them
        myRB = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.Find("player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If enemy's health is zero, time for it to go bye-bye

        // Calculate the position the enemy needs to look in order to see the player
        Vector3 lookPos = playerTarget.transform.position - transform.position;

        // Option angle computation code in the off chance your sprite may need to rotate to face the player similar to our top-down game
        //float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //myRB.rotation = angle;

        // Normalize the look position so we get cleaner values to work with when calculating our velocity
        lookPos.Normalize();

        // Temporarily store our velocity from our last frame in case we need to preserve the movement from the last frame
        velocity = myRB.velocity;

        // If we're not following the player, keep our velocity at 0
        if (!isFollowing)
        {
            velocity.x = 0;
        }

        // If we are following the player, set the horizontal velocity to be equal to the x position of where our enemy is looking and amplify that by our movement speed.
        if (isFollowing)
        {
            velocity.x = lookPos.x * movementSpeed;

            // Checking to see if we're moving to the right (point sprite to the right)
            if (velocity.x > 0)
                GetComponent<SpriteRenderer>().flipX = false;

            // Checking to see if we're moving to the left (point sprite to the left)
            else if (velocity.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
        }

        // Give our Rigibody our temporary velocity that we may or may have altered depending on the siutation
        myRB.velocity = velocity;
    }

    // If our circle trigger collider collides with the player and we aren't already following the player, tell the enemy to follow the player.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFollowing && (collision.gameObject.name == "player"))
            isFollowing = true;
    }

    // If our player leaves our circle trigger collider, the enemy should stop following the player if it already is.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isFollowing && (collision.gameObject.name == "player"))
            isFollowing = false;
    }

}            

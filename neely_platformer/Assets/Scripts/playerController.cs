using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Quaternion zero;
    private Vector2 respawnPos;

    //These variables are for the dash mechanic
    private float dashCooldown = 0;
    public bool dashActive = false;
    private float timeDifference2;
    private float dashLength = 2;



    public Vector2 velocity;
    public float speed = 5;
    public float jumpHeight = 6.25f;
    public bool isGrounded = true;
    public int health = 3;
    public int knockbackModifier = 5;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

        respawnPos = new Vector2(0, 2);
        zero = new Quaternion();

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            transform.SetPositionAndRotation(respawnPos, zero);
            health = 3;
        }

        velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpHeight;
            isGrounded = false;
        }

        myRB.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashActive = true;
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.blue;
        }


        if (dashActive == true)
        {
            timeDifference2 += Time.deltaTime;
            speed = 10;

            if (timeDifference2 >= dashLength)
            {
                dashActive = false;
                speed = 5;
                timeDifference2 = 0;
                GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            isGrounded = true;
        }

        if (collision.gameObject.name.Contains("endGoal1"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().nextLevel = 1;
        }

        if (collision.gameObject.name.Contains("endGoal2"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().nextLevel = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("enemy") && !dashActive)
        {
            health--;
        }

        else if (collision.gameObject.name.Contains("pickup") && health < 3)
        {
            health = 3;
            collision.gameObject.SetActive(false);
        }
    }
}

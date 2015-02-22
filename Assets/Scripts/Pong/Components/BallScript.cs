using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
    public float speed = 3.0f;
    public float maxSpeed = 10.0f;

    private Vector3 startLocation = new Vector3(0,0,0);
    private int direction;

    public Sprite[] possibleSprites;

    private AudioSource bounce;

    void Start()
    {
        bounce = GetComponent<AudioSource>();

        ResetBall();
    }
	
    public void ResetBall()
    {
        Debug.Log("ResetBall");
        GetComponent<SpriteRenderer>().sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        speed += 0.5f;
        speed = Mathf.Min(speed, maxSpeed);
        gameObject.transform.position = startLocation;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.Sleep();
    }

    public void LaunchBall()
    {
        Debug.Log("LaunchBall");
        rigidbody2D.WakeUp();
        direction = (Random.Range(0, 2) > 0 ? 1 : -1);
        rigidbody2D.velocity = Vector2.one.normalized * speed * direction;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        bounce.Play();
        if (coll.collider.CompareTag("Player"))
        {
            direction = (direction == 1 ? -1 : 1);
            speed += 0.1f;
            speed = Mathf.Min(speed, maxSpeed);

            // Calculate hit Factor
            float y = Ricochet(transform.position,
                                coll.transform.position,
                                coll.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(direction, y).normalized;

            // Set Velocity with dir * speed
            rigidbody2D.velocity = dir * speed;
        }
    }

    float Ricochet(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }
}

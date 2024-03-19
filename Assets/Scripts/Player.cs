using UnityEngine;

public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal")*2, rigidbody2D.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        rigidbody2D.velocity = Vector2.up * 10;
    }
}

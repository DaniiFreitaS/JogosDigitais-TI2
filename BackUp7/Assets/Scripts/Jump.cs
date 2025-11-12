using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 20f; 
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 5f;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; //Da uma pequena escalada na quina de objetos
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //O ForceMode.Impulse faz o player acekerar pro salto dependendo da massa (podemos usar disso se quiser)
            isGrounded = false;//Quando eu pular, ja não vou estar no chao :p
        }
    }

    void OnCollisionEnter(Collision collision)//Vai comparar a TAG para ver se tocou no chão, se sim, vai ser true, se não, vai ser false
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

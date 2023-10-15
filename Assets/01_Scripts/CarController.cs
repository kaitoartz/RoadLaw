using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 10f;
    private float maxY = -1f;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Street")
        {
            gameObject.SetActive(true);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.y < maxY)
        {
            gameObject.SetActive(false);
        }
    }
}

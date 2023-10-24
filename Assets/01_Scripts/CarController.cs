using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 10f;
    private float maxY = -1f;
    [SerializeField]private Semaforo semaforo;

    private void Start()
    {
        semaforo = FindObjectOfType<Semaforo>(); // Encuentra el semáforo en la escena
    }

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

        if (semaforo.currentState == TrafficLightState.Green)
        {
            // Mover los autos hacia adelante cuando el semáforo está en verde.
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else
        {
            // Detener los autos cuando el semáforo no está en verde.
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}

using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 10f;
    private float maxX = -30f;
    public Semaforo semaforo;

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
        if (transform.position.x < maxX)
        {
            gameObject.SetActive(false);
        }

        if (semaforo.currentState == TrafficLightState.Red)
        {
            // Detener los autos cuando el semaforo este en rojo
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (semaforo.currentState == TrafficLightState.Green)
        {
            // Mover los autos hacia adelante cuando el semaforo está en verde.
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }
}

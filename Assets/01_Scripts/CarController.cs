using UnityEngine;

public class CarController : MonoBehaviour
{
    private float speed = 10f;
    private float maxX = -30f;
    [SerializeField]private TrafficManager trafficManager;

    private void Start()
    {
        transform.position = new Vector3(0, 40, 0);
        trafficManager = FindObjectOfType<TrafficManager>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Semaforo")){
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (collision.gameObject.tag == "Street")
        {
            gameObject.SetActive(true);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
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

        if (trafficManager.currentState == TrafficLightState.Green)
        {
            // Mover los autos hacia adelante cuando el semaforo está en verde.
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }
}

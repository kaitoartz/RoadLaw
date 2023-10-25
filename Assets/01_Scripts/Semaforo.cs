using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrafficLightState
{
    Red,
    Yellow,
    Green
}

public class Semaforo : MonoBehaviour
{ 
    public delegate void TrafficLightChanged(TrafficLightState newState);
    public static event TrafficLightChanged OnTrafficLightChanged;
    public TrafficLightState currentState;
    public float redTime = 10f;
    public float yellowTime = 3f;
    public float greenTime = 15f;

    void Start()
    {
        StartCoroutine(TrafficLightSequence());
    }

    void Update()
    {
        switch (currentState)
        {
            case TrafficLightState.Red:
                // Lógica para estado rojo 
                break;

            case TrafficLightState.Yellow:
                // Lógica para estado amarillo
                break;

            case TrafficLightState.Green:
                // Lógica para estado verde
                break;
        }
    }

    IEnumerator TrafficLightSequence()
    {
        while (true)
        {
            currentState = TrafficLightState.Red;
            OnTrafficLightChanged?.Invoke(currentState);
            yield return new WaitForSeconds(redTime);

            currentState = TrafficLightState.Green;
            OnTrafficLightChanged?.Invoke(currentState);
            yield return new WaitForSeconds(greenTime);

            currentState = TrafficLightState.Yellow;
            OnTrafficLightChanged?.Invoke(currentState);
            yield return new WaitForSeconds(yellowTime);
        }
     }
}

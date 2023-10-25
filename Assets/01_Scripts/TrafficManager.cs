using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrafficLightState
{
    Red,
    Green
}

public class TrafficManager : MonoBehaviour
{
    public delegate void TrafficLightChanged(TrafficLightState newState);
    public static event TrafficLightChanged OnTrafficLightChanged;
    public TrafficLightState currentState;
    public float redTime = 10f;
    public float greenTime = 15f;
    private bool semaforoSequenceStarted = false;

    void Start()
    {
        currentState = TrafficLightState.Green;
    }

    void Update()
    {
        if (!semaforoSequenceStarted)
        {
            StartTrafficLightSequenceIfActiveSemaforo();
        }
        switch (currentState)
        {
            case TrafficLightState.Red:
                ActivateStopVehicle(true); // Activar Trigger StopVehicle
                break;

            case TrafficLightState.Green:
                ActivateStopVehicle(false); // Desactivar trigger StopVehicle
                break;
        }
    }

    private void StartTrafficLightSequenceIfActiveSemaforo()
    {
        Semaforo[] semaforos = FindObjectsOfType<Semaforo>();
        foreach (Semaforo semaforo in semaforos)
        {
            if (semaforo.isActive)
            {
                StartCoroutine(TrafficLightSequence());
                ActivateStopVehicle(true); // Activa StopVehicle cuando está en rojo.
                semaforoSequenceStarted = true;
                return;
            }
        }
        ActivateStopVehicle(false); // Desactiva StopVeahicle cuando no hay semáforos activos.
    }

    private void ActivateStopVehicle(bool isActive)
    {
        foreach (Semaforo semaforo in FindObjectsOfType<Semaforo>())
        {
            Transform stopVehicles = semaforo.transform.Find("StopVehicles");
            if (stopVehicles != null)
            {
                stopVehicles.gameObject.SetActive(isActive);
            }
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
        }
     }
}

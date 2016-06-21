using UnityEngine;
using System.Collections;

public class equipmentHandler : MonoBehaviour {

	public GameObject player;

    private static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null;
    }

    private void SetVehicleColour(Color colour)
    {
        // Apply colours to each part that is the player's
        GameObject[] vehicleBases = GameObject.FindGameObjectsWithTag("Player Model");
        foreach (GameObject vehicle in vehicleBases)
        {
            if (vehicle.transform.root.tag != player.tag)
                continue;

            // Skip item sockets
            if (FindParentWithTag(vehicle, "ItemSockets") != null)
                return;

            Renderer vehicleRenderer = vehicle.GetComponent<Renderer>();
            if (vehicleRenderer)
                vehicleRenderer.material.color = colour;

            foreach (Transform part in vehicle.transform.GetComponentInChildren<Transform>())
            {
                Renderer partRenderer = part.GetComponent<Renderer>();
                if (partRenderer)
                    partRenderer.material.color = colour;
            }
        }
    }

    // changed to START from AWAKE so socketEqupment script is called first. Not sure if this has broken other things
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        VehicleController playerVehicle = player.GetComponent<VehicleController>();

        if (playerVehicle.player == 1)
        {
            if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            {
                persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

                player.GetComponent<SocketEquipment>().SocketItems(playerData.playerItems, playerData.model);

                player.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

                //Renderer[] playerModel = player.GetComponentsInChildren<Renderer>();
                SetVehicleColour(playerData.playerColor);
            }
        }
        else if (playerVehicle.player == 2)
        {
            if (GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>() != null)
            {
                persistentStats playerData = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

                player.GetComponent<SocketEquipment>().SocketItems(playerData.player2Items, playerData.model);

                //player.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

                SetVehicleColour(playerData.player2Color);
            }
        }
    }
}

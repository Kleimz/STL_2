using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : MonoBehaviour
{
    public CharacterController player1Controller;
    public CharacterController player2Controller;
    public GameObject chainPrefab;
    public float maxDistance = 5f; // Maximum distance the chain can stretch
    public float springForce = 100f; // Adjust this value to control the force applied by the spring

    private GameObject chainInstance;
    private ConfigurableJoint jointPlayer1;
    private ConfigurableJoint jointPlayer2;

    void Start()
    {
        // Instantiate the chain between the players
        chainInstance = Instantiate(chainPrefab, Vector3.zero, Quaternion.identity);

        // Set the chain's position halfway between the players
        chainInstance.transform.position = (player1Controller.transform.position + player2Controller.transform.position) / 2f;

        // Attach the chain to the players using ConfigurableJoint
        jointPlayer1 = player1Controller.gameObject.AddComponent<ConfigurableJoint>();
        jointPlayer1.connectedBody = player2Controller.gameObject.GetComponent<Rigidbody>();
        jointPlayer1.autoConfigureConnectedAnchor = false;
        jointPlayer1.connectedAnchor = Vector3.zero;

        jointPlayer2 = player2Controller.gameObject.AddComponent<ConfigurableJoint>();
        jointPlayer2.connectedBody = player1Controller.gameObject.GetComponent<Rigidbody>();
        jointPlayer2.autoConfigureConnectedAnchor = false;
        jointPlayer2.connectedAnchor = Vector3.zero;

        // Configure the spring settings to limit the maximum distance
        SpringJoint springJoint = player1Controller.gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = player2Controller.gameObject.GetComponent<Rigidbody>();
        springJoint.spring = springForce;
        springJoint.damper = 0f; // Optional: Adjust this value to control damping
        springJoint.minDistance = maxDistance;
        springJoint.maxDistance = maxDistance;
    }

    void Update()
    {
        // Update the chain's position to always stay halfway between the players
        chainInstance.transform.position = (player1Controller.transform.position + player2Controller.transform.position) / 2f;
    }
}

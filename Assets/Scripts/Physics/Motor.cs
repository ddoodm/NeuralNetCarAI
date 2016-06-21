using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour
{
    /// <summary>
    /// Speed: amount of acceleration over time
    /// angularSpeed: Amount of angular (turning) acceleration over time
    /// </summary>
    public float
        speed = 100.0f,
        angularSpeed = 5.0f;

    /// <summary>
    /// Properties for vehicle suspension.
    /// These properties are used in a dampened spring function.
    /// </summary>
    public float
        suspensionForce = 65.0f,
        suspensionHeight = 3.5f;

    /// <summary>
    /// Rays that are cast below the vehicle
    /// </summary>
    public Transform[] edgeRaysPositions;

    /// <summary>
    /// Input (user) forces
    /// </summary>
    private float
        powerInput, turnInput;

    /// <summary>
    /// Component references are deprecated; we need to obtain our own here.
    /// </summary>
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // W / S = acceleration, A / D = angular
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        handleSuspension();
        handlePowerInput();
    }

    private void handleSuspension()
    {
        Vector3
            linearForce = Vector3.zero,
            targetNormal = Vector3.zero;

        int groundedEdges = 0;

        foreach (Transform rayPos in edgeRaysPositions)
        {
            // Cast a ray below the vehicle
            Ray floorRay = new Ray(rayPos.position, this.transform.up * -1.0f);
            RaycastHit hit;

            if (Physics.Raycast(floorRay, out hit, suspensionHeight))
            {
                // We're under the spring equilibrium; apply an opposing force
                float dy = (suspensionHeight - hit.distance) / suspensionHeight;
                Vector3 springForce = hit.normal * suspensionForce * dy;

                linearForce += springForce;
                targetNormal += hit.normal;

                //rigidbody.AddForce(springForce / (float)edgeRaysPositions.Length);

                // Tend toward 0 rotation
                //Quaternion targetRotation = Quaternion.LookRotation(transform.forward, hit.normal);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);

                groundedEdges++;
            }
        }

        if (groundedEdges > 0)
        {
            linearForce /= (float)groundedEdges;
            rigidbody.AddForce(linearForce);
        }

        targetNormal = targetNormal.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetNormal);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
        //transform.rotation = targetRotation;
    }

    private void handlePowerInput()
    {
        // addRelativeForce works as if we multiplied force by transform.forward
        rigidbody.AddRelativeForce(0.0f, 0.0f, powerInput * speed);

        // Rotate only when we have velocity
        turnInput *= Vector3.Scale(rigidbody.velocity, transform.forward).magnitude;
        rigidbody.AddRelativeTorque(0.0f, turnInput * angularSpeed, 0.0f);
    }
}

using UnityEngine;
using System.Collections;

public class CameraVehicleTrack : MonoBehaviour
{
    public Transform target;
    public float
        butter = 0.1f,
        velocityContribution = 0.2f;

    private Rigidbody targetBody;
    private Vector3 offset;
    private Vector3 offsetRot;

	void Start ()
    {
        offset = target.position - transform.position;
        offsetRot = transform.rotation.eulerAngles;
        targetBody = target.gameObject.GetComponent<Rigidbody>();

        offset.y *= -1.0f;
    }
	
	void FixedUpdate ()
    {
        transform.rotation = Quaternion.identity;

        Vector3 t = target.position;
        if (targetBody)
            t -= targetBody.velocity * velocityContribution;

        Vector3 dampPlayerPos = (transform.position - t) * butter * Time.deltaTime;

        transform.Translate(-dampPlayerPos);
        transform.Rotate(target.up, target.rotation.eulerAngles.y);
        transform.Translate(offset * butter * Time.deltaTime);
        transform.LookAt(target.position);

        transform.Rotate(-Vector3.Scale(offsetRot, Vector3.one - Vector3.up));

        transform.position = calcAvoidancePosition(transform.position);
        transform.position = avoidFloor(transform.position);
    }

    /// <summary>
    /// Avoids static objects.
    /// TODO: Make this better by fixing the camera sticking issue
    /// </summary>
    Vector3 calcAvoidancePosition(Vector3 inPos)
    {
        Ray r = new Ray(target.position, (transform.position - target.position).normalized);

        //Vector3 colEpsilon = this.transform.forward;
        float camDistance = (target.position - this.transform.position).magnitude;

        // Exclude all but scene geometry from raycast
        LayerMask sceneLayer = LayerMask.GetMask(new string[] { "SceneGeometry" });

        RaycastHit hit;
        if (Physics.Raycast(r, out hit, camDistance, sceneLayer))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = 0;
            return hitPoint;
        }
        return inPos;
    }

    /// <summary>
    /// Avoids terrain / static floor by casting a ray down, and obtaining the distance
    /// </summary>
    Vector3 avoidFloor(Vector3 inPos)
    {
        RaycastHit hit;
        float terrainDistance = 0;
        Ray r = new Ray(inPos, Vector3.down);
        LayerMask sceneLayer = LayerMask.GetMask(new string[] { "SceneGeometry" });
        if (Physics.Raycast(r, out hit, 100.0f, sceneLayer))
            terrainDistance = hit.distance;

        Debug.DrawLine(r.origin, hit.point, Color.yellow);

        // Move the camera to the floor hit position, and offset
        return new Vector3(
            inPos.x,
            inPos.y - terrainDistance + offset.y,
            inPos.z);
    }
}

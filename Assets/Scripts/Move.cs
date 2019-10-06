using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [Header("Parameters")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float stopDistance = 0.0f;

    private float turnAngle = 0.0f;

    private RaycastHit hit;
    private NavMeshPath path;

    private NavMeshAgent agent;

    void Start()
    {
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
    }

    public Transform target;

    private void Update()
    {
        MoveTo(target.position);
    }

    public void MoveTo(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);

        if (agent.CalculatePath(position, path) && distance > stopDistance)
        {
            Look();

            agent.Move(transform.forward * Time.deltaTime * moveSpeed);
        }
    }
    
    private void Look()
    {
        Vector3 direction = path.corners[1] - transform.position;

        float value = Vector3.Angle(transform.forward, direction);

        turnAngle = Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, turnAngle, 0);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * GetRotateSpeed());
    }

    /// <summary>
    /// Возвращает скорость поворота.
    /// </summary>
    /// <returns></returns>
    private float GetRotateSpeed()
    {
        float rotateSpeedModifier = 0;
        float maxDistance = 5.0f;

        Vector3 dir1 = Quaternion.Euler(0, 15, 0) * transform.forward;
        Vector3 dir2 = Quaternion.Euler(0, -15, 0) * transform.forward;

        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), dir1 * maxDistance, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), dir2 * maxDistance, Color.red);

        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir1, out hit, maxDistance))
        {
            rotateSpeedModifier += maxDistance / hit.distance;
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir2, out hit, maxDistance))
        {
            rotateSpeedModifier += maxDistance / hit.distance;
        }

        return rotationSpeed + rotateSpeedModifier;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if(path != null)
        {
            foreach (Vector3 p in path.corners)
            {
                Gizmos.DrawSphere(p, 0.5f);
            }
        }
    }
}

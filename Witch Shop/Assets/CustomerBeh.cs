using UnityEngine;
using System.Collections;

public class CustomerBeh : MonoBehaviour
{
	public Transform[] waypoints; // Array of waypoints
	public float speed = 5f; // The speed at which the GameObject moves
	public float delayBeforeNewCycle = 5f; // Delay before starting a new cycle

	private bool hasReachedTarget = false; // Flag to check if the current target has been reached
	private int currentWaypointIndex = 0; // Index of the current waypoint in the array
	private bool isReversed = false; // Flag to indicate if the waypoints traversal direction is reversed

	void Start()
	{
		if (waypoints.Length > 0)
		{
			MoveToNextWaypoint();
		}
		else
		{
			Debug.LogWarning("No waypoints assigned.");
		}
	}

	void Update()
	{
		if (!hasReachedTarget)
		{
			// Calculate the direction to the current waypoint
			Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

			// Check if the current waypoint has been reached
			if (direction.magnitude <= 0.1f)
			{
				hasReachedTarget = true;
				StartCoroutine(DelayBeforeNextWaypoint());
			}
			else
			{
				// Move the GameObject towards the current waypoint
				transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World); // Move in world space
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if the trigger zone has been entered
		if (other.CompareTag("Waypoint"))
		{
			// Move to the next waypoint if there is one
			if (!isReversed)
			{
				if (currentWaypointIndex < waypoints.Length - 1)
				{
					currentWaypointIndex++;
				}
				else
				{
					// Reverse the traversal direction
					isReversed = true;
					currentWaypointIndex = waypoints.Length - 2; // Start from the second-to-last waypoint
				}
			}
			else
			{
				// Move to the previous waypoint if there is one
				if (currentWaypointIndex > 0)
				{
					currentWaypointIndex--;
				}
				else
				{
					// Reset to the original traversal direction
					isReversed = false;
					currentWaypointIndex = 1; // Start from the second waypoint
				}
			}
			MoveToNextWaypoint();
		}
	}

	IEnumerator DelayBeforeNextWaypoint()
	{
		yield return new WaitForSeconds(delayBeforeNewCycle);

		if (!isReversed)
		{
			// Move to the next waypoint if there is one
			if (currentWaypointIndex < waypoints.Length - 1)
			{
				currentWaypointIndex++;
			}
			else
			{
				// Reverse the traversal direction
				isReversed = true;
				currentWaypointIndex = waypoints.Length - 2; // Start from the second-to-last waypoint
			}
		}
		else
		{
			// Move to the previous waypoint if there is one
			if (currentWaypointIndex > 0)
			{
				currentWaypointIndex--;
			}
			else
			{
				// Reset to the original traversal direction
				isReversed = false;
				currentWaypointIndex = 1; // Start from the second waypoint
			}
		}

		MoveToNextWaypoint();
	}

	void MoveToNextWaypoint()
	{
		// Reset flag for reaching target
		hasReachedTarget = false;

		// Look at the next waypoint
		if (waypoints.Length > 0 && currentWaypointIndex < waypoints.Length)
		{
			Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}
}
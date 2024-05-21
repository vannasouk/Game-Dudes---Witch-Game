using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
	public float spawnInterval = 10f; // Interval between customer spawns in seconds


	public GameObject[] possibleRequestedItems; 

	void Start()
	{
		StartCoroutine(SpawnCustomers());
	}

	IEnumerator SpawnCustomers()
	{
		while (true) // Loop forever
		{
			yield return new WaitForSeconds(spawnInterval); // Wait for spawnInterval seconds

			// Call a function to spawn a customer
			SpawnCustomer();

			// Optionally, you can add more actions here after spawning a customer
		}
	}

	void SpawnCustomer()
	{
		// Here you can put the code to instantiate a customer GameObject or do whatever is needed
		Debug.Log("Spawn customer"); // Print a message to the console
	}
}

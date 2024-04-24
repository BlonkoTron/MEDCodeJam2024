using UnityEngine;

public class SpawnOnSpace : MonoBehaviour
{
  public GameObject prefabToSpawn; // Assign your prefab in the inspector

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      // Spawn the prefab at the current game object's position and rotation
      Instantiate(prefabToSpawn, transform.position, transform.rotation);
    }
  }
}

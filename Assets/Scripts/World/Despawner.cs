using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject.transform.parent.gameObject);
    }
}

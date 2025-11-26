using UnityEngine;

public class VFXTrigger : MonoBehaviour
{
    public GameObject vfxPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(vfxPrefab, collision.contacts[0].point, Quaternion.identity);
        }
    }
}

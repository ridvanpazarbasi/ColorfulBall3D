
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision hit)
    {
       Destroy(hit.gameObject);
    }
}

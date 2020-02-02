using Assets.Scripts;
using Assets.Scripts.Controllers;
using UnityEngine;

public class GetDash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Dash>().enabled = true;
            Destroy(gameObject);
            other.GetComponent<Dash>().DashStart();
        }

    }
}

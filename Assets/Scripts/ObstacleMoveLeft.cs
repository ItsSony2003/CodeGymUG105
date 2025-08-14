using UnityEngine;

public class ObstacleMoveLeft : MonoBehaviour
{
    public ObstacleConfig config;

    void Update()
    {
        transform.Translate(transform.forward * config.speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }


}

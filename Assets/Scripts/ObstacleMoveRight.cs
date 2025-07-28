using UnityEngine;

public class ObstacleMoveRight : MonoBehaviour
{
    public ObstacleConfig config;

    void Update()
    {
        transform.Translate(Vector3.right * config.speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class DisableOnExit : MonoBehaviour
{
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log($"{gameObject.name} exited trigger with {other.name}");

    //    if (other.CompareTag("DisableObstacle"))
    //    {
    //        Debug.Log($"Disabling {gameObject.name}");
    //        gameObject.SetActive(false);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{gameObject.name} exited trigger with {other.name}");

        if (other.CompareTag("Ground"))
        {
            //Debug.Log($"Disabling {gameObject.name}");
            gameObject.SetActive(false);
        }
    }


}

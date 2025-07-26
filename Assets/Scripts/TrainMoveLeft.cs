using UnityEngine;

public class TrainMoveLeft : VehicleMoveLeft
{
    public float speedMultiplier = 4f; 

    void Update()
    {
        transform.Translate(Vector3.left * speed * speedMultiplier * Time.deltaTime);
    }
}

using UnityEngine;

public class TrainMoveLeft : VehicleMoveLeft
{
    public float speedMultiplier = 4f; // tốc độ tàu nhanh gấp đôi

    void Update()
    {
        transform.Translate(Vector3.left * speed * speedMultiplier * Time.deltaTime);
    }
}

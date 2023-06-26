using UnityEngine;

namespace RunningFishes.Pong.Ball
{
    public class BallMovementController : MonoBehaviour
    {
        private void Start()
        {
            // add force to the ball in left or right direction (degree with horizontal axis less than 60 degree)
            float randomDegree = Random.Range(-60, 60);
            float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomForce = Random.Range(10, 15);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(randomDirection * randomForce * Mathf.Cos(randomDegree * Mathf.Deg2Rad), randomForce * Mathf.Sin(randomDegree * Mathf.Deg2Rad)), ForceMode2D.Impulse);
        }
    }
}
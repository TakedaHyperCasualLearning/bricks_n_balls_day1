using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private List<Ball> ballPool = new List<Ball>();

    private int BALL_POOL_SIZE = 100;

    public void GenerateBall()
    {
        for (int i = 0; i < BALL_POOL_SIZE; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.SetActive(false);
            ballPool.Add(ball.GetComponent<Ball>());
            Debug.Log(ball.gameObject.activeSelf);
        }
    }

    public Ball GetBall()
    {
        foreach (Ball ball in ballPool)
        {
            if (!ball.gameObject.activeSelf)
            {
                ball.gameObject.SetActive(true);
                return ball;
            }
        }
        return null;
    }

    public void ReturnBall(GameObject ball)
    {
        ball.SetActive(false);
    }

    public List<Ball> GetBallList()
    {
        return ballPool;
    }
}

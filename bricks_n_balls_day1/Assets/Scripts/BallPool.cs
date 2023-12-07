using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private List<Ball> ballPool = new List<Ball>();

    private int BALL_POOL_SIZE = 50;

    private bool isFirstStop = false;
    private Vector3 firstStopPosition = Vector3.zero;

    public void GenerateBall()
    {
        for (int i = 0; i < BALL_POOL_SIZE; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ballPool.Add(ball.GetComponent<Ball>());
        }
    }

    public Ball GetBall()
    {
        foreach (Ball ball in ballPool)
        {
            if (!ball.gameObject.activeSelf)
            {
                // ball.gameObject.SetActive(true);
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

    public bool CheckIsStop()
    {
        bool isStop = true;

        for (int i = 0; i < ballPool.Count; i++)
        {
            if (ballPool[i].GetIsMove()) { isStop = false; continue; }

            if (!isFirstStop)
            {
                isFirstStop = true;
                firstStopPosition = ballPool[i].transform.position;
                Debug.Log("firstStopPosition: " + firstStopPosition);
            }
        }

        return isStop;
    }
}

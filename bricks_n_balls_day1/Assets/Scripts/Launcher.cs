using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject predictionMarkerPrefab;
    [SerializeField] private GameObject dotPointPrefab;
    [SerializeField] private BallPool ballPool;

    private Vector3 mouseToMakerDirection = Vector3.zero;
    private float DOTTEDLINE_DISTANCE = 0.2f;
    private GameObject predictionMarker;
    private List<GameObject> dotPoints = new List<GameObject>();
    private float BOUND_LENGTH = 3.0f;
    private bool isShot = false;
    private float shotInterval = 0.0f;
    private float SHOT_INTERVAL_MAX = 0.05f;
    private int shotCount = 0;

    void Start()
    {
        predictionMarker = Instantiate(predictionMarkerPrefab, transform.position, Quaternion.identity);
        predictionMarker.transform.parent = transform;
        predictionMarker.SetActive(false);

        ballPool.transform.position = transform.position;
        ballPool.GenerateBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShot)
        {
            shotInterval += Time.deltaTime;
            if (shotInterval > SHOT_INTERVAL_MAX)
            {
                if (ballPool.GetBallList().Count > shotCount)
                {
                    var ball = ballPool.GetBallList()[shotCount];
                    ball.SetVelocity(mouseToMakerDirection);
                    shotCount++;
                    shotInterval = 0.0f;
                }
            }

            if (shotCount >= ballPool.GetBallList().Count)
            {
                if (ballPool.CheckIsStop())
                {
                    isShot = false;
                    shotCount = 0;
                    shotInterval = 0.0f;
                }
            }
            return;
        }

        if (Input.GetMouseButton(0))
        {
            // マウスポジションの取得と、座標の変換
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x = (float)(mousePosition.x - Screen.width * 0.5);
            mousePosition.y = (float)(mousePosition.y - Screen.height * 0.5);

            // マウスとの方向ベクトルの取得
            mouseToMakerDirection = transform.position - mousePosition;
            mouseToMakerDirection.Normalize();

            // レイキャストの実行
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, mouseToMakerDirection);
            Debug.DrawRay(transform.position, mouseToMakerDirection, Color.red, 1, false);

            if (raycast.collider == null || raycast.collider.tag != "Wall") return;

            DrawDottedLine(raycast, mouseToMakerDirection);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Shot(mouseToMakerDirection);
            dotPoints.ForEach(dotPoint => dotPoint.SetActive(false));
            predictionMarkerPrefab.SetActive(false);
            predictionMarker.SetActive(false);
        }
        else
        {
            dotPoints.ForEach(dotPoint => dotPoint.SetActive(false));
            predictionMarkerPrefab.SetActive(false);
            predictionMarker.SetActive(false);
        }
    }


    private void DrawDottedLine(RaycastHit2D raycast, Vector3 direction)
    {
        // 当たった地点にマーカーを表示
        predictionMarker.SetActive(true);
        predictionMarker.transform.position = raycast.point;

        // 点線を表示するための処理
        float lineLength = Vector3.Distance(transform.position, raycast.point);
        int dottedLineCount = (int)(lineLength / DOTTEDLINE_DISTANCE);

        for (int i = 0; i < dottedLineCount; i++)
        {
            Vector3 dottedLinePosition = transform.position + direction * DOTTEDLINE_DISTANCE * i;

            if (dotPoints.Count <= i)
            {
                GameObject gameObject = Instantiate(dotPointPrefab, dottedLinePosition, Quaternion.identity);
                gameObject.transform.parent = transform;
                dotPoints.Add(gameObject);
            }
            else
            {
                dotPoints[i].SetActive(true);
                dotPoints[i].transform.position = dottedLinePosition;
            }
        }
        if (dotPoints.Count > dottedLineCount)
        {
            for (int i = dotPoints.Count - 1; i >= dottedLineCount; i--)
            {
                dotPoints[i].SetActive(false);
            }
        }

        // 跳ね返った後も少し描画する
        Vector3 boundDirection = Vector3.Reflect(direction, raycast.normal).normalized;
        int boundDottedLineCount = (int)(BOUND_LENGTH / DOTTEDLINE_DISTANCE);
        // Debug.Log(boundDottedLineCount);

        for (int i = 0; i < boundDottedLineCount; i++)
        {
            Vector3 dottedLinePosition = raycast.point + new Vector2(boundDirection.x, boundDirection.y) * (float)DOTTEDLINE_DISTANCE * i;
            if (dotPoints.Count <= dottedLineCount + i)
            {
                GameObject gameObject = Instantiate(dotPointPrefab, dottedLinePosition, Quaternion.identity);
                gameObject.transform.parent = transform;
                dotPoints.Add(gameObject);
            }
            else
            {
                dotPoints[dottedLineCount + i].SetActive(true);
                dotPoints[dottedLineCount + i].transform.position = dottedLinePosition;
            }
        }
        if (dotPoints.Count > dottedLineCount + boundDottedLineCount)
        {
            for (int i = dotPoints.Count - 1; i >= dottedLineCount + boundDottedLineCount; i--)
            {
                dotPoints[i].SetActive(false);
            }
        }
    }


    private void Shot(Vector3 direction)
    {
        isShot = true;
        ballPool.GetBallList().ForEach(ball => ball.SetIsMove(true));
    }
}

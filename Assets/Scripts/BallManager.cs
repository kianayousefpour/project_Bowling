using UnityEngine;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> BallList = new List<GameObject>();
    [SerializeField] private List<GameObject> BallIconList = new List<GameObject>();
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject BallIconPrefab;
    [SerializeField] private Vector3 BallIconPosition;

    private int BallIndex = 0;

    private void Start()
    {
        CreateBalls();
        CreateBallIcons();
    }

    private void CreateBalls()
    {
        int BallCount = 3;
        for (int i = 0; i < BallCount; i++)
        {
            GameObject newBall = Instantiate(BallPrefab, transform.position, Quaternion.identity);
            newBall.SetActive(false);
            BallList.Add(newBall);
        }
        BallList[0].SetActive(true); // فعال کردن توپ اول
    }

    private void CreateBallIcons()
    {
        int BallCount = 3;
        for (int i = 0; i < BallCount; i++)
        {
            GameObject newBallIcon = Instantiate(BallIconPrefab, BallIconPosition, Quaternion.identity);
            BallIconPosition.x += 1f; // موقعیت آیکن بعدی
            BallIconList.Add(newBallIcon);
        }
    }

    public void SetActiveBall()
    {
        if (BallIndex < BallList.Count - 1)
        {
            // غیرفعال کردن توپ قبلی
            BallList[BallIndex].SetActive(false);

            BallIndex++; // افزایش ایندکس توپ
            BallList[BallIndex].SetActive(true);

            // حذف یکی از آیکن‌های توپ
            if (BallIconList.Count > 0)
            {
                Destroy(BallIconList[0]);
                BallIconList.RemoveAt(0);
            }
        }
    }
}

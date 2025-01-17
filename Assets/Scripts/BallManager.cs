using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> BallList = new List<GameObject>();
    [SerializeField] private List<GameObject> BallIconList = new List<GameObject>();
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject BallIconPrefab;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Vector3 BallIconPostion;

    private int BallIndex = 0;

    private void Start(){
        CreateBalls();  
        CreatBallIcons();  
    }

    private void CreateBalls(){
        int BallCount=3;
        for(int i=0; i<BallCount; i++){
            GameObject newBall = Instantiate(BallPrefab, transform.position, Quaternion.identity);
            newBall.SetActive(false);
            BallList.Add(newBall);
        }
        BallList[0].SetActive(true);
    }

        private void CreatBallIcons(){
        int BallCount=3;
            for(int i = 0; i < BallCount; i++){
                GameObject newBallIcon = Instantiate(BallIconPrefab, BallIconPostion, Quaternion.identity);
                newBallIcon.GetComponent<SpriteRenderer>().color = activeColor;
                BallIconPostion.x += 1f;
                BallIconList.Add(newBallIcon);
        }
    }
    
    public void SetActiveBall(){
        int BallCount=3;
        if(BallIndex < BallCount - 1){
            BallIndex++;
            BallList[BallIndex].SetActive(true);
        }
    }
}

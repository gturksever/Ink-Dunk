using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBall : MonoBehaviour
{

    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject BallThrowingArea;
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject[] BucketPoints;
    int ActiveBallIndex;
    int RandomBucketPointIndex;
    bool  Lock;

    public static int howManyBallsThrown;
    public static int numberOfBallShots;

    private void Start()
    {
        numberOfBallShots = 0;
        howManyBallsThrown = 0;
    }
    public void GameStart()
    {
        StartCoroutine(BallThrowingSystem());
    }

    IEnumerator BallThrowingSystem()
    {
        while (true)
        {
            if (!Lock)
            {
                yield return new WaitForSeconds(.5f);
                
                if(GameManager.BasketScore > 20 && numberOfBallShots % 6 == 0)
                {
                    doubleballthrower();
                }

                else if (GameManager.BasketScore > 50 && numberOfBallShots % 4 == 0)
                {
                    doubleballthrower();
                }

                else if (GameManager.BasketScore > 100)
                {
                    doubleballthrower();
                }
                else
                {
                    
                    Balls[ActiveBallIndex].transform.position = BallThrowingArea.transform.position;
                    Balls[ActiveBallIndex].SetActive(true);
                    float angle = Random.Range(70f, 100f);
                    int Power = Random.Range(800, 1150);
                    Vector3 Pos = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                    Balls[ActiveBallIndex].GetComponent<Rigidbody2D>().AddForce(Pos * Power);
                    
                    if (ActiveBallIndex != Balls.Length - 1)
                        ActiveBallIndex++;
                    else
                        ActiveBallIndex = 0;
                    
                    howManyBallsThrown = 1;
                    numberOfBallShots++;
                }


                yield return new WaitForSeconds(0.6f);
                RandomBucketPointIndex = Random.Range(0, BucketPoints.Length - 1);
                Bucket.transform.position = BucketPoints[RandomBucketPointIndex].transform.position;
                Bucket.SetActive(true);
                Lock = true;
                Invoke("CheckTheBall", 4f);
            }
            else
            {
                yield return null;
            }    
        }
    }

    public void Continue()
    {
        if (howManyBallsThrown == 1)
        {
            Lock = false;
            Bucket.SetActive(false);
            CancelInvoke("CheckTheBall");
            howManyBallsThrown--;
        }
        else
        {
            howManyBallsThrown--;
        }
        
    }

    void CheckTheBall()
    {
        if (Lock)
        {
            GetComponent<GameManager>().GameOver();
        }
    }

    void doubleballthrower()
    {
        for (int i = 0; i < 2; i++)
        {
            Balls[ActiveBallIndex].transform.position = BallThrowingArea.transform.position;
            Balls[ActiveBallIndex].SetActive(true);
            float angle = Random.Range(70f, 100f);
            int Power = Random.Range(800, 1150);
            Vector3 Pos = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            Balls[ActiveBallIndex].GetComponent<Rigidbody2D>().AddForce(Pos * Power);

            if (ActiveBallIndex != Balls.Length - 1)
                ActiveBallIndex++;
            else
                ActiveBallIndex = 0;
        }


        howManyBallsThrown = 2;
        numberOfBallShots++;
    }
}

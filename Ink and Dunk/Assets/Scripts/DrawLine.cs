using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    public GameObject LinePrefab;
    public GameObject Line;

    public LineRenderer linerenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> FingerPositionList;
    public List<GameObject> Lines;
    bool gameStart;
    int remainingDraw=0;


    private void Start()
    {
        remainingDraw = 0;
    }

    void Update()
    {

        if (remainingDraw != 2)
        {
            if (Input.GetMouseButtonDown(0) && _GameManager.hasGameStart == true)
            {
                CreateLine();


            }
            if (Input.GetMouseButton(0) && _GameManager.hasGameStart == true)
            {
                Vector2 FingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if ((Vector2.Distance(FingerPosition, FingerPositionList[^1]) > .1f) && _GameManager.hasGameStart == true)
                {

                    UpdateLine(FingerPosition);
                }
            }
        }

        if(Lines.Count !=0  && remainingDraw != 2)
        {
            if (Input.GetMouseButtonUp(0))
            {
                
                _GameManager.RemainingDrawList[remainingDraw].SetActive(false);
                remainingDraw++;
            }
        }
        
    }

    void CreateLine()
    {
        Line = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);
        Lines.Add(Line);
        linerenderer = Line.GetComponent<LineRenderer>();
        edgeCollider = Line.GetComponent<EdgeCollider2D>();
        FingerPositionList.Clear();
        FingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        linerenderer.SetPosition(0, FingerPositionList[0]);
        linerenderer.SetPosition(1, FingerPositionList[1]);
        edgeCollider.points = FingerPositionList.ToArray();

    }

    void UpdateLine(Vector2 GetFingerPosition)
    {
       
        FingerPositionList.Add(GetFingerPosition);
        linerenderer.positionCount++;
        linerenderer.SetPosition(linerenderer.positionCount - 1,GetFingerPosition);
        edgeCollider.points = FingerPositionList.ToArray();
        
    }

    public void Continue()
    {
        if (ThrowingBall.howManyBallsThrown == 0)
        {
            foreach (var item in Lines)
            {

                Destroy(item.gameObject);

            }
            Lines.Clear();
            remainingDraw = 0;
            _GameManager.RemainingDrawList[0].SetActive(true);
            _GameManager.RemainingDrawList[1].SetActive(true);
        }
        
        




    }
}

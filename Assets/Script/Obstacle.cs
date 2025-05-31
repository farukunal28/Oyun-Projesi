using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    Transform characterControl;
    BoxCollider2D col;


    [SerializeField] private LayerMask wallMask;


    private Transform upFlankPoint, downFlankPoint, rightFlankPoint, leftFlankPoint;
    void Start()
    {
        characterControl = GameObject.Find("Karakter").transform;
        col = GetComponent<BoxCollider2D>();
        SetFlankPoints();
    }
    private void SetFlankPoints()
    {
        Vector3 pos = transform.position;
        Vector3 offset = col.offset;
        float horizontalDistance = (transform.localScale.x + col.size.x + 1) / 2;
        float verticalDistance   = (transform.localScale.y + col.size.y + 1) / 2;

           upFlankPoint = new GameObject().transform;
         downFlankPoint = new GameObject().transform;
        rightFlankPoint = new GameObject().transform;
         leftFlankPoint = new GameObject().transform;


           upFlankPoint.position = pos + offset + Vector3.up * verticalDistance;
         downFlankPoint.position = pos + offset + Vector3.down * verticalDistance;
        rightFlankPoint.position = pos + offset + Vector3.right * horizontalDistance;
         leftFlankPoint.position = pos + offset + Vector3.left * horizontalDistance;

        SetObjectPositions();
    }
    private void SetObjectPositions()
    {
        upFlankPoint.transform.position = upFlankPoint.position;
        downFlankPoint.transform.position = downFlankPoint.position;
        rightFlankPoint.transform.position = rightFlankPoint.position;
        leftFlankPoint.transform.position = leftFlankPoint.position;
    }



    public List<Transform> FindSeeingFlankPoints()
    {
        List <Transform> list = new List<Transform>();

        if (IsFlankSeesThePlayer(upFlankPoint.position))
            list.Add(upFlankPoint);

        if (IsFlankSeesThePlayer(downFlankPoint.position))
            list.Add(downFlankPoint);

        if (IsFlankSeesThePlayer(rightFlankPoint.position))
            list.Add(rightFlankPoint);
        
        if (IsFlankSeesThePlayer(leftFlankPoint.position))
            list.Add(leftFlankPoint);
        
        return list;
    }
    bool IsFlankSeesThePlayer(Vector3 point)
    {
        Vector3 end = characterControl.position;
        Vector3 direction = (end - point).normalized;
        float distance = Vector3.Distance(point, end);

        return !Physics2D.Raycast(point, direction, distance, wallMask);
    }
}

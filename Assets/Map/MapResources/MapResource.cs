using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResource : MonoBehaviour
{
    Vector3 centerPos;
    /// <summary>
    /// ������������� ������� ������� ������ �����. x � z - ������������ ����� �� -1 �� 1
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void SetInnerPosition(float x, float z)
    {
        Vector3 pos = transform.position;
        pos.x += x * HexMetrics.innerRadius * HexMetrics.solidFactor;
        pos.z += z * HexMetrics.innerRadius * HexMetrics.solidFactor;
        transform.position = pos;
    }
    private void Awake()
    {
        centerPos = transform.position;
    }
}

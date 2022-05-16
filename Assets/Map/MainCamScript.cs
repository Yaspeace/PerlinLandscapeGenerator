using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamScript : MonoBehaviour
{
    [SerializeField]
    public float delta = 0.5f;

    public float rotationDelta = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 newPos = pos;
        var camTransform = gameObject.transform;

        if (Input.GetKey(KeyCode.W))
            newPos = camTransform.position + camTransform.forward * delta;
        if (Input.GetKey(KeyCode.A))
            newPos = camTransform.position - camTransform.right * delta;
        if (Input.GetKey(KeyCode.S))
            newPos = camTransform.position - camTransform.forward * delta;
        if (Input.GetKey(KeyCode.D))
            newPos = camTransform.position + camTransform.right * delta;

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(rotationDelta, 0, 0, Space.Self);
        if(Input.GetKey(KeyCode.E))
            transform.Rotate(-rotationDelta, 0, 0, Space.Self);
        /*if (Input.mouseScrollDelta.y > 0)
            transform.Rotate(0, rotationDelta, 0, Space.Self);
        if (Input.mouseScrollDelta.y < 0)
            transform.Rotate(0, -rotationDelta, 0, Space.Self);*/
        transform.Rotate(0, Input.mouseScrollDelta.y * 5f, 0, Space.Self);
        if (Input.GetKey(KeyCode.Space))
            newPos = camTransform.position + camTransform.up * delta;
        if (Input.GetKey(KeyCode.LeftControl))
            newPos = camTransform.position - camTransform.up * delta;

        gameObject.transform.position = newPos;
    }
}

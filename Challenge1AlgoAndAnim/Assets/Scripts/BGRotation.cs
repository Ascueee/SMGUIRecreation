using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGRotation : MonoBehaviour
{
    [SerializeField] float backGroundRotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, backGroundRotationSpeed * Time.deltaTime, Space.Self);
    }
}

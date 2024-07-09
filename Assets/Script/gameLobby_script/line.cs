using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public LineRenderer posit;
    public Transform post1;
    public Transform post2;
    // Start is called before the first frame update
    void Start()
    {
        posit.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        posit.SetPosition(0, post1.position);
        posit.SetPosition(1, post2.position);
    }
}

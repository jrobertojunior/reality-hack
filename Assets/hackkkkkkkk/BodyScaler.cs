using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScaler : MonoBehaviour
{
    public GameObject body;

    public float scale = 150;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.transform.localScale = new Vector3(scale, scale, scale);   
    }
}

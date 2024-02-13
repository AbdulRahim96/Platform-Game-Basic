using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void teleport()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}

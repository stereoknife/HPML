using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScheduler : MonoBehaviour
{
    [SerializeField] private MeshFilter[] meshes;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}

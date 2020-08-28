using System.Collections;
using System.Collections.Generic;
using StarterKit.Base;
using UnityEngine;

public class CameraController : BaseObject
{
    public Transform ObjectToAttachCamera;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness;
    private Vector3 _destinationPos;

    public override void BaseObjectStart()
    {
        if (smoothness <= 0)
        {
            smoothness = 0.12f;
        }
    }
    
    public override void BaseObjectLateUpdate()
    {
        
        _destinationPos = new Vector3(ObjectToAttachCamera.position.x + offset.x,transform.position.y,transform.position.z) ;
        transform.position = Vector3.Lerp(transform.position, _destinationPos, Time.fixedDeltaTime);
        
    }  
    

  
   
}

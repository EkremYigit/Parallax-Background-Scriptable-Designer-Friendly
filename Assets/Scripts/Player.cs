using System.Collections;
using System.Collections.Generic;
using StarterKit.Base;
using UnityEngine;

public class Player : BaseObject
{

    [SerializeField] private float jumpRange;
    private Rigidbody rb;
    public override void BaseObjectAwake()
    {
        Debug.Log("PLAYER AWAKE!");
        transform.hasChanged = false;
    }
    public override void BaseObjectStart()
    {
        rb = GetComponent<Rigidbody>();
        GameManager.instance.MoveFlag = true;
    }
    
    public override void BaseObjectUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!GameManager.instance.MoveFlag)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.hasChanged = true;
            transform.position += transform.right * Time.fixedDeltaTime;
            GameManager.instance.MoveFlag=true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (GameManager.instance.MoveFlag)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            transform.hasChanged = true;
            transform.position += transform.right * Time.fixedDeltaTime;
            GameManager.instance.MoveFlag=false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //jump
            rb.AddForce(new Vector3(0,jumpRange,0));
            StartCoroutine("StartJumping");


        }

      
      
        
        

    
        
        
    }
    IEnumerator StartJumping()
    {
        GameManager.instance.isJumping = true;
        GameManager.instance.isFalling = false;
        yield return  new WaitForSeconds(0.5f);
        yield return StartCoroutine(StopJumping());
        
    }


    IEnumerator StopJumping()
    {
        GameManager.instance.isJumping = false;
        GameManager.instance.isFalling = true;
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.isFalling = false;
    }

    public override void BaseObjectDestroy()
    {
        Debug.Log("PLAYER DESTROY!");
    }
}

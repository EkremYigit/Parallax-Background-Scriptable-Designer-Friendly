using System;
using System.Collections.Generic;
using StarterKit.Base;
using UnityEngine;

[DisallowMultipleComponent] // to avoid attach another ParallaxBackground Script to attached game object.
[ExecuteInEditMode] //to allow scene view changes directly attached to game view
public class ParallaxBackground : BaseObject
{


    [HeaderAttribute("that its name ParallaxManager and have a ChildObject that its name ParallaxBG")]
    [HeaderAttribute("This Script Requires a GameObject")]
    [Header("Parallax Background")]
    
    [Tooltip("This list must contain at least one ParallaxScriptable object")]
    [SerializeField] private List<ParallaxScriptable> parallaxLayers;

    [SerializeField][Tooltip("To instantiate scriptable objects as a child of ParallaxManager.")] private GameObject parallaxPrefab;

    [SerializeField][Range(3,10)] [Header("ParallaxBG RepeatCount")][Tooltip("Number of clones ParallaxBG that is child of Parallax Manager  3 is optimum performance")]
    private int RepeatCount;

    [SerializeField][Range(1,25)][Tooltip("This multiplier determine when the parallaxBG reposition on the progress 1.3 Ideal Form")] private float rePositionDistance;
    [SerializeField] private float distanceBetweenParallaxBGs;
    [Space] [Header("Object To Attach Parallax Layers")] [SerializeField]
    private Transform attachedObject;
    [SerializeField] private Vector3 distanceFromAttachedObject;
    [Space][Range(1,10)] [Tooltip("If game needs additional general scaling of all layers change the value or use 1 as defauls")]
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float Speed;
    private float RangeMultiplier;
    private GameObject[] ParallaxBGs;
    private GameObject ParallexObj;

    private float _firstXPosFront,_firstXPosBack;
    
    public override void BaseObjectAwake()
    {
        Debug.Log("PARALLAX AWAKE!");
    }

    public override void BaseObjectStart()
    {
        if (RepeatCount <= 2)
        {
            RepeatCount = 3;
        }
        ParallaxBGs=new GameObject[RepeatCount];
        RangeMultiplier=1;
        _firstXPosFront = attachedObject.position.x;
        _firstXPosBack = attachedObject.position.x;
        PlaceParallaxes();
    }




    public override void BaseObjectLateUpdate()
    {

        if (attachedObject.transform.hasChanged)
        {
            if (!GameManager.instance.MoveFlag)
            {
                MoveParallaxesRight();    
            }
            if(GameManager.instance.MoveFlag)
            {
                MoveParallaxesLeft();
            }

            if (Mathf.Abs(attachedObject.position.x - _firstXPosFront) >= rePositionDistance && GameManager.instance.MoveFlag)
            {
                
                    _firstXPosFront = attachedObject.position.x;
                    _firstXPosBack = attachedObject.position.x;
                    ChangeFirstSiblingPositionToFront();
                
            }

            if (Mathf.Abs(_firstXPosBack - attachedObject.position.x) >= rePositionDistance && !GameManager.instance.MoveFlag)
            {
             
                    _firstXPosBack = attachedObject.position.x;
                    _firstXPosFront = attachedObject.position.x;
                    ChangeLastSiblingPositionToBack();
                
            }

            if (GameManager.instance.isJumping)
            {
                
                MoveParallaxesUp();
                
            }

            if (GameManager.instance.isFalling)
            {
                MoveParallaxesDown();
            }
            

            attachedObject.transform.hasChanged = false;
        }
        
    }

    private void MoveParallaxesLeft()
    {
        foreach (var parallaxBG in ParallaxBGs)
        {
            for (int i = 0; i < parallaxBG.transform.childCount; i++)
            {
                if (parallaxLayers != null)
                    if (parallaxLayers[i].Speed > 0)
                    {
                        var modifier =
                            (Mathf.Abs(
                                1 * 1.0f / (parallaxLayers[i].LayerIndex != 0 ? parallaxLayers[i].LayerIndex : 1)))
                            * parallaxLayers[i].Speed;
                        parallaxBG.transform.GetChild(i).position -=
                            Time.fixedDeltaTime *modifier* transform.right;
                    }
                    else
                    {
                        var modifier = (Mathf.Abs(1* 1.0f / parallaxLayers[i].LayerIndex));
                        parallaxBG.transform.GetChild(i).position -=
                            Time.fixedDeltaTime * modifier * transform.right;    
                    }
                    
            }
        }
    }

    private void MoveParallaxesRight()
    {
        foreach (var parallaxBG in ParallaxBGs)
        {
            for (int i = 0; i < parallaxBG.transform.childCount; i++)
            {
                if (parallaxLayers != null)
                    if (parallaxLayers[i].Speed > 0)
                    {
                        var modifier =
                            (Mathf.Abs(
                                 1 * 1.0f / (parallaxLayers[i].LayerIndex != 0 ? parallaxLayers[i].LayerIndex : 1)))
                             * parallaxLayers[i].Speed;
                        parallaxBG.transform.GetChild(i).position +=
                            Time.fixedDeltaTime * modifier* transform.right;
                      
                    }
                    else
                    {
                        
                        var modifier = (Mathf.Abs(1* 1.0f / parallaxLayers[i].LayerIndex));
                        parallaxBG.transform.GetChild(i).position +=
                            Time.fixedDeltaTime * modifier * transform.right;    
                       // Debug.Log("Speed =0 And SpeedModifier = : " + modifier);
                    }
            }
        }
    }

    private void MoveParallaxesUp()
    {
        foreach (var parallaxBG in ParallaxBGs)
        {
            for (int i = 0; i < parallaxBG.transform.childCount; i++)
            {
                if (parallaxLayers != null)
                    if (parallaxLayers[i].Speed > 0)
                    {
                        var modifier =
                            (Mathf.Abs(
                                1 * 0.5f / (parallaxLayers[i].LayerIndex != 0 ? parallaxLayers[i].LayerIndex : 1)))
                            * parallaxLayers[i].Speed;
                        parallaxBG.transform.GetChild(i).position +=
                            Time.fixedDeltaTime * modifier* transform.up;
                      
                    }
                    else
                    {
                        
                        var modifier = (Mathf.Abs(1* 0.5f / parallaxLayers[i].LayerIndex));
                        parallaxBG.transform.GetChild(i).position +=
                            Time.fixedDeltaTime * modifier * transform.up;    
                        // Debug.Log("Speed =0 And SpeedModifier = : " + modifier);
                    }
            }
        }
    }
    
    private void MoveParallaxesDown()
    {
        foreach (var parallaxBG in ParallaxBGs)
        {
            for (int i = 0; i < parallaxBG.transform.childCount; i++)
            {
                if (parallaxLayers != null)
                    if (parallaxLayers[i].Speed > 0)
                    {
                        var modifier =
                            (Mathf.Abs(
                                1 * 0.5f / (parallaxLayers[i].LayerIndex != 0 ? parallaxLayers[i].LayerIndex : 1)))
                            * parallaxLayers[i].Speed;
                        parallaxBG.transform.GetChild(i).position +=
                            Time.fixedDeltaTime * modifier* transform.up*-1;
                      
                    }
                    else
                    {
                        
                        var modifier = (Mathf.Abs(1* 0.5f / parallaxLayers[i].LayerIndex));
                        parallaxBG.transform.GetChild(i).position +=
                            Time.fixedDeltaTime * modifier * transform.up*-1;    
                        // Debug.Log("Speed =0 And SpeedModifier = : " + modifier);
                    }
            }
        }
    }
    
    

    private void PlaceParallaxes()
    {
        CreateParallaxBG(); //at least one BG mus be created.

        for (int i = 1; i < RepeatCount; i++)
        {
          InstantiateParallaxBgClone(i);

        }
        
        
    }

    private void InstantiateParallaxBgClone(int repeatCount)
    {
        var temp = GameObject.Find("ParallaxBG");
        
        var ParallaxBG = Instantiate(temp, 
            repeatCount % 2 == 0 ? ParallaxBGClonePos(temp.transform.position) : ParallaxBGClonePos(temp.transform.position)*-1,
            Quaternion.identity);
        if (repeatCount % 2 == 0) RangeMultiplier++;
        if (repeatCount %2==1)
        {
            ParallaxBG.transform.parent =  GameObject.Find("ParallaxManager").transform;
            ParallaxBG.transform.SetAsFirstSibling();
        }
        else
        {
            ParallaxBG.transform.parent =  GameObject.Find("ParallaxManager").transform; //Attach ParallaxManager as a child object
            ParallaxBG.transform.SetAsLastSibling();
        }

        ParallaxBGs[repeatCount] = ParallaxBG;


    }

    private Vector3 ParallaxBGClonePos(Vector3 pos)
    {
        return new Vector3(pos.x+distanceBetweenParallaxBGs*RangeMultiplier,pos.y,pos.z);
    }

    private void CreateParallaxBG()
    {
        //First Strategy:   instantiate a empty game object and attach scriptable properties. 
        //Second Strategy:  design a prefab game object and instantiate with scriptable properties. "CHOSEN"

     
        
        //parallaxLayers[0].InitalSpeed;
        
        
        foreach (var Obj in parallaxLayers)
        {
            ParallexObj= Instantiate(parallaxPrefab, PositionAccordingToLayerIndex(Obj.LayerIndex,Obj.İnitialPosition), Quaternion.identity);
            ParallexObj.GetComponent<SpriteRenderer>().sprite = Obj.ParallaxImage;
           
            ParallexObj.transform.localScale = SetParallaxScale(ParallexObj.transform.localScale,Obj);
            
            ParallexObj.transform.parent =  GameObject.Find("ParallaxBG").transform; //Attach ParallaxManager as a child object

        }

        ParallaxBGs[0] =GameObject.Find("ParallaxBG");

    }

    private Vector3 SetParallaxScale(Vector3 transformLocalScale, ParallaxScriptable obj)
    {
        if (obj.ScaleMultiplierX <= 0)
        {
            obj.ScaleMultiplierX = 1;
        }

        if (obj.ScaleMultiplierY <= 0)
        {
            obj.ScaleMultiplierY = 1;
        }
        return new Vector3(transformLocalScale.x*obj.ScaleMultiplierX,transformLocalScale.y*obj.ScaleMultiplierY,transformLocalScale.z)*scaleMultiplier;
    }

    private Vector3 PositionAccordingToLayerIndex(int objLayerIndex, Vector3 objInitialPosition)
    {
        //initial position is optional.  Can be vector zero. 
        var pos = attachedObject.position + objInitialPosition + distanceFromAttachedObject;
        return new Vector3(pos.x, pos.y, pos.z * objLayerIndex);
    }








    private void ChangeLastSiblingPositionToBack()
    {
        var fistPos = transform.GetChild(0).position; //take last child pos to move back as first child.
        var newPos = new Vector3(fistPos.x-distanceBetweenParallaxBGs,fistPos.y,fistPos.z); //Sliding position.
        transform.GetChild(transform.childCount - 1).position = newPos;
        transform.GetChild(transform.childCount - 1).SetAsFirstSibling();
        
    }

    private void ChangeFirstSiblingPositionToFront()
    {
        var lastPos = transform.GetChild(transform.childCount-1).position; //take first child pos to move forward as last child.
        var newPos = new Vector3(lastPos.x+distanceBetweenParallaxBGs,lastPos.y,lastPos.z); //Sliding position.
        transform.GetChild(0).position = newPos;
        transform.GetChild(0).SetAsLastSibling();
    }


    public override void BaseObjectDestroy()
    {
        Debug.Log("PARALLAX DESTROY!");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using StarterKit.Base;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //TODO refactor
    
    private bool moveFlag;
    

    
    
    [SerializeField]
    private Player player;
    [SerializeField]
    private ParallaxBackground parallaxBackground;

    [SerializeField]
    private BaseObject[] baseObjects;

    public bool isJumping;
    public bool isFalling;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        CallBaseObjectAwakes();
    }

    private void Start()
    {
        CallBaseObjectStarts();
    }

    private void Update()
    {
        CallBaseObjectUpdates();
    }

    private void LateUpdate()
    {
        CallBaseObjectLateUpdate();
    }

    private void OnDestroy()
    {
        CallBaseObjectDestroys();
    }

    private void CallBaseObjectAwakes()
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            baseObjects[i].BaseObjectAwake();
        }
    }

    private void CallBaseObjectStarts()
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            baseObjects[i].BaseObjectStart();
        }
    }

    private void CallBaseObjectUpdates()
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            baseObjects[i].BaseObjectUpdate();
        }
    }
    
    private void CallBaseObjectLateUpdate()
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            baseObjects[i].BaseObjectLateUpdate();
        }
    }

    private void CallBaseObjectDestroys()
    {
        for (int i = 0; i < baseObjects.Length; i++)
        {
            baseObjects[i].BaseObjectDestroy();
        }
    }
    
    public bool MoveFlag
    {
        get => moveFlag;
        set => moveFlag = value;
    }
}

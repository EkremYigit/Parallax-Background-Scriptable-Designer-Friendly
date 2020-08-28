using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Parallax")]
public class ParallaxScriptable : ScriptableObject
{
  [Header("--Parallax Customizer--")]
  [HeaderAttribute("ParallaxBG places with their layer indexes")]
  [SerializeField] private int layerIndex;
  [SerializeField] private Sprite parallaxImage;
  [SerializeField] private Vector3 initialPosition;
  [SerializeField] private float speed;

  [SerializeField] private float scaleMultiplierX;
  [SerializeField] private float scaleMultiplierY;
  [SerializeField][Tooltip("obje sahnede karakter duruyorken harekete edecek ise mesela tepeden geçen kuşlar.")] private float initalSpeed;

  public float ScaleMultiplierX
  {
    get => scaleMultiplierX;
    set => scaleMultiplierX = value;
  }

  public float ScaleMultiplierY
  {
    get => scaleMultiplierY;
    set => scaleMultiplierY = value;
  }

  public int LayerIndex
  {
    get => layerIndex;
    set => layerIndex = value;
  }

  public Sprite ParallaxImage
  {
    get => parallaxImage;
    set => parallaxImage = value;
  }

  public Vector3 İnitialPosition
  {
    get => initialPosition;
    set => initialPosition = value;
  }

  public float Speed
  {
    get => speed;
    set => speed = value;
  }


  public float InitalSpeed
  {
    get => initalSpeed;
    set => initalSpeed = value;
  }
}

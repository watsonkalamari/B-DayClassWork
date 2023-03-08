using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class UISpritesAnimation : MonoBehaviour
{
    //public float duration;


   
    //public Sprite[] sprites;
    private SpriteRenderer renderer;
    private Image image;
   // private int index = 0;
   // private float timer = 0;

    void OnEnable()
    {
        //index = 0;
      // image.sprite = sprites[0];
    }

    void OnDisable()
    {
        //index = 0;
        //image.sprite = sprites[0];
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    
    }
    private void Update()
    {
        image.sprite = renderer.sprite;
        //if ((timer += Time.deltaTime) >= (duration / sprites.Count))
        //{
        //    timer = 0;
        //    image.sprite = sprites[index];
        //    index = (index + 1) % sprites.Count;
        //}
    }
}
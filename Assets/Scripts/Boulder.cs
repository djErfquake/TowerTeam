using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private int animationIndex = 0;
    public List<Sprite> animationImages = new List<Sprite>();
    

    private Vector2 lastPosition = Vector2.zero;


    private List<GameObject> playersInRange = new List<GameObject>();



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 500;
    }

    private void Update()
    {
        Vector2 currentPosition = transform.localPosition;
        if (Vector2.Distance(currentPosition, lastPosition) >= 0.5f)
        {
            animationIndex++;
            if (animationIndex >= animationImages.Count) { animationIndex = 0; }
            spriteRenderer.sprite = animationImages[animationIndex];
            lastPosition = currentPosition;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;
        
        if (colliderObject.CompareTag("Player") && !playersInRange.Contains(colliderObject))
        {
            //Debug.Log(colliderObject.name + " entered");
            playersInRange.Add(colliderObject);
            if (playersInRange.Count == GameManager.instance.explorers.Count)
            {
                rb.mass = 0.5f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;
        
        if (colliderObject.CompareTag("Player") && playersInRange.Contains(colliderObject))
        {
            //Debug.Log(colliderObject.name + " exited");
            playersInRange.Remove(colliderObject);
            if (playersInRange.Count < GameManager.instance.explorers.Count)
            {
                rb.mass = 500f;
            }
        }
    }


}

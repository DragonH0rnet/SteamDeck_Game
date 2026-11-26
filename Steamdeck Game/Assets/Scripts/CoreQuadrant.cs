using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoreQuadrant : MonoBehaviour
{

    [SerializeField] private int quadrantNum;
    [SerializeField] private GameObject sprite;

    private float cooldown = 0f;
    private float maxCool = 10f;
    private float damage = 5;
    
    private InputAction triggerAction;
    private SpriteRenderer spriteRenderer;
    
    private Collider2D collider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateWedge();
        if (quadrantNum == 1)
        {
            triggerAction = InputSystem.actions.FindAction("Core Quadrant 1");
        }
        if (quadrantNum == 2)
        {
            triggerAction = InputSystem.actions.FindAction("Core Quadrant 2");
        }
        if (quadrantNum == 3)
        {
            triggerAction = InputSystem.actions.FindAction("Core Quadrant 3");
        }
        if (quadrantNum == 4)
        {
            triggerAction = InputSystem.actions.FindAction("Core Quadrant 4");
        }
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Color tempColor = spriteRenderer.color;
        if (cooldown < 9.7)
        {
            tempColor.a = (cooldown / maxCool) * 35;
            spriteRenderer.color = tempColor;
        }
        
        if  (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0f && triggerAction.triggered)
        {
            List<Collider2D> overlaps = new List<Collider2D>();
            collider.Overlap(overlaps);
            foreach (Collider2D col in overlaps)
            {
                if (col.tag == "Enemy")
                {
                    Enemy script = col.gameObject.GetComponent<Enemy>();
                    script.TakeDamage(damage);
                }
            }

            tempColor.a = 150;
            spriteRenderer.color = tempColor;
        }
    }
    
    public void GenerateWedge()
    {
        float radius = 2.5f;
        float angle = 90f;
        int arcSegments = 12;
        
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[arcSegments + 2];

        // Start at the center of the circle
        points[0] = Vector2.zero;

        float startAngle = -angle / 2f;
        float angleStep = angle / arcSegments;

        // Plot points along the outer curve
        for (int i = 0; i <= arcSegments; i++)
        {
            float rad = (startAngle + angleStep * i) * Mathf.Deg2Rad;
            points[i + 1] = new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);
        }
        
        poly.points = points;
        
    }
}

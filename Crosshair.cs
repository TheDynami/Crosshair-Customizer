using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    Camera cam;

    public Image[] images;
    public Transform[] transforms, outlineTransforms;
    public RectTransform dot;
    public RectTransform left, right, up, down;

    public float dotSize;

    public float length, width, gap, outline;
    public bool tStyle;

    public Color neutral, enemy;
    public LayerMask enemyLayer;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        ApplyChanges();
    }

    public void ApplyChanges()
    {
        //size
        foreach (RectTransform rect in transforms)
            if (rect.name == "left" || rect.name == "right")
                rect.sizeDelta = new Vector2(length, width);
            else if (rect.name == "up" || rect.name == "down")
                rect.sizeDelta = new Vector2(width, length);
            else
                rect.sizeDelta = new Vector2(dotSize, dotSize);

        //gap
        dot.localPosition = new Vector2(0, 0);
        left.localPosition = new Vector2(gap, 0);
        right.localPosition = new Vector2(-gap, 0);
        up.localPosition = new Vector2(0, -gap);
        down.localPosition = new Vector2(0, gap);

        //tStyle
        if (tStyle)
            up.gameObject.SetActive(false);
        else
            up.gameObject.SetActive(true);

        //Colour
        if (!enemyCheck())
        {
            foreach (Image image in images)
                image.color = neutral;
        }
        else
        {
            foreach (Image image in images)
                image.color = enemy;
        }
    }

    bool enemyCheck()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer);
    }
} 

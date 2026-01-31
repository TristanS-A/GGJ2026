using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SkiballLine : MonoBehaviour
{
    public static SkiballLine instance;

    [SerializeField] LineRenderer line;

    GameObject skiballTarget;
    bool drawingLine;

    [SerializeField] float criticalDistanceStart = 1;
    [SerializeField] float criticalDistanceEnd = 5;

    [SerializeField] Color baseLineColor = Color.green;
    [SerializeField] Color criticalLineColor = Color.red;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (drawingLine)
        {
            line.SetPosition(0, skiballTarget.transform.position);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                line.SetPosition(1, hit.point);
                UpdateGradient(hit.point);
            }
            else
            {
                line.SetPosition(1, skiballTarget.transform.position);
            }
        }
    }

    public void StartLine(GameObject target)
    {
        skiballTarget = target;
        drawingLine = true;
    }

    public void EndLine()
    {
        drawingLine = false;

        line.SetPosition(0, Vector3.one * -10);
        line.SetPosition(1, Vector3.one * -10);
    }

    public void UpdateGradient(Vector3 hitPoint)
    {
        Gradient gradient = new Gradient();

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        float distance = Vector3.Distance(hitPoint, skiballTarget.transform.position);

        Color finalColor;
        float midPoint;

        if (distance <= criticalDistanceStart)
        {
            // Below threshold: end of line is a partial blend, never fully end color
            float t = distance / criticalDistanceStart;
            finalColor = Color.Lerp(baseLineColor, criticalLineColor, t);
            midPoint = 1f; // no gradient spread, just a single blend
        }
        else
        {
            // Above threshold: line reaches full end color, and it creeps forward
            float overshoot = (distance - criticalDistanceStart) / (criticalDistanceEnd - criticalDistanceStart);
            overshoot = Mathf.Clamp01(overshoot);
            midPoint = Mathf.Lerp(1f, 0f, overshoot);
            finalColor = criticalLineColor;
        }

        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0] = new GradientColorKey(baseLineColor, 0f);    
        colorKeys[1] = new GradientColorKey(finalColor, midPoint); 
        colorKeys[2] = new GradientColorKey(finalColor, 1f);

        gradient.SetKeys(colorKeys, alphaKeys);

        line.colorGradient = gradient;
    }
}

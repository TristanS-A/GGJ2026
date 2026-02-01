using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float speed = 100f;

    private Vector3 randomAxis;

    void Start()
    {
        randomAxis = Random.onUnitSphere;
    }

    void Update()
    {
        // Pick a new random axis each frame
        randomAxis = Vector3.Slerp(randomAxis, Random.onUnitSphere, Time.deltaTime);
        transform.Rotate(randomAxis * speed * Time.deltaTime);
    }
}
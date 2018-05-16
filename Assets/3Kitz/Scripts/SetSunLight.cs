using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour
{
    Material sky;

    public Transform stars;

    void Start()
    {
        sky = RenderSettings.skybox;
    }

    void Update()
    {
        stars.transform.rotation = transform.rotation;

        Vector3 tvec = Camera.main.transform.position;
    }
}
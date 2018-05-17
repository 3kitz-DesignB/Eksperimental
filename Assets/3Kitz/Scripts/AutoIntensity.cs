using UnityEngine;
using System.Collections;

public class AutoIntensity : MonoBehaviour
{
    [SerializeField]
    private Gradient nightDayColor;

    [SerializeField]
    private float
        maxIntensity = 3f,
        minIntensity = 0f,
        minPoint = -0.2f;

    [SerializeField]
    private float
        maxAmbient = 1f,
        minAmbient = 0f,
        minAmbientPoint = -0.2f;

    [SerializeField] private Gradient nightDayFogColor;
    [SerializeField] private AnimationCurve fogDensityCurve;
    [SerializeField] private float fogScale = 1f;

    [SerializeField]
    private float
        dayAtmosphereThickness = 0.4f,
        nightAtmosphereThickness = 0.87f;

    [SerializeField]
    private Vector3
        dayRotateSpeed,
        nightRotateSpeed;
    
    private Light mainLight;
    private Skybox sky;
    private Material skyMat;

    public float SkySpeed { get; set; } = 1f;

    private void Start()
    {
        this.mainLight = GetComponent<Light>();
        this.skyMat = RenderSettings.skybox;
    }

    private void Update()
    {
        float tRange = 1f - this.minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(this.mainLight.transform.forward, Vector3.down) - this.minPoint) / tRange);
        float intensity = ((this.maxIntensity - this.minIntensity) * dot) + this.minIntensity;

        this.mainLight.intensity = intensity;

        tRange = 1 - this.minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(this.mainLight.transform.forward, Vector3.down) - this.minAmbientPoint) / tRange);
        intensity = ((this.maxAmbient - this.minAmbient) * dot) + this.minAmbient;
        RenderSettings.ambientIntensity = intensity;

        this.mainLight.color = this.nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = this.mainLight.color;

        RenderSettings.fogColor = this.nightDayFogColor.Evaluate(dot);
        RenderSettings.fogDensity = this.fogDensityCurve.Evaluate(dot) * this.fogScale;

        intensity = ((this.dayAtmosphereThickness - this.nightAtmosphereThickness) * dot) + this.nightAtmosphereThickness;
        this.skyMat.SetFloat("_AtmosphereThickness", intensity);
        
        this.transform.Rotate((dot > 0 ? this.dayRotateSpeed : this.nightRotateSpeed) * Time.deltaTime * this.SkySpeed);
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.SkySpeed *= 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            this.SkySpeed *= 2f;
        }
    }
}
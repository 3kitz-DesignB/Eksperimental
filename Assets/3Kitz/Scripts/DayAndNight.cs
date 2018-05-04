using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    private Material currentSkybox;

    [SerializeField]
    private Vector3 RotationVector = new Vector3(1, 0.1f, 0.1f);

    private GameObject sun, moon;

    private Light sunLight, moonLight;

    private bool sunOn, moonOn;

    [SerializeField]
    private Vector2 SunIntensity = new Vector2(0.25f, 1.1f), MoonIntensity = new Vector2(0.25f, 0.75f);

    [SerializeField]
    private AnimationCurve intensityCurve;

    [SerializeField]
    private Material sunSky, moonSky;

    [SerializeField]
    private float TimeOfDay = 0.25f, TimeSpeed = 0.25f;

    private float sunSize, moonSize, sunThickness, moonThickness, sunExpo, moonExpo;

    private Color sunColor, moonColor;

    public void Update()
    {
        TimeOfDay += TimeSpeed * Time.deltaTime * Time.timeScale;
        if (TimeOfDay > 1)
            TimeOfDay -= 1;
        else if (TimeOfDay < 0)
            TimeOfDay += 1;
        UpdateEnables();
        UpdateIntensity();
        UpdateSkybox();

        transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(0, 360, TimeOfDay), transform.right);
        if (TimeOfDay > 0.56f && RenderSettings.sun != moonLight)
        {
            RenderSettings.sun = moonLight;
        }
        else if (TimeOfDay < 0.56f && RenderSettings.sun != sunLight)
        {
            RenderSettings.sun = sunLight;
        }
    }

    private void UpdateSkybox()
    {
        if (TimeOfDay > 0.4f && TimeOfDay < 0.6f)
        {
            float t = Mathf.Clamp01(Mathf.InverseLerp(0.4f, 0.6f, TimeOfDay));
            RenderSettings.skybox.SetFloat("_SunSize", Mathf.Lerp(sunSize, moonSize, t));
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", Mathf.Lerp(sunThickness, moonThickness, t));
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(sunExpo, moonExpo, t));
            RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(sunColor, moonColor, t));
        }
        else if (TimeOfDay > 0.9f || TimeOfDay < 0.1f)
        {
            float t = 0;
            if (TimeOfDay < 0.9f)
            {
                t = Mathf.Clamp01(Mathf.InverseLerp(-0.1f, 0.1f, TimeOfDay));
            }
            else
            {
                t = Mathf.Clamp01(Mathf.InverseLerp(0.9f, 1.1f, TimeOfDay));
            }
            RenderSettings.skybox.SetFloat("_SunSize", Mathf.Lerp(moonSize, sunSize, t));
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", Mathf.Lerp(moonThickness, sunThickness, t));
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(moonExpo, sunExpo, t));
            RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(moonColor, sunColor, t));
        }
    }

    private void Awake()
    {
        currentSkybox = RenderSettings.skybox;
        sun = transform.GetChild(0).gameObject;
        sunLight = sun.GetComponent<Light>();
        moon = transform.GetChild(1).gameObject;
        moonLight = moon.GetComponent<Light>();
        sunSize = sunSky.GetFloat("_SunSize");
        moonSize = moonSky.GetFloat("_SunSize");
        sunThickness = sunSky.GetFloat("_AtmosphereThickness");
        moonThickness = moonSky.GetFloat("_AtmosphereThickness");
        sunExpo = sunSky.GetFloat("_Exposure");
        moonExpo = moonSky.GetFloat("_Exposure");
        sunColor = sunSky.GetColor("_SkyTint");
        moonColor = moonSky.GetColor("_SkyTint");

        //if (TimeOfDay > 0.5f)
        //{
        //    sunOn = false;
        //    sun.SetActive(false);
        //    moonOn = true;
        //    moon.SetActive(true);
        //}
        //else
        //{
        //    sunOn = true;
        //    sun.SetActive(true);
        //    moonOn = false;
        //    moon.SetActive(false);
        //}
    }

    private void UpdateEnables()
    {
        //if (!moonOn && (TimeOfDay > 0.475f && (TimeOfDay < 1.025f || TimeOfDay < 0.025f)))
        //{
        //    moonOn = true;
        //    moon.SetActive(true);
        //}
        //else if (moonOn && (TimeOfDay < 0.475f && (TimeOfDay > 1.025f || TimeOfDay > 0.025f)))
        //{
        //    moonOn = false;
        //    moon.SetActive(false);
        //}
        //if (!sunOn && (TimeOfDay < 0.525f && (TimeOfDay > -0.025f || TimeOfDay > 0.975f)))
        //{
        //    sunOn = true;
        //    sun.SetActive(true);
        //}
        //else if (sunOn && (TimeOfDay > 0.525f && (TimeOfDay < -0.025f || TimeOfDay < 0.975f)))
        //{
        //    sunOn = false;
        //    sun.SetActive(false);
        //}
    }

    private void UpdateIntensity()
    {
        //if (sunOn)
        //{
        sunLight.intensity = Mathf.Lerp(SunIntensity.x, SunIntensity.y, intensityCurve.Evaluate(Mathf.InverseLerp(0, 0.51f, TimeOfDay)));
        //}
        //if (moonOn)
        //{
        moonLight.intensity = Mathf.Lerp(MoonIntensity.x, MoonIntensity.y, intensityCurve.Evaluate(Mathf.InverseLerp(0.51f, 1, TimeOfDay)));
        //}
    }
}
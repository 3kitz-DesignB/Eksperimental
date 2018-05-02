using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    private Material currentSkybox;

    private GameObject sun, moon;

    private Light sunLight, moonLight;
    private bool sunOn, moonOn;

    [SerializeField]
    private Vector2 sunSize = new Vector2(0.04f, 0.06f), SunIntensity = new Vector2(0.25f, 1.1f), MoonIntensity = new Vector2(0.25f, 0.75f);

    [SerializeField]
    private AnimationCurve sunSizeCurve, intensityCurve;

    [SerializeField]
    private float TimeOfDay = 0.25f, TimeSpeed = 0.25f;

    public void Update()
    {
        TimeOfDay += TimeSpeed * Time.deltaTime * Time.timeScale;
        if (TimeOfDay > 1)
            TimeOfDay -= 1;
        else if (TimeOfDay < 0)
            TimeOfDay += 1;
        currentSkybox.SetFloat("_SunSize", Mathf.Lerp(sunSize.y, sunSize.x, sunSizeCurve.Evaluate((TimeOfDay % 0.5f) / 0.5f)));
        UpdateEnables();
        UpdateIntensity();
    }

    private void Awake()
    {
        currentSkybox = RenderSettings.skybox;
        sun = transform.GetChild(0).gameObject;
        sunLight = sun.GetComponent<Light>();
        moon = transform.GetChild(1).gameObject;
        moonLight = moon.GetComponent<Light>();

        if (TimeOfDay > 0.5f)
        {
            sunOn = false;
            sun.SetActive(false);
            moonOn = true;
            moon.SetActive(true);
        }
        else
        {
            sunOn = true;
            sun.SetActive(true);
            moonOn = false;
            moon.SetActive(false);
        }
    }

    private void UpdateEnables()
    {
        if (!moonOn && (TimeOfDay > 0.475f && (TimeOfDay < 1.025f || TimeOfDay < 0.025f)))
        {
            moonOn = true;
            moon.SetActive(true);
        }
        else if (moonOn && (TimeOfDay < 0.475f && (TimeOfDay > 1.025f || TimeOfDay > 0.025f)))
        {
            moonOn = false;
            moon.SetActive(false);
        }
        if (!sunOn && (TimeOfDay < 0.525f && (TimeOfDay > -0.025f || TimeOfDay > 0.975f)))
        {
            sunOn = true;
            sun.SetActive(true);
        }
        else if (sunOn && (TimeOfDay > 0.525f && (TimeOfDay < -0.025f || TimeOfDay < 0.975f)))
        {
            sunOn = false;
            sun.SetActive(false);
        }
    }

    private void UpdateIntensity()
    {
        if (sunOn)
        {
            sunLight.intensity = Mathf.Lerp(SunIntensity.y, SunIntensity.x, intensityCurve.Evaluate((TimeOfDay % 0.5f) / 0.5f));
        }
        if (moonOn)
        {
            moonLight.intensity = Mathf.Lerp(MoonIntensity.y, MoonIntensity.x, intensityCurve.Evaluate((TimeOfDay % 0.5f) / 0.5f));
        }
    }
}
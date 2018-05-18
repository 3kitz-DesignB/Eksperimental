using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureSafari : MonoBehaviour
{
    [SerializeField]
    private IntVariable modifier;

    [SerializeField]
    private Vector2Int screenSize = new Vector2Int(1920, 1080);

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F10))
            TakePicture();
    }

    private void TakePicture()
    {
        RenderTexture rt = new RenderTexture(screenSize.x, screenSize.y, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(screenSize.x, screenSize.y, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenSize.x, screenSize.y), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/screenshots/screenshot_" + modifier.value + ".png";
        System.IO.File.WriteAllBytes(filename, bytes);
        modifier.value++;
    }
}
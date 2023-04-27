using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.IO;
//using System.Drawing.Imaging;

public class GifManager : MonoBehaviour
{
    private const float Fps = 24;
    private UnityEngine.UI.Image _image;
    public List<Texture2D> _tex2DList = new List<Texture2D>();
    private float _time;
    private int _framCount;

    private void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
    }

    /// <summary>
    /// ??Gif???????
    /// </summary>
    /// <param name="path"></param>
    public void SetGifPath(string path)
    {
        //var image = System.Drawing.Image.FromFile(path);
        //_tex2DList = GifToTexture2D(image);
    }


}

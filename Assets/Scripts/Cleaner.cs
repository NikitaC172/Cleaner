using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Cleaner : MonoBehaviour
{
    [SerializeField] private Init _init;
    [SerializeField] private int _circleSize;
    [SerializeField] private Texture2D _mainTexture;
    [SerializeField] private int _maskTextureResolution;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Collider _hole;
    [SerializeField] private int _offsetX;
    [SerializeField] private int _offsetZ;
    [SerializeField] private float _scaleRatio = 1 / 3.5f;
    [SerializeField] private float _delayRenderSecond;

    private int _pointX;
    private int _pointZ;

    private Texture2D _maskTexture;
    private MeshCollider _collider;
    private MeshRenderer _renderer;
    private Upgrader _upgrader;

    public int TotalMaskPixels => _maskTextureResolution * _maskTextureResolution;
    public int TotalClearedPixels { get; private set; }

    private void Awake()
    {
        _upgrader = _init.GetUpgrader();
    }

    private void OnEnable()
    {
        _upgrader.ChangedSize += ChangeRadius;
    }

    private void Start()
    {
        _collider = GetComponent<MeshCollider>();
        _renderer = GetComponent<MeshRenderer>();

        _maskTexture = new Texture2D(_maskTextureResolution, _maskTextureResolution, TextureFormat.ARGB32, false);

        _renderer.material.SetTexture("_Mask", _maskTexture);
        _renderer.material.mainTexture = _mainTexture;
        _maskTexture.filterMode = FilterMode.Point;
        _maskTexture.wrapMode = TextureWrapMode.Clamp;

        for (int x = 0; x < _maskTexture.width; x++)
        {
            for (int y = 0; y < _maskTexture.height; y++)
            {
                _maskTexture.SetPixel(x, y, Color.red);
            }
        }

        _maskTexture.Apply();
    }

    private void OnDisable()
    {
        _upgrader.ChangedSize -= ChangeRadius;
    }

    private void ChangeRadius(float deltaRadius)
    {
        int percent = 100;
        _circleSize += (int)(deltaRadius * percent);
    }

    private void OnValidate()
    {
        if (_maskTextureResolution < 128)
            _maskTextureResolution = 512;
        if (_circleSize <= 0)
            _circleSize = 80;
        if (_mainTexture == null)
            throw new System.Exception($"Нет основной текстуры на объекте {_gameObject}");
    }


    private void Update()
    {
        _pointX = (int)(_offsetX + transform.position.x - _gameObject.transform.position.x * _maskTexture.width * (_scaleRatio));
        _pointZ = (int)(_offsetZ + transform.position.y - _gameObject.transform.position.z * _maskTexture.height * (_scaleRatio));

        DrawCircle(_pointX, _pointZ);
        _maskTexture.Apply();
    }


    private void DrawCircle(int pointX, int pointY)
    {
        for (int y = 0; y < _circleSize; y++)
        {
            for (int x = 0; x < _circleSize; x++)
            {
                float x2 = Mathf.Pow(x - _circleSize / 2, 2);
                float y2 = Mathf.Pow(y - _circleSize / 2, 2);
                float r2 = Mathf.Pow(_circleSize / 2, 2);

                if ((x2 + y2) < r2)
                {
                    int settingPixelX = pointX + x - _circleSize / 2;
                    int settingPixelY = pointY + y - _circleSize / 2;
                    if (_maskTexture.GetPixel(settingPixelX, settingPixelY) != Color.green)
                    {
                        _maskTexture.SetPixel(settingPixelX, settingPixelY, Color.green);
                        TotalClearedPixels++;
                    }
                }
            }
        }
    }
}

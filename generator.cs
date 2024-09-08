using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Generator : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    [Range(3f, 100f)] public int _levelLength = 50;
    [Range(1f, 58f)] public float _xMultiplier = 2f;
    [Range(1f, 50f)] public float _yMultiplier = 2f;  // Added 'f' to ensure it's float
    [Range(0f, 1f)] public float _curveSmoothness = 0.5f;
    public float _noiseStep = 0.5f;
    public float _bottom = 10f;
    private Vector3 _lastPos;

    public void OnValidate()
    {
        spriteShapeController.spline.Clear();
        for (int i = 0; i < _levelLength; i++)  // Use '_levelLength' instead of 'levelLength'
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier);
            spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLength - 1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }

        // Close the terrain shape at the bottom
        spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastPos.x, transform.position.y - _bottom));
        spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }
}

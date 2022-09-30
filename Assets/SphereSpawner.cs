using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private int m_rows;
    [SerializeField] private int m_columns;
    [SerializeField] private float m_distanceBetweenSpheres = 1.5f;
    [SerializeField] private Material m_material;
    [SerializeField] private Color m_color2;
    [SerializeField] private Color m_color1;
    [SerializeField] private bool m_useMaterialPropertyBlock;
    private float m_startXPosition;
    private float m_startZPosition;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialPositions();
        
        SpawnSpheres();
    }

    private void SetInitialPositions()
    {
        m_startXPosition = (m_rows - 1) * m_distanceBetweenSpheres * -.5f;
        m_startZPosition = 0;
    }

    private void SpawnSpheres()
    {
        for (var i = 0; i < m_rows; i++)
        {
            for (var j = 0; j < m_columns; j++)
            {
                var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                var spawnPosition = CalculatePosition(i, j);
                obj.transform.position = spawnPosition;
                
                obj.GetComponent<Renderer>().material = m_material;
                
                var lerpColor = obj.AddComponent<LerpColor>();
                lerpColor.SetColor(m_color1,m_color2);
                lerpColor.SetOffset(i);
                lerpColor.UseMaterialPropertyBlock(m_useMaterialPropertyBlock);
            }
        }
    }

    private Vector3 CalculatePosition(int _i, int _j)
    {
        var positionX = m_startXPosition + _i * m_distanceBetweenSpheres;
        var positionZ = m_startZPosition + _j * m_distanceBetweenSpheres;
        return new Vector3(positionX, 0, positionZ);
    }
}

public class LerpColor : MonoBehaviour
{
    private Color m_color01;
    private Color m_color02;
    
    public float Speed = 5, m_offset;
 
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private bool m_isUsingMaterialPropertyBlock
        ;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }
 
    void Update()
    {
        
        if (m_isUsingMaterialPropertyBlock)
        {
            // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
        _propBlock.SetColor("_Color", Color.Lerp(m_color01, m_color02, (Mathf.Sin(Time.time * Speed + m_offset) + 1) / 2f));
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
        }
        else
        {
            _renderer.material.color = Color.Lerp(m_color01, m_color02, (Mathf.Sin(Time.time * Speed + m_offset) + 1) / 2f);
        }
    }

    public void SetColor(Color _color1, Color _color2)
    {
        m_color01 = _color1;
        m_color02 = _color2;
    }
    
    public void SetOffset(float _offset)
    {
        m_offset = _offset;
    }
    

    public void UseMaterialPropertyBlock(bool _switchTechnics)
    {
        m_isUsingMaterialPropertyBlock = _switchTechnics;
    }
}

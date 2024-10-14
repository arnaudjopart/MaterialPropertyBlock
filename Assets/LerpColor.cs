using UnityEngine;

public class LerpColor : MonoBehaviour
{
    private Color m_color01;
    private Color m_color02;
    private const float Speed = 5;
    private float m_offset;
 
    private Renderer m_renderer;

    private MaterialPropertyBlock m_propertyBlock;

    void Awake()
    {
        m_propertyBlock = new MaterialPropertyBlock();
        m_renderer = GetComponent<Renderer>();
    }
 
    void Update()
    {
        m_renderer.GetPropertyBlock(m_propertyBlock);
        m_renderer.material.color =Color.Lerp(m_color01, m_color02, (Mathf.Sin(Time.time * Speed + m_offset) + 1) / 2f);
        //m_propertyBlock.SetColor("_Color",Color.Lerp(m_color01, m_color02, (Mathf.Sin(Time.time * Speed + m_offset) + 1) / 2f));
        m_renderer.SetPropertyBlock(m_propertyBlock);
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
}
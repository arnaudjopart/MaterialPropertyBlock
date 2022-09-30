using UnityEngine;
public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private int m_rows;
    [SerializeField] private int m_columns;
    [SerializeField] private float m_distanceBetweenSpheres = 1.5f;
    [SerializeField] private Material m_material;
    [SerializeField] private Color m_color2;
    [SerializeField] private Color m_color1;

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
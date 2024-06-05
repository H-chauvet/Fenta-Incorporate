using UnityEngine;

[CreateAssetMenu(menuName = "New Palette", fileName = "Color Palette")]
public class ColorPalletteSO : ScriptableObject
{
    [SerializeField] private Color32[] colors = new Color32[16];
    
    public Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }
}

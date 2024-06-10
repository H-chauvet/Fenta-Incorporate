using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] private ColorPalletteSO colorPaletteSO;

    private void Start()
    {
        var modelRenderer = GetComponent<Renderer>();
        var baseMaterial = modelRenderer.material;
        var newMaterial = Instantiate(baseMaterial);
        newMaterial.color = colorPaletteSO.GetRandomColor();
        modelRenderer.material = newMaterial;
    }
}

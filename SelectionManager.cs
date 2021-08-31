using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material _highLightMaterial;
    [SerializeField] private Material _defaultMaterial;

    private Transform _selection;

    private void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = _defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.TryGetComponent(out SelectedItems items))
            {
                var SelectionRender = selection.GetComponent<Renderer>();
                if (SelectionRender != null)
                {
                    SelectionRender.material = _highLightMaterial;
                }
                _selection = selection;
            }
        }  
    }
}

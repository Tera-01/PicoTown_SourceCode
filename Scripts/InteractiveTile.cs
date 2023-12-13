using UnityEngine;

public class InteractiveTile : MonoBehaviour
{
    [SerializeField] private TileManager _tileManager;
    [SerializeField] private Transform _transform;

    private void Update()
    {
        _transform.position = _tileManager._floor.GetCellCenterWorld(_tileManager.GetBtnPressedPosition());
    }
}

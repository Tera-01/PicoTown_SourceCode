using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void LateUpdate()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * 2f);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}

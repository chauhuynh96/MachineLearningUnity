using UnityEngine;

public class MouseSelectAndDrag : MonoBehaviour
{
    public float planeY = 0f;     // mặt phẳng di chuyển
    public float yOffset = 0.05f;

    private Transform selectedObj;

    void Update()
    {
        // Click chuột trái → chọn object
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectObject();
        }

        // Giữ chuột → kéo object đã chọn
        if (Input.GetMouseButton(0) && selectedObj != null)
        {
            DragSelectedObject();
        }

        // Thả chuột → bỏ chọn
        if (Input.GetMouseButtonUp(0))
        {
            selectedObj = null;
        }
    }

    void TrySelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Goal"))   // object A
            {
                selectedObj = hit.collider.transform;
            }
        }
    }

    void DragSelectedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, planeY, 0));

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 pos = ray.GetPoint(enter);
            pos.y = planeY + yOffset;
            selectedObj.position = pos;
        }
    }
}

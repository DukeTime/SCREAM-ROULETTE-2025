using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // ������ �� Sprite Renderer �������
    public GameObject player; // ������ �� ������
    public int baseOrder = 0; // ������� Order in Layer

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player"); // ����� ������ �� ����
    }

    void Update()
    {
        if (player != null && spriteRenderer != null)
        {
            // �������� Y-���������� ������ � �������
            float playerY = player.transform.position.y;
            float objectY = transform.position.y;

            // ���� ������ ���� ������ (����� ����, ������ ���� �������), ������ ������ �� ������ ����
            if (objectY > playerY)
            {
                spriteRenderer.sortingOrder = baseOrder - 1; // ������ ���� (����, ����� ����� ��� ������)
            }
            // ���� ������ ���� ������ (����� ����, ������ ���� ������), ������ �� �������� ����
            else
            {
                spriteRenderer.sortingOrder = baseOrder + 1; // �������� ���� (����, ����� ������ ��� ������)
            }

            // �������: ������� ������� �������� ��� ��������
            Debug.Log($"Object Y: {objectY}, Player Y: {playerY}, Sorting Order: {spriteRenderer.sortingOrder}");
        }
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private string playerPrefabPath = "Prefabs/Player"; // Resources 폴더 내의 프리팹 경로
    private Transform groundTransform; // Ground의 Transform에 대한 참조

    private GameObject playerInstance;

    void Start()
    {
        InitializeGroundTransform();
        LoadAndSpawnPlayer();
    }

    private void InitializeGroundTransform()
    {
        // "Ground" 태그를 가진 오브젝트의 Transform을 찾음
        GameObject groundObject = GameObject.FindWithTag("Ground");
        if (groundObject != null)
        {
            groundTransform = groundObject.transform;
        }
        else
        {
            Debug.LogError("Ground 오브젝트를 찾을 수 없습니다. 'Ground' 태그가 설정되어 있는지 확인하세요.");
        }
    }

    private void LoadAndSpawnPlayer()
    {
        // Resources 폴더에서 Player 프리팹 로드
        GameObject playerPrefab = Resources.Load<GameObject>(playerPrefabPath);

        if (playerPrefab == null)
        {
            Debug.LogError("Player 프리팹을 로드할 수 없습니다. 'Assets/Resources/Prefabs/Player.prefab' 경로를 확인하세요.");
            return;
        }

        if (groundTransform == null)
        {
            Debug.LogError("Ground Transform이 할당되지 않았습니다. 'Ground' 태그가 설정되어 있는지 확인하세요.");
            return;
        }

        // Ground의 경계를 계산하여 Z 축의 가장 하단 위치를 찾음
        Renderer groundRenderer = groundTransform.GetComponent<Renderer>();
        if (groundRenderer == null)
        {
            Debug.LogError("Ground 오브젝트에 Renderer가 없습니다. 경계를 계산할 수 없습니다.");
            return;
        }

        Bounds groundBounds = groundRenderer.bounds;
        Vector3 spawnPosition = new Vector3(groundBounds.center.x, groundBounds.max.y + 0.5f, groundBounds.min.z);

        // 플레이어의 회전을 설정하여 XZ 평면에 수직으로 서 있도록 함
        Quaternion playerRotation = Quaternion.Euler(90, 0, 0); // X축을 기준으로 회전 설정

        // 계산된 위치와 회전으로 Player 프리팹 인스턴스화
        playerInstance = Instantiate(playerPrefab, spawnPosition, playerRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInstance != null && groundTransform != null)
        {
            // Get the ground's bounds
            Renderer groundRenderer = groundTransform.GetComponent<Renderer>();
            if (groundRenderer != null)
            {
                Bounds groundBounds = groundRenderer.bounds;

                // Get the player's current position
                Vector3 playerPosition = playerInstance.transform.position;

                // Clamp the player's position within the ground's bounds
                playerPosition.x = Mathf.Clamp(playerPosition.x, groundBounds.min.x, groundBounds.max.x);
                playerPosition.z = Mathf.Clamp(playerPosition.z, groundBounds.min.z, groundBounds.max.z);

                // Apply the clamped position back to the player
                playerInstance.transform.position = playerPosition;
            }
        }
    }
}

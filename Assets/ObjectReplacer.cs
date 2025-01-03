using UnityEngine;

public class MeshReplacer : MonoBehaviour
{
    public GameObject prefabToReplace;

    private void Start()
    {
        ReplaceMesh();
    }

    public void ReplaceMesh()
    {
        if (prefabToReplace == null)
        {
            Debug.LogError("Префаб для замены не выбран!");
            return;
        }

        // Проверяем наличие Skinned Mesh Renderer в префабе
        SkinnedMeshRenderer prefabSkinnedMesh = prefabToReplace.GetComponent<SkinnedMeshRenderer>();

        if (prefabSkinnedMesh != null)
        {
            // Если это скиннед меш
            ReplaceSkinnedMesh(prefabSkinnedMesh);
        }
        else
        {
            // Если это обычный меш
            MeshFilter prefabMeshFilter = prefabToReplace.GetComponent<MeshFilter>();
            MeshRenderer prefabMeshRenderer = prefabToReplace.GetComponent<MeshRenderer>();

            if (prefabMeshFilter != null && prefabMeshRenderer != null)
            {
                ReplaceStaticMesh(prefabMeshFilter, prefabMeshRenderer);
            }
        }
    }

    private void ReplaceStaticMesh(MeshFilter sourceMeshFilter, MeshRenderer sourceMeshRenderer)
    {
        // Получаем или создаем компоненты на текущем объекте
        MeshFilter currentMeshFilter = GetComponent<MeshFilter>();
        if (currentMeshFilter == null)
            currentMeshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer currentMeshRenderer = GetComponent<MeshRenderer>();
        if (currentMeshRenderer == null)
            currentMeshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Копируем меш
        currentMeshFilter.mesh = sourceMeshFilter.sharedMesh;

        // Копируем материалы
        currentMeshRenderer.materials = sourceMeshRenderer.sharedMaterials;

        // Копируем настройки рендерера
        currentMeshRenderer.shadowCastingMode = sourceMeshRenderer.shadowCastingMode;
        currentMeshRenderer.receiveShadows = sourceMeshRenderer.receiveShadows;
        currentMeshRenderer.lightProbeUsage = sourceMeshRenderer.lightProbeUsage;
        currentMeshRenderer.reflectionProbeUsage = sourceMeshRenderer.reflectionProbeUsage;
    }

    private void ReplaceSkinnedMesh(SkinnedMeshRenderer sourceSkinnedMesh)
    {
        // Удаляем обычный MeshRenderer и MeshFilter если они есть
        MeshRenderer existingRenderer = GetComponent<MeshRenderer>();
        if (existingRenderer != null)
            DestroyImmediate(existingRenderer);

        MeshFilter existingFilter = GetComponent<MeshFilter>();
        if (existingFilter != null)
            DestroyImmediate(existingFilter);

        // Получаем или создаем SkinnedMeshRenderer
        SkinnedMeshRenderer currentSkinnedMesh = GetComponent<SkinnedMeshRenderer>();
        if (currentSkinnedMesh == null)
            currentSkinnedMesh = gameObject.AddComponent<SkinnedMeshRenderer>();

        // Копируем меш
        currentSkinnedMesh.sharedMesh = sourceSkinnedMesh.sharedMesh;

        // Копируем материалы
        currentSkinnedMesh.materials = sourceSkinnedMesh.sharedMaterials;

        // Копируем кости и привязки
        currentSkinnedMesh.rootBone = sourceSkinnedMesh.rootBone;
        currentSkinnedMesh.bones = sourceSkinnedMesh.bones;

        // Копируем настройки рендерера
        currentSkinnedMesh.shadowCastingMode = sourceSkinnedMesh.shadowCastingMode;
        currentSkinnedMesh.receiveShadows = sourceSkinnedMesh.receiveShadows;
        currentSkinnedMesh.lightProbeUsage = sourceSkinnedMesh.lightProbeUsage;
        currentSkinnedMesh.reflectionProbeUsage = sourceSkinnedMesh.reflectionProbeUsage;
        currentSkinnedMesh.updateWhenOffscreen = sourceSkinnedMesh.updateWhenOffscreen;
        currentSkinnedMesh.skinnedMotionVectors = sourceSkinnedMesh.skinnedMotionVectors;
    }
}
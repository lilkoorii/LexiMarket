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
            Debug.LogError("������ ��� ������ �� ������!");
            return;
        }

        // ��������� ������� Skinned Mesh Renderer � �������
        SkinnedMeshRenderer prefabSkinnedMesh = prefabToReplace.GetComponent<SkinnedMeshRenderer>();

        if (prefabSkinnedMesh != null)
        {
            // ���� ��� ������� ���
            ReplaceSkinnedMesh(prefabSkinnedMesh);
        }
        else
        {
            // ���� ��� ������� ���
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
        // �������� ��� ������� ���������� �� ������� �������
        MeshFilter currentMeshFilter = GetComponent<MeshFilter>();
        if (currentMeshFilter == null)
            currentMeshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer currentMeshRenderer = GetComponent<MeshRenderer>();
        if (currentMeshRenderer == null)
            currentMeshRenderer = gameObject.AddComponent<MeshRenderer>();

        // �������� ���
        currentMeshFilter.mesh = sourceMeshFilter.sharedMesh;

        // �������� ���������
        currentMeshRenderer.materials = sourceMeshRenderer.sharedMaterials;

        // �������� ��������� ���������
        currentMeshRenderer.shadowCastingMode = sourceMeshRenderer.shadowCastingMode;
        currentMeshRenderer.receiveShadows = sourceMeshRenderer.receiveShadows;
        currentMeshRenderer.lightProbeUsage = sourceMeshRenderer.lightProbeUsage;
        currentMeshRenderer.reflectionProbeUsage = sourceMeshRenderer.reflectionProbeUsage;
    }

    private void ReplaceSkinnedMesh(SkinnedMeshRenderer sourceSkinnedMesh)
    {
        // ������� ������� MeshRenderer � MeshFilter ���� ��� ����
        MeshRenderer existingRenderer = GetComponent<MeshRenderer>();
        if (existingRenderer != null)
            DestroyImmediate(existingRenderer);

        MeshFilter existingFilter = GetComponent<MeshFilter>();
        if (existingFilter != null)
            DestroyImmediate(existingFilter);

        // �������� ��� ������� SkinnedMeshRenderer
        SkinnedMeshRenderer currentSkinnedMesh = GetComponent<SkinnedMeshRenderer>();
        if (currentSkinnedMesh == null)
            currentSkinnedMesh = gameObject.AddComponent<SkinnedMeshRenderer>();

        // �������� ���
        currentSkinnedMesh.sharedMesh = sourceSkinnedMesh.sharedMesh;

        // �������� ���������
        currentSkinnedMesh.materials = sourceSkinnedMesh.sharedMaterials;

        // �������� ����� � ��������
        currentSkinnedMesh.rootBone = sourceSkinnedMesh.rootBone;
        currentSkinnedMesh.bones = sourceSkinnedMesh.bones;

        // �������� ��������� ���������
        currentSkinnedMesh.shadowCastingMode = sourceSkinnedMesh.shadowCastingMode;
        currentSkinnedMesh.receiveShadows = sourceSkinnedMesh.receiveShadows;
        currentSkinnedMesh.lightProbeUsage = sourceSkinnedMesh.lightProbeUsage;
        currentSkinnedMesh.reflectionProbeUsage = sourceSkinnedMesh.reflectionProbeUsage;
        currentSkinnedMesh.updateWhenOffscreen = sourceSkinnedMesh.updateWhenOffscreen;
        currentSkinnedMesh.skinnedMotionVectors = sourceSkinnedMesh.skinnedMotionVectors;
    }
}
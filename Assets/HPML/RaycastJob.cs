using HPML.lib;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

public class RaycastJob : IJobFor
{
    public float3 origin;
    public float3 direction;
    
    [ReadOnly] public NativeArray<float3> vertices;
    [ReadOnly] public NativeArray<int3> triangles;
    
    public NativeArray<float> t;

    public void Execute(int index)
    {
        var indices = triangles[index];
        var triangle = new float3x3(vertices[indices.x], vertices[indices.y], vertices[indices.z]);
        
        scalar.raycast(origin, direction, triangle, out var tp, out var _);
        
        t[index] = t[index] > tp ? tp : t[index];
    }
}

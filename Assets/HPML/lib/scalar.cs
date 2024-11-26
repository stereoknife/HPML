using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using float3 = Unity.Mathematics.float3;

namespace HPML.lib
{
    public static partial class scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool moller_trumbore(float3 origin, float3 direction, float3x3 triangle, out float t)
        {
            float3 edge1 = triangle.c1 - triangle.c0;
            float3 edge2 = triangle.c2 - triangle.c0;
            float3 ray_cross_e2 = cross(direction, edge2);
            float det = dot(edge1, ray_cross_e2);
            
            t = 0f;

            if (det > -float.Epsilon && det < float.Epsilon)
                return false;    // This ray is parallel to this triangle.

            float inv_det = 1.0f / det;
            float3 s = origin - triangle.c0;
            float u = inv_det * dot(s, ray_cross_e2);

            if ((u < 0 && abs(u) > float.Epsilon) || (u > 1 && abs(u-1) > float.Epsilon))
                return false;

            float3 s_cross_e1 = cross(s, edge1);
            float v = inv_det * dot(direction, s_cross_e1);

            if ((v < 0 && abs(v) > float.Epsilon) || (u + v > 1 && abs(u + v - 1) > float.Epsilon))
                return false;

            // At this stage we can compute t to find out where the intersection point is on the line.
            t = inv_det * dot(edge2, s_cross_e1);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool raycast(float3 origin, float3 direction, float3x3 triangle, out float t, out float3 intersection)
        {
            var mt = moller_trumbore(origin, direction, triangle, out t);
            intersection = origin + direction * t;
            return mt && t > 0f;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool linecast(float3 point, float3 direction, float3x3 triangle, out float t, out float3 intersection)
        {
            var mt = moller_trumbore(point, direction, triangle, out t);
            intersection = point + direction * t;
            return mt;
        }
    }
}
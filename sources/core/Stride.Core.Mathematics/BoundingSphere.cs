// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
//
// -----------------------------------------------------------------------------
// Original code from SlimMath project. http://code.google.com/p/slimmath/
// Greetings to SlimDX Group. Original code published with the following license:
// -----------------------------------------------------------------------------
/*
* Copyright (c) 2007-2011 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Stride.Core.Mathematics;

/// <summary>
/// Represents a bounding sphere in three dimensional space.
/// </summary>
[DataContract]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct BoundingSphere : IEquatable<BoundingSphere>, ISpanFormattable, IIntersectableWithRay, IIntersectableWithPlane
{
    /// <summary>
    /// An empty bounding sphere (Center = 0 and Radius = 0).
    /// </summary>
    public static readonly BoundingSphere Empty = new();

    /// <summary>
    /// The center of the sphere in three dimensional space.
    /// </summary>
    public Vector3 Center;

    /// <summary>
    /// The radius of the sphere.
    /// </summary>
    public float Radius;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingSphere"/> struct.
    /// </summary>
    /// <param name="center">The center of the sphere in three dimensional space.</param>
    /// <param name="radius">The radius of the sphere.</param>
    public BoundingSphere(Vector3 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Ray ray)
    {
        return CollisionHelper.RayIntersectsSphere(in ray, ref this, out float _);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <param name="distance">When the method completes, contains the distance of the intersection,
    /// or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Ray ray, out float distance)
    {
        return CollisionHelper.RayIntersectsSphere(in ray, ref this, out distance);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The ray to test.</param>
    /// <param name="point">When the method completes, contains the point of intersection,
    /// or <see cref="Stride.Core.Mathematics.Vector3.Zero"/> if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Ray ray, out Vector3 point)
    {
        return CollisionHelper.RayIntersectsSphere(in ray, ref this, out point);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="Plane"/>.
    /// </summary>
    /// <param name="plane">The plane to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public PlaneIntersectionType Intersects(ref readonly Plane plane)
    {
        return CollisionHelper.PlaneIntersectsSphere(in plane, ref this);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triagnle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly Vector3 vertex1, ref readonly Vector3 vertex2, ref readonly Vector3 vertex3)
    {
        return CollisionHelper.SphereIntersectsTriangle(ref this, in vertex1, in vertex2, in vertex3);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly BoundingBox box)
    {
        return CollisionHelper.BoxIntersectsSphere(in box, ref this);
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a <see cref="BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public bool Intersects(ref readonly BoundingSphere sphere)
    {
        return CollisionHelper.SphereIntersectsSphere(ref this, in sphere);
    }

    /// <summary>
    /// Determines whether the current objects contains a point.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly Vector3 point)
    {
        return CollisionHelper.SphereContainsPoint(ref this, in point);
    }

    /// <summary>
    /// Determines whether the current objects contains a triangle.
    /// </summary>
    /// <param name="vertex1">The first vertex of the triangle to test.</param>
    /// <param name="vertex2">The second vertex of the triagnle to test.</param>
    /// <param name="vertex3">The third vertex of the triangle to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly Vector3 vertex1, ref readonly Vector3 vertex2, ref readonly Vector3 vertex3)
    {
        return CollisionHelper.SphereContainsTriangle(ref this, in vertex1, in vertex2, in vertex3);
    }

    /// <summary>
    /// Determines whether the current objects contains a <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The box to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly BoundingBox box)
    {
        return CollisionHelper.SphereContainsBox(ref this, in box);
    }

    /// <summary>
    /// Determines whether the current objects contains a <see cref="BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The sphere to test.</param>
    /// <returns>The type of containment the two objects have.</returns>
    public ContainmentType Contains(ref readonly BoundingSphere sphere)
    {
        return CollisionHelper.SphereContainsSphere(ref this, in sphere);
    }

    /// <summary>
    /// Constructs a <see cref="BoundingSphere"/> that fully contains the given points.
    /// </summary>
    /// <param name="points">The points that will be contained by the sphere.</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding sphere.</param>
    public static unsafe void FromPoints(Vector3[] points, out BoundingSphere result)
    {
        ArgumentNullException.ThrowIfNull(points);
        if (points.Length == 0) throw new ArgumentException("Array cannot be empty or null.", nameof(points));
        fixed (void* pointsPtr = points)
        {
            FromPoints((IntPtr)pointsPtr, 0, points.Length, Unsafe.SizeOf<Vector3>(), out result);
        }
    }

    /// <summary>
    /// Constructs a <see cref="Stride.Core.Mathematics.BoundingSphere" /> that fully contains the given unmanaged points.
    /// </summary>
    /// <param name="vertexBufferPtr">A pointer to of vertices containing points.</param>
    /// <param name="vertexPositionOffsetInBytes">The point offset in bytes starting from the vertex structure.</param>
    /// <param name="vertexCount">The verterx vertexCount.</param>
    /// <param name="vertexStride">The vertex stride (size of vertex).</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding sphere.</param>
    public static unsafe void FromPoints(IntPtr vertexBufferPtr, int vertexPositionOffsetInBytes, int vertexCount, int vertexStride, out BoundingSphere result)
    {
        if (vertexBufferPtr == IntPtr.Zero)
        {
            throw new ArgumentNullException(nameof(vertexBufferPtr));
        }

        var startPoint = (byte*)vertexBufferPtr + vertexPositionOffsetInBytes;

        //Find the center of all points.
        Vector3 center = Vector3.Zero;
        var nextPoint = startPoint;
        for (int i = 0; i < vertexCount; ++i)
        {
            Vector3.Add(ref *(Vector3*)nextPoint, ref center, out center);
            nextPoint += vertexStride;
        }

        //This is the center of our sphere.
        center /= (float)vertexCount;

        //Find the radius of the sphere
        float radius = 0f;
        nextPoint = startPoint;
        for (int i = 0; i < vertexCount; ++i)
        {
            //We are doing a relative distance comparasin to find the maximum distance
            //from the center of our sphere.
            Vector3.DistanceSquared(ref center, ref *(Vector3*)nextPoint, out var distance);

            if (distance > radius)
                radius = distance;
            nextPoint += vertexStride;
        }

        //Find the real distance from the DistanceSquared.
        radius = MathF.Sqrt(radius);

        //Construct the sphere.
        result.Center = center;
        result.Radius = radius;
    }

    /// <summary>
    /// Constructs a <see cref="BoundingSphere"/> that fully contains the given points.
    /// </summary>
    /// <param name="points">The points that will be contained by the sphere.</param>
    /// <returns>The newly constructed bounding sphere.</returns>
    public static BoundingSphere FromPoints(Vector3[] points)
    {
        FromPoints(points, out var result);
        return result;
    }

    /// <summary>
    /// Constructs a <see cref="BoundingSphere"/> from a given box.
    /// </summary>
    /// <param name="box">The box that will designate the extents of the sphere.</param>
    /// <param name="result">When the method completes, the newly constructed bounding sphere.</param>
    public static void FromBox(ref readonly BoundingBox box, out BoundingSphere result)
    {
        Vector3.Lerp(in box.Minimum, in box.Maximum, 0.5f, out result.Center);

        float x = box.Minimum.X - box.Maximum.X;
        float y = box.Minimum.Y - box.Maximum.Y;
        float z = box.Minimum.Z - box.Maximum.Z;

        float distance = MathF.Sqrt((x * x) + (y * y) + (z * z));
        result.Radius = distance * 0.5f;
    }

    /// <summary>
    /// Constructs a <see cref="BoundingSphere"/> from a given box.
    /// </summary>
    /// <param name="box">The box that will designate the extents of the sphere.</param>
    /// <returns>The newly constructed bounding sphere.</returns>
    public static BoundingSphere FromBox(BoundingBox box)
    {
        FromBox(ref box, out var result);
        return result;
    }

    /// <summary>
    /// Transforms a bounding sphere, yielding the bounding sphere of all points contained by the original one, transformed by the specified transform.
    /// </summary>
    /// <param name="value">The original bounding sphere.</param>
    /// <param name="transform">The transform to apply to the bounding sphere.</param>
    /// <param name="result">The transformed bounding sphere.</param>
    public static void Transform(ref readonly BoundingSphere value, ref readonly Matrix transform, out BoundingSphere result)
    {
        Vector3.TransformCoordinate(in value.Center, in transform, out result.Center);

        var majorAxisLengthSquared = MathF.Max(
            (transform.M11 * transform.M11) + (transform.M12 * transform.M12) + (transform.M13 * transform.M13), MathF.Max(
            (transform.M21 * transform.M21) + (transform.M22 * transform.M22) + (transform.M23 * transform.M23),
            (transform.M31 * transform.M31) + (transform.M32 * transform.M32) + (transform.M33 * transform.M33)));

        result.Radius = value.Radius * MathF.Sqrt(majorAxisLengthSquared);
    }

    /// <summary>
    /// Constructs a <see cref="BoundingSphere"/> that is the as large as the total combined area of the two specified spheres.
    /// </summary>
    /// <param name="value1">The first sphere to merge.</param>
    /// <param name="value2">The second sphere to merge.</param>
    /// <param name="result">When the method completes, contains the newly constructed bounding sphere.</param>
    public static void Merge(ref readonly BoundingSphere value1, ref readonly BoundingSphere value2, out BoundingSphere result)
    {
        // Pre-exit if one of the bounding sphere by assuming that a merge with an empty sphere is equivalent at taking the non-empty sphere
        if (value1 == Empty)
        {
            result = value2;
            return;
        }

        if (value2 == Empty)
        {
            result = value1;
            return;
        }

        Vector3 difference = value2.Center - value1.Center;

        float length = difference.Length();
        float radius = value1.Radius;
        float radius2 = value2.Radius;

        if (radius + radius2 >= length)
        {
            if (radius - radius2 >= length)
            {
                result = value1;
                return;
            }

            if (radius2 - radius >= length)
            {
                result = value2;
                return;
            }
        }

        Vector3 vector = difference * (1.0f / length);
        float min = MathF.Min(-radius, length - radius2);
        float max = (MathF.Max(radius, length + radius2) - min) * 0.5f;

        result.Center = value1.Center + (vector * (max + min));
        result.Radius = max;
    }

    /// <summary>
    /// Constructs a <see cref="BoundingSphere"/> that is the as large as the total combined area of the two specified spheres.
    /// </summary>
    /// <param name="value1">The first sphere to merge.</param>
    /// <param name="value2">The second sphere to merge.</param>
    /// <returns>The newly constructed bounding sphere.</returns>
    public static BoundingSphere Merge(BoundingSphere value1, BoundingSphere value2)
    {
        Merge(ref value1, ref value2, out var result);
        return result;
    }

    /// <summary>
    /// Tests for equality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(BoundingSphere left, BoundingSphere right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Tests for inequality between two objects.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(BoundingSphere left, BoundingSphere right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{this}";

    /// <summary>
    /// Returns a <see cref="string"/> that represents this instance.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string"/> that represents this instance.
    /// </returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        var handler = new DefaultInterpolatedStringHandler(15, 2, formatProvider);
        handler.AppendLiteral("Center:");
        handler.AppendFormatted(Center, format);
        handler.AppendLiteral(" Radius:");
        handler.AppendFormatted(Radius, format);
        return handler.ToStringAndClear();
    }

    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        var format1 = format.Length > 0 ? format.ToString() : null;
        var handler = new MemoryExtensions.TryWriteInterpolatedStringHandler(15, 2, destination, provider, out _);
        handler.AppendLiteral("Center:");
        handler.AppendFormatted(Center, format1);
        handler.AppendLiteral(" Radius:");
        handler.AppendFormatted(Radius, format1);
        return destination.TryWrite(ref handler, out charsWritten);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Center, Radius);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Stride.Core.Mathematics.Vector4"/> is equal to this instance.
    /// </summary>
    /// <param name="value">The <see cref="Stride.Core.Mathematics.Vector4"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="Stride.Core.Mathematics.Vector4"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public readonly bool Equals(BoundingSphere value)
    {
        return Center == value.Center && Radius == value.Radius;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to this instance.
    /// </summary>
    /// <param name="value">The <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override readonly bool Equals(object? value)
    {
        return value is BoundingSphere boundingSphere && Equals(boundingSphere);
    }

#if SlimDX1xInterop
    /// <summary>
    /// Performs an implicit conversion from <see cref="Stride.Core.Mathematics.BoundingSphere"/> to <see cref="SlimDX.BoundingSphere"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator SlimDX.BoundingSphere(BoundingSphere value)
    {
        return new SlimDX.BoundingSphere(value.Center, value.Radius);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="SlimDX.BoundingSphere"/> to <see cref="Stride.Core.Mathematics.BoundingSphere"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator BoundingSphere(SlimDX.BoundingSphere value)
    {
        return new BoundingSphere(value.Center, value.Radius);
    }
#endif

#if SlimDX1xInterop
    /// <summary>
    /// Performs an implicit conversion from <see cref="Stride.Core.Mathematics.BoundingSphere"/> to <see cref="Microsoft.Xna.Framework.BoundingSphere"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Microsoft.Xna.Framework.BoundingSphere(BoundingSphere value)
    {
        return new Microsoft.Xna.Framework.BoundingSphere(value.Center, value.Radius);
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Microsoft.Xna.Framework.BoundingSphere"/> to <see cref="Stride.Core.Mathematics.BoundingSphere"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator BoundingSphere(Microsoft.Xna.Framework.BoundingSphere value)
    {
        return new BoundingSphere(value.Center, value.Radius);
    }
#endif
}

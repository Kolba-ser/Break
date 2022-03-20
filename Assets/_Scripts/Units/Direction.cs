
using UnityEngine;

public sealed class Direction
{
    private Transform from;
    private Transform to;
    

    public Direction(Transform from, Transform to)
    {
        this.from = from;
        this.to = to;
    }

    /// <summary>
    /// Возвращает направление 
    /// </summary>
    /// <returns></returns>
    public Vector3 Get()
    {
        return to.position - from.position;
    }


    /// <summary>
    /// Возвращет нормализованное направление
    /// </summary>
    /// <returns></returns>
    public Vector3 GetNormalized()
    {
        return (to.position - from.position).normalized;
    }
}


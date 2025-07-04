using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class MathUtils
{
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * Mathf.Clamp01(t);
    }

    public static Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 q0 = Vector3.Lerp(p0, p1, t);
        Vector3 q1 = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(q0, q1, t);
    }
}



/// <summary>
/// 操作限制管理工具类，支持枚举类型的操作限制管理
/// </summary>
/// <typeparam name="TOpEnum">操作枚举类型</typeparam>
public class OperationLimiterUtil<TOpEnum> where TOpEnum : struct, Enum
{
    // 存储每个操作类型的剩余次数（null 表示无限制）
    private readonly Dictionary<TOpEnum, int?> _operationLimits = new Dictionary<TOpEnum, int?>();
    private readonly UnityAction<OperationLimiterUtil<TOpEnum>, TOpEnum> _event;

    public OperationLimiterUtil(UnityAction<OperationLimiterUtil<TOpEnum>, TOpEnum> evt, int? defaultLimit = null)
    {
        InitializeDefaults(defaultLimit);
        _event = evt;
    }


    /// <summary>
    /// 初始化限制规则
    /// </summary>
    /// <param name="defaultLimit">默认限制次数（null=无限制）</param>
    public void InitializeDefaults(int? defaultLimit = null)
    {
        foreach (TOpEnum op in Enum.GetValues(typeof(TOpEnum)))
        {
            _operationLimits[op] = defaultLimit;
            _event?.Invoke(this, op);
        }
    }

    /// <summary>
    /// 设置特定操作的次数限制
    /// </summary>
    /// <param name="operation">操作类型</param>
    /// <param name="limit">限制次数（null=无限制）</param>
    public void SetLimit(TOpEnum operation, int? limit)
    {
        _operationLimits[operation] = limit;
        _event?.Invoke(this, operation);
    }

    /// <summary>
    /// 增加操作限制次数（仅对有限制的操作有效）
    /// </summary>
    public bool IncreaseLimit(TOpEnum operation, int amount)
    {
        if (amount <= 0) return false;
        if (!_operationLimits.TryGetValue(operation, out var current) || current == null) return false;

        _operationLimits[operation] = current + amount;
        _event?.Invoke(this, operation);
        return true;
    }

    /// <summary>
    /// 减少操作限制次数（仅对有限制的操作有效）
    /// </summary>
    public bool DecreaseLimit(TOpEnum operation, int amount)
    {
        if (amount <= 0) return false;
        if (!_operationLimits.TryGetValue(operation, out var current) || current == null) return false;

        _operationLimits[operation] = Math.Max(0, current.Value - amount);
        _event?.Invoke(this, operation);
        return true;
    }

    /// <summary>
    /// 检查操作是否可用（剩余次数 > 0）
    /// </summary>
    /// <returns>true=可用，false=不可用或操作不存在</returns>
    public bool CheckValid(TOpEnum operation)
    {
        return GetRemainingCount(operation) > 0;
    }

    /// <summary>
    /// 尝试消耗一次操作次数
    /// </summary>
    /// <returns>true=消耗成功，false=不可用或操作不存在</returns>
    public bool TryUse(TOpEnum operation)
    {
        if (!_operationLimits.TryGetValue(operation, out var limit) || limit == null) return true;
        if (limit <= 0) return false;

        _operationLimits[operation] = limit - 1;
        _event?.Invoke(this, operation);
        return true;
    }

    /// <summary>
    /// 获取操作剩余次数
    /// </summary>
    /// <returns>-1=无限制，>=0=实际剩余次数</returns>
    public int GetRemainingCount(TOpEnum operation)
    {
        if (!_operationLimits.TryGetValue(operation, out var limit)) return -1;
        return limit ?? -1;
    }

    /// <summary>
    /// 重置所有操作次数为初始限制值（需提前通过SetLimit设置）
    /// </summary>
    //public void ResetAll()
    //{
    //    var keys = new List<TOpEnum>(_operationLimits.Keys);
    //    foreach (var op in keys)
    //    {
    //        // 重置时需要额外存储初始值（这里简化处理）
    //        // 实际项目可将初始值存储在另一个字典
    //        if (_operationLimits[op] != null)
    //        {
    //            // 实际应用需记录初始值，这里仅作演示
    //        }
    //    }
    //}
}
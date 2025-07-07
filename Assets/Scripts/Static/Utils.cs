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
/// �������ƹ������֧࣬��ö�����͵Ĳ������ƹ���
/// </summary>
/// <typeparam name="TOpEnum">����ö������</typeparam>
public class OperationLimiterUtil<TOpEnum> where TOpEnum : struct, Enum
{
    // �洢ÿ���������͵�ʣ�������null ��ʾ�����ƣ�
    private readonly Dictionary<TOpEnum, int?> _operationLimits = new Dictionary<TOpEnum, int?>();
    private readonly UnityAction<OperationLimiterUtil<TOpEnum>, TOpEnum> _event;

    public OperationLimiterUtil(UnityAction<OperationLimiterUtil<TOpEnum>, TOpEnum> evt, int? defaultLimit = null)
    {
        InitializeDefaults(defaultLimit);
        _event = evt;
    }


    /// <summary>
    /// ��ʼ�����ƹ���
    /// </summary>
    /// <param name="defaultLimit">Ĭ�����ƴ�����null=�����ƣ�</param>
    public void InitializeDefaults(int? defaultLimit = null)
    {
        foreach (TOpEnum op in Enum.GetValues(typeof(TOpEnum)))
        {
            _operationLimits[op] = defaultLimit;
            _event?.Invoke(this, op);
        }
    }

    /// <summary>
    /// �����ض������Ĵ�������
    /// </summary>
    /// <param name="operation">��������</param>
    /// <param name="limit">���ƴ�����null=�����ƣ�</param>
    public void SetLimit(TOpEnum operation, int? limit)
    {
        _operationLimits[operation] = limit;
        _event?.Invoke(this, operation);
    }

    /// <summary>
    /// ���Ӳ������ƴ��������������ƵĲ�����Ч��
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
    /// ���ٲ������ƴ��������������ƵĲ�����Ч��
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
    /// �������Ƿ���ã�ʣ����� > 0��
    /// </summary>
    /// <returns>true=���ã�false=�����û����������</returns>
    public bool CheckValid(TOpEnum operation)
    {
        return GetRemainingCount(operation) > 0;
    }

    /// <summary>
    /// ��������һ�β�������
    /// </summary>
    /// <returns>true=���ĳɹ���false=�����û����������</returns>
    public bool TryUse(TOpEnum operation)
    {
        if (!_operationLimits.TryGetValue(operation, out var limit) || limit == null) return true;
        if (limit <= 0) return false;

        _operationLimits[operation] = limit - 1;
        _event?.Invoke(this, operation);
        return true;
    }

    /// <summary>
    /// ��ȡ����ʣ�����
    /// </summary>
    /// <returns>-1=�����ƣ�>=0=ʵ��ʣ�����</returns>
    public int GetRemainingCount(TOpEnum operation)
    {
        if (!_operationLimits.TryGetValue(operation, out var limit)) return -1;
        return limit ?? -1;
    }

    /// <summary>
    /// �������в�������Ϊ��ʼ����ֵ������ǰͨ��SetLimit���ã�
    /// </summary>
    //public void ResetAll()
    //{
    //    var keys = new List<TOpEnum>(_operationLimits.Keys);
    //    foreach (var op in keys)
    //    {
    //        // ����ʱ��Ҫ����洢��ʼֵ������򻯴���
    //        // ʵ����Ŀ�ɽ���ʼֵ�洢����һ���ֵ�
    //        if (_operationLimits[op] != null)
    //        {
    //            // ʵ��Ӧ�����¼��ʼֵ�����������ʾ
    //        }
    //    }
    //}
}
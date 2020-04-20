using System;

namespace FindPath.Core
{
    /// 模式
    [Flags]
    public enum Model
    {
        Normal = 1,
        DoubleTarget = 2, // 双目标
        NoRepeat = 4, // 相邻的元素禁止重复 
        Disturb = 8, // 干扰，每次操作前会使当前值加1 
    }
}
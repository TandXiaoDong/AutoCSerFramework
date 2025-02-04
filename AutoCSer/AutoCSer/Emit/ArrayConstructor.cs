﻿using System;
#if NOJIT
using System.Reflection;
#else
using/**/System.Reflection.Emit;
#endif

namespace AutoCSer.Emit
{
    /// <summary>
    /// 集合构造函数
    /// </summary>
    /// <typeparam name="valueType">集合类型</typeparam>
    /// <typeparam name="argumentType">枚举值类型</typeparam>
    internal static class ArrayConstructor<valueType, argumentType>
    {
#if NOJIT
        /// <summary>
        /// 构造函数
        /// </summary>
        private static readonly ConstructorInfo constructor = typeof(valueType).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(argumentType).MakeArrayType() }, null);
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static valueType Constructor(argumentType[] value)
        {
            return (valueType)constructor.Invoke(new object[] { value });
        }
#else
        /// <summary>
        /// 构造函数
        /// </summary>
        public static readonly Func<argumentType[], valueType> Constructor = (Func<argumentType[], valueType>)Pub.CreateConstructor(typeof(valueType), typeof(argumentType).MakeArrayType());
#endif
    }
}

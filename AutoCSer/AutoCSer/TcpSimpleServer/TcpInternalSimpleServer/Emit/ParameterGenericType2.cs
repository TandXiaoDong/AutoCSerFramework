﻿using System;
using AutoCSer.Threading;
using System.Reflection;
using AutoCSer.Net.TcpServer;

namespace AutoCSer.Net.TcpInternalSimpleServer.Emit
{
    /// <summary>
    /// 输入+输出参数泛型类型元数据
    /// </summary>
    internal abstract partial class ParameterGenericType2 : AutoCSer.Net.TcpSimpleServer.Emit.ParameterGenericType2
    {
        /// <summary>
        /// 泛型类型元数据缓存
        /// </summary>
        private static readonly AutoCSer.Threading.LockEquatableLastDictionary<AutoCSer.Metadata.GenericType2.TypeKey, ParameterGenericType2> cache = new LockEquatableLastDictionary<AutoCSer.Metadata.GenericType2.TypeKey, ParameterGenericType2>();
        /// <summary>
        /// 创建泛型类型元数据
        /// </summary>
        /// <typeparam name="Type1"></typeparam>
        /// <typeparam name="Type2"></typeparam>
        /// <returns></returns>
        [AutoCSer.IOS.Preserve(Conditional = true)]
        private static ParameterGenericType2 create<Type1, Type2>()
            where Type1 : struct
            where Type2 : struct
        {
            return new ParameterGenericType2<Type1, Type2>();
        }
        /// <summary>
        /// 创建泛型类型元数据 函数信息
        /// </summary>
        private static readonly MethodInfo createMethod = typeof(ParameterGenericType2).GetMethod("create", BindingFlags.Static | BindingFlags.NonPublic);
        /// <summary>
        /// 获取泛型类型元数据
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static ParameterGenericType2 Get(Type type1, Type type2)
        {
            ParameterGenericType2 value;
            AutoCSer.Metadata.GenericType2.TypeKey typeKey = new AutoCSer.Metadata.GenericType2.TypeKey { Type1 = type1, Type2 = type2 };
            if (!cache.TryGetValue(ref typeKey, out value))
            {
                value = new UnionType { Value = createMethod.MakeGenericMethod(type1, type2).Invoke(null, null) }.ParameterGenericType2;
                cache.Set(ref typeKey, value);
            }
            return value;
        }
    }
    /// <summary>
    /// 泛型类型元数据
    /// </summary>
    /// <typeparam name="inputParameterType">泛型类型</typeparam>
    /// <typeparam name="outputParameterType">泛型类型</typeparam>
    internal sealed partial class ParameterGenericType2<inputParameterType, outputParameterType> : ParameterGenericType2
        where inputParameterType : struct
        where outputParameterType : struct
    {
        /// <summary>
        /// TCP调用并返回参数值
        /// </summary>
        internal delegate TcpServer.ReturnType ClientGet(TcpServer.CommandInfoBase commandInfo, ref inputParameterType inputParameter, ref outputParameterType outputParameter);
        /// <summary>
        /// TCP调用并返回参数值
        /// </summary>
        internal override MethodInfo ClientGetMethod
        {
            get { return ((ClientGet)ParameterGenericType.Client.Get<inputParameterType, outputParameterType>).Method; }
        }
    }
}

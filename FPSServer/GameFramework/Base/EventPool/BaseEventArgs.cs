﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using ProtoBuf;

namespace GameFramework
{
    /// <summary>
    /// 事件基类。
    /// </summary>
    [ProtoContract]
    public abstract class BaseEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 获取类型编号。
        /// </summary>
        public abstract int Id
        {
            get;
        }
    }
}

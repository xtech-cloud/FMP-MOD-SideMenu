
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.66.0.  DO NOT EDIT!
//*************************************************************************************

using System;
using System.IO;
using UnityEngine;

namespace XTC.FMP.MOD.SideMenu.LIB.Unity
{
    /// <summary>
    /// 内容读取器
    /// </summary>
    public class ContentReader
    {
        protected ObjectsPool contentObjectsPool_ { get; private set; }

        public ContentReader(ObjectsPool _contentObjectsPool)
        {
            contentObjectsPool_ = _contentObjectsPool;
        }

        /// <summary>
        /// 资产库的根目录的绝对路径
        /// </summary>
        public string AssetRootPath { get; set; }

        /// <summary>
        /// 内容的短路径，格式为 包名/内容名
        /// </summary>
        public string ContentUri { get; set; }

        /// <summary>
        /// 加载纹理
        /// </summary>
        /// <param name="_file">文件相对路径，相对于包含format.json的资源文件夹</param>
        public void LoadTexture(string _file, Action<Texture2D> _onFinish, Action _onError)
        {
            string dir = Path.Combine(AssetRootPath, ContentUri);
            string filefullpath = Path.Combine(dir, _file);
            contentObjectsPool_.LoadTexture(filefullpath, null, _onFinish, _onError);
        }

        /// <summary>
        /// 加载文本
        /// </summary>
        /// <param name="_file">文件相对路径，相对于包含format.json的资源文件夹</param>
        public void LoadText(string _file, Action<byte[]> _onFinish, Action _onError)
        {
            string dir = Path.Combine(AssetRootPath, ContentUri);
            string filefullpath = Path.Combine(dir, _file);
            contentObjectsPool_.LoadText(filefullpath, null, _onFinish, _onError);
        }
    }
}


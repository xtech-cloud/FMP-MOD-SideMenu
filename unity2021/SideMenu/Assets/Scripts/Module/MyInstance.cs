

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.SideMenu.LIB.Proto;
using XTC.FMP.MOD.SideMenu.LIB.MVCS;
using System.Linq;

namespace XTC.FMP.MOD.SideMenu.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        public class UiReference
        {
            public RectTransform menu;
            public Transform templateItem;
            public Transform templatePageSlot;
            public List<Toggle> itemS = new List<Toggle>();
            public RectTransform page;
        }

        private UiReference uiReference_ = new UiReference();


        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        /// <remarks>
        /// 可用于加载主题目录的数据
        /// </remarks>
        public void HandleCreated()
        {
            uiReference_.menu = rootUI.transform.Find("menu").GetComponent<RectTransform>();
            uiReference_.templateItem = rootUI.transform.Find("menu/Viewport/Content/itemTemplate");
            uiReference_.templateItem.gameObject.SetActive(false);
            uiReference_.templatePageSlot = rootUI.transform.Find("page/Viewport/slot");
            uiReference_.templatePageSlot.gameObject.SetActive(false);
            uiReference_.page = rootUI.transform.Find("page").GetComponent<RectTransform>();
            applyStyle();
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        /// <remarks>
        /// 可用于加载内容目录的数据
        /// </remarks>
        public void HandleOpened(string _source, string _uri)
        {
            rootUI.gameObject.SetActive(true);
            if (uiReference_.itemS.Count > 0)
                uiReference_.itemS.First().isOn = true;
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
        }

        private void applyStyle()
        {
            if (style_.position == "left")
            {
                alignByAncor(uiReference_.menu.transform, style_.menu.anchor);
            }
            var rtMenuViewport = uiReference_.menu.Find("Viewport").GetComponent<RectTransform>();
            rtMenuViewport.anchoredPosition = new Vector2(
                (style_.menu.viewport.padding.left - style_.menu.viewport.padding.right) / 2,
                -(style_.menu.viewport.padding.top - style_.menu.viewport.padding.bottom) / 2);
            rtMenuViewport.sizeDelta = new Vector2(
                -style_.menu.viewport.padding.left - style_.menu.viewport.padding.right,
                -style_.menu.viewport.padding.top - style_.menu.viewport.padding.bottom);
            loadTextureFromTheme(style_.menu.image, (_texture) =>
            {
                uiReference_.menu.GetComponent<RawImage>().texture = _texture;
            }, () => { });

            foreach (var item in style_.itemS)
            {
                var clone = GameObject.Instantiate(uiReference_.templateItem.gameObject, uiReference_.templateItem.parent);
                clone.SetActive(true);
                loadTextureFromTheme(item.image, (_texture) =>
                {
                    clone.transform.Find("Background").GetComponent<RawImage>().texture = _texture;
                }, () => { });
                alignByAncor(clone.transform, item.anchor);
                var imgCheckmark = clone.transform.Find("Checkmark").GetComponent<RawImage>();
                alignByAncor(imgCheckmark.transform, style_.cursor.anchor);
                loadTextureFromTheme(style_.cursor.image, (_texture) =>
                {
                    imgCheckmark.texture = _texture;
                }, () => { });
                var toggle = clone.GetComponent<Toggle>();
                uiReference_.itemS.Add(toggle);
                toggle.onValueChanged.AddListener((_toggled) =>
                {
                    if (_toggled)
                        publishSubjects(item.onSubjects);
                    else
                        publishSubjects(item.offSubjects);
                });
            }

            loadTextureFromTheme(style_.page.image, (_texture) =>
            {
                uiReference_.page.GetComponent<RawImage>().texture = _texture;
            }, () => { });

            var rtPageViewport = uiReference_.page.Find("Viewport").GetComponent<RectTransform>();
            rtPageViewport.anchoredPosition = new Vector2(
                (style_.page.viewport.padding.left - style_.page.viewport.padding.right) / 2,
                -(style_.page.viewport.padding.top - style_.page.viewport.padding.bottom) / 2);
            rtPageViewport.sizeDelta = new Vector2(
                -style_.page.viewport.padding.left - style_.page.viewport.padding.right,
                -style_.page.viewport.padding.top - style_.page.viewport.padding.bottom);
            rtPageViewport.localScale = Vector3.one * style_.page.viewport.scale;
        }

        protected void publishSubjects(MyConfig.Subject[] _subjects)
        {
            logger_.Debug("publish");
            foreach (var subject in _subjects)
            {
                var data = new Dictionary<string, object>();
                foreach (var parameter in subject.parameters)
                {
                    if (parameter.type.Equals("string"))
                        data[parameter.key] = parameter.value;
                    else if (parameter.type.Equals("int"))
                        data[parameter.key] = int.Parse(parameter.value);
                    else if (parameter.type.Equals("float"))
                        data[parameter.key] = float.Parse(parameter.value);
                    else if (parameter.type.Equals("bool"))
                        data[parameter.key] = bool.Parse(parameter.value);
                }
                (entry_ as MyEntry).getDummyModel().Publish(subject.message, data);
            }
        }

    }
}

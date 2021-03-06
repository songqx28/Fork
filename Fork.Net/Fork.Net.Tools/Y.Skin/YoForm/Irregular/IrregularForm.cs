﻿//************************************************************************
//      https://github.com/yuzhengyang
//      author:     yuzhengyang
//      date:       2017.4.27 - 2017.8.25
//      desc:       工具描述
//      Copyright (c) yuzhengyang. All rights reserved.
//************************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Y.Skin.YoForm.Irregular
{
    public partial class IrregularForm : Form
    {
        private IrregularFormSkin Skin;
        public  Point MouseLocation { get { return Skin.MouseLocation; } }
        public IrregularForm()
        {
            InitializeComponent();
            //SetStyles();//减少闪烁
            ShowInTaskbar = false;//禁止控件层显示到任务栏
            FormBorderStyle = FormBorderStyle.None;//设置无边框的窗口样式
            TransparencyKey = BackColor;//使控件层背景透明
        }
        private void IrregularForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Skin = new IrregularFormSkin(this);//创建皮肤层
                BackgroundImage = null;//去除控件层背景
                Skin.Show();//显示皮肤层

                //设置右键菜单
                if (ContextMenuStrip != null) Skin.ContextMenuStrip = ContextMenuStrip;
            }
        }

        #region 属性
        private bool _Moveable = true;
        [Category("Skin")]
        [Description("窗体是否可以移动")]
        [DefaultValue(typeof(bool), "true")]
        public bool Movable
        {
            get { return _Moveable; }
            set
            {
                if (_Moveable != value)
                {
                    _Moveable = value;
                }
            }
        }
        private bool _InTaskbar = true;
        [Category("Skin")]
        [Description("窗体是否显示到任务栏")]
        [DefaultValue(typeof(bool), "true")]
        public bool InTaskbar
        {
            get { return _InTaskbar; }
            set
            {
                if (_InTaskbar != value)
                {
                    _InTaskbar = value;
                }
            }
        }
        #endregion
        #region 减少闪烁
        private void SetStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion
        /// <summary>
        /// 窗体显示状态
        /// </summary>
        /// <param name="value"></param>
        public void Visibility(bool value)
        {
            if (value)
            {
                Show();
                Skin.Show();
            }
            else
            {
                Hide();
                Skin.Hide();
            }
        }
        public void SetBackground(Image img)
        {
            Skin.BackgroundImage = img;
            Skin.SetBits();
        }
    }
}

﻿#pragma checksum "..\..\..\..\ViewModel\Helpers\TimeControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7224B7C2DB11F94800C07597A4F7739CA7A6E2BC7CD8B4BE1445FC25AC59E244"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PTC_Management.ViewModel.Helpers {
    
    
    /// <summary>
    /// TimeControl
    /// </summary>
    public partial class TimeControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PTC_Management.ViewModel.Helpers.TimeControl UserControl;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid hour;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock mmTxt;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock sep1;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid min;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ddTxt;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock sep2;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid sec;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock yyTxt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PTC Management;component/viewmodel/helpers/timecontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserControl = ((PTC_Management.ViewModel.Helpers.TimeControl)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.hour = ((System.Windows.Controls.Grid)(target));
            
            #line 25 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
            this.hour.KeyDown += new System.Windows.Input.KeyEventHandler(this.Down);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mmTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.sep1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.min = ((System.Windows.Controls.Grid)(target));
            
            #line 41 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
            this.min.KeyDown += new System.Windows.Input.KeyEventHandler(this.Down);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ddTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.sep2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.sec = ((System.Windows.Controls.Grid)(target));
            
            #line 57 "..\..\..\..\ViewModel\Helpers\TimeControl.xaml"
            this.sec.KeyDown += new System.Windows.Input.KeyEventHandler(this.Down);
            
            #line default
            #line hidden
            return;
            case 10:
            this.yyTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

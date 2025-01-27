﻿#pragma checksum "..\..\..\NetworkingWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FF39311EDA0BC0CFC7B3809BDBF0530D9A731091"
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
using System.Windows.Controls.Ribbon;
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
using UserInterf;


namespace UserInterf {
    
    
    /// <summary>
    /// NetworkingWindow
    /// </summary>
    public partial class NetworkingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortNumberInput;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border IPInputCanvas;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IPInput;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConnectToHostButton;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ConnectedDevices;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel DynamicPanel;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ConnectedAsClientInfo;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ConnectedInfoField;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ConnectingButton;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OpenConnectionButton;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConnectButton;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\NetworkingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UserInterf;component/networkingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\NetworkingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PortNumberInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 37 "..\..\..\NetworkingWindow.xaml"
            this.PortNumberInput.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumericInput);
            
            #line default
            #line hidden
            return;
            case 2:
            this.IPInputCanvas = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.IPInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ConnectToHostButton = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\NetworkingWindow.xaml"
            this.ConnectToHostButton.Click += new System.Windows.RoutedEventHandler(this.ConnectToHostButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ConnectedDevices = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.DynamicPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            
            #line 82 "..\..\..\NetworkingWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Load_Image_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ConnectedAsClientInfo = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.ConnectedInfoField = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.ConnectingButton = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            this.OpenConnectionButton = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\NetworkingWindow.xaml"
            this.OpenConnectionButton.Click += new System.Windows.RoutedEventHandler(this.OpenConnectionButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ConnectButton = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\..\NetworkingWindow.xaml"
            this.ConnectButton.Click += new System.Windows.RoutedEventHandler(this.ConnectButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.BackButton = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\NetworkingWindow.xaml"
            this.BackButton.Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\MultyProcessingWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9DA25D729EFE06DC6FF007C9357B3D8D850F4CB6"
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
    /// MultyProcessingWindow
    /// </summary>
    public partial class MultyProcessingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\MultyProcessingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LoadedImage;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\MultyProcessingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonToLoadImg;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\MultyProcessingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadImageButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\MultyProcessingWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/UserInterf;component/multyprocessingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MultyProcessingWindow.xaml"
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
            this.LoadedImage = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.ButtonToLoadImg = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\MultyProcessingWindow.xaml"
            this.ButtonToLoadImg.Click += new System.Windows.RoutedEventHandler(this.LoadImage);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LoadImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\MultyProcessingWindow.xaml"
            this.LoadImageButton.Click += new System.Windows.RoutedEventHandler(this.CompletePocessings);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BackButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\MultyProcessingWindow.xaml"
            this.BackButton.Click += new System.Windows.RoutedEventHandler(this.BackToMainMenu);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


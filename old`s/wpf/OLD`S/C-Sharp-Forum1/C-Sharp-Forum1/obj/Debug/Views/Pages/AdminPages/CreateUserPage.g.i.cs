﻿#pragma checksum "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9B3D4BFA898E914C445B04D578983534FED335B02F3E8BCFCC6F675B8347FFE3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using C_Sharp_Forum1.Views.Pages.AdminPages;
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


namespace C_Sharp_Forum1.Views.Pages.AdminPages {
    
    
    /// <summary>
    /// CreateUserPage
    /// </summary>
    public partial class CreateUserPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbUsername;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbPassword;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonCancel;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonCreate;
        
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
            System.Uri resourceLocater = new System.Uri("/C-Sharp-Forum1;component/views/pages/adminpages/createuserpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
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
            this.txbUsername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txbPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.buttonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
            this.buttonCancel.Click += new System.Windows.RoutedEventHandler(this.buttonCancel_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonCreate = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\..\Views\Pages\AdminPages\CreateUserPage.xaml"
            this.buttonCreate.Click += new System.Windows.RoutedEventHandler(this.buttonCreate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

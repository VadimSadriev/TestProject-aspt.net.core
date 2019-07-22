#pragma checksum "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Admin\UsersPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34a792cb55a36bb27aeb5ecfb47e9e5aed9156b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_UsersPartial), @"mvc.1.0.view", @"/Views/Admin/UsersPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/UsersPartial.cshtml", typeof(AspNetCore.Views_Admin_UsersPartial))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\_ViewImports.cshtml"
using TestProject.WebServer;

#line default
#line hidden
#line 2 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\_ViewImports.cshtml"
using TestProject.WebServer.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34a792cb55a36bb27aeb5ecfb47e9e5aed9156b0", @"/Views/Admin/UsersPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23f8e6b36ab9396c17b9a962104d9a795f98e5ac", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_UsersPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2629, true);
            WriteLiteral(@"<div class=""row"">
    <div class=""col-md-12 crud-buttons-panel"">
        <div class=""btn-toolbar btn-group-sm"">
            <button type=""button"" ng-disabled=""isLoading"" class=""btn btn-outline-dark mr-2"" ng-click=""addNewUser()"">Новый пользователь</button>
            <button type=""button"" ng-disabled=""isloading || (!selectedUser || !selectedUser.isChanged)""
                    class=""btn btn-outline-dark mr-2"" ng-click=""updateUser()"">
                Сохранить пользователя
            </button>
            <button type=""button"" class=""btn btn-outline-dark mr-2"" ng-disabled=""isLoading || !selectedUser"" ng-click=""deleteUser()"">
                Удалить пользователя
            </button>
        </div>
    </div>
</div>

<div class=""row pt-2"">
    <div class=""col-sm-12 col-md-3 col-lg-3"">
        <ul class=""list-group list-group-flush item-list"">
            <li class=""list-group-item list-group-item-action"" ng-repeat=""user in users"" ng-click=""selectUser(user)"" ng-class=""{true: 'selected'}[user.");
            WriteLiteral(@"isSelected]"">
                <span ng-bind=""user.userName""></span> &nbsp; <span class=""badge badge-warning badge-pill"" ng-show=""!user.id"">Не сохранён</span>
            </li>
        </ul>
    </div>
    <div class=""col-sm-12 col-md-5 col-lg-5 item-info"" ng-if=""selectedUser.isSelected"">
        <!--Email-->
        <div class=""form-group w-100"">
            <label for=""email"">Почта</label>
            <input id=""email"" type=""text"" name=""email"" ng-disabled=""isLoading"" class=""form-control form-control-sm"" ng-class=""{true: 'shadow-error rounded'}[selectedUser.error.email]"" ng-model=""selectedUser.email"" ng-change=""selectedUser.isChanged = true; CheckEmail()"" role=""alert"" ng-required=""true"">
        </div>
        <!--Login-->
        <div class=""form-group w-100"">
            <label for=""login"">Логин</label>
            <input id=""login"" name=""login"" type=""text"" ng-disabled=""isLoading"" class=""form-control form-control-sm"" ng-class=""{true: 'shadow-error rounded'}[selectedUser.error.userName]"" ng-mod");
            WriteLiteral(@"el=""selectedUser.userName"" ng-change=""selectedUser.isChanged = true; checkUserName()"" ng-trim=""false"" ng-required=""true"">
        </div>
        <!--list of roles-->
        <div class=""form-group w-100"" ng-repeat=""role in selectedUser.customRoles"">
            <label class=""small-text"">
                <input class=""mat-checkbox"" type=""checkbox"" ng-disabled=""isLoading"" ng-model=""role.isSelected"" ng-change=""role.isChanged = true; selectedUser.isChanged = true"" />
                <span ng-bind=""role.name""></span>
            </label>
        </div>
    </div>
</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
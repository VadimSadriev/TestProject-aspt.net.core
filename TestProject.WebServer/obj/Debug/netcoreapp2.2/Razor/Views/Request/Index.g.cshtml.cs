#pragma checksum "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "03706acf6b3a16e5da8a7e19c0c46816a18e19ef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Request_Index), @"mvc.1.0.view", @"/Views/Request/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Request/Index.cshtml", typeof(AspNetCore.Views_Request_Index))]
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
#line 1 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
using TestProject.Core;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03706acf6b3a16e5da8a7e19c0c46816a18e19ef", @"/Views/Request/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23f8e6b36ab9396c17b9a962104d9a795f98e5ac", @"/Views/_ViewImports.cshtml")]
    public class Views_Request_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ng-repeat", new global::Microsoft.AspNetCore.Html.HtmlString("requestStatus in requestStatuses"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ng-value", new global::Microsoft.AspNetCore.Html.HtmlString("requestStatus.value"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("ng-bind", new global::Microsoft.AspNetCore.Html.HtmlString("requestStatus.displayName"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/directives/pageListDirective.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/request/request.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("include", "development", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/request/request.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("include", "production", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(105, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
  
    var isAdmin = User.IsInRole("Admin");
    var canApply = isAdmin || User.IsInRole("CanApplyRequest");
    var canEdit = isAdmin || User.IsInRole("CanEditRequest");

#line default
#line hidden
            BeginContext(285, 3745, true);
            WriteLiteral(@"
<div ng-app=""requestApp"" ng-controller=""requestCtrl"" ng-cloak>
    <div class=""row"">
        <div  ng-class=""{true: 'col-md-2'}[requestTypes && requestTypes.length > 0]"">
            <div ng-if=""requestTypes && requestTypes.length > 0"">
                <h5 class="""">Типы заявок</h5>
                <ul class=""list-unstyled"">
                    <li class="""" ng-repeat=""requestType in requestTypes"">
                        <label class=""small-text"" ng-click=""setFilterRequestType(requestType)"">
                            <input class=""mat-checkbox"" type=""checkbox"" ng-disabled=""isLoading"" ng-model=""requestType.isSelected"" />
                            <span ng-bind=""requestType.name""></span>
                        </label>
                    </li>
                </ul>
            </div>
        </div>
        <!-- request page-->
        <div ng-class=""{true: 'col-md-6', false: 'col-md-8'}[requestTypes && requestTypes.length > 0]"">
            <div class=""crud-buttons-panel btn-group-sm"">
");
            WriteLiteral(@"                <button class=""btn btn-outline-dark"" ng-click=""createRequestTemplate()"">Подать заявку</button>
            </div>
            <table class=""table table-hover table-sm mt-2"">
                <thead>
                    <tr>
                        <th scope=""col"">
                            Номер заявки
                            <input class=""form-control form-control-sm smallest-text"" ng-model=""grid.id"" ng-change=""onPropertyChanged()"" />
                        </th>
                        <th scope=""col"">
                            Тип заявки
                            <input class=""form-control form-control-sm smallest-text"" ng-model=""grid.requestType"" ng-change=""onPropertyChanged()"" />
                        </th>
                        <th scope=""col"" class=""align-top"">
                            Дата подачи
                        </th>
                        <th scope=""col"">
                            Заявитель
                            <input class=""form-c");
            WriteLiteral(@"ontrol form-control-sm smallest-text"" ng-model=""grid.creator"" ng-change=""onPropertyChanged()"" />
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class=""pointer"" ng-class=""{true: 'selected'}[request.isSelected]"" ng-click=""getRequest(request)"" ng-repeat=""request in requests"">
                        <td>
                            <span ng-bind=""request.id""></span>
                            <small ng-if=""request.displayStatus""><span ng-bind=""request.displayStatus""></span></small>
                        </td>
                        <td ng-bind=""request.type""></td>
                        <td ng-bind=""request.dateCreated""></td>
                        <td ng-bind=""request.creator""></td>
                    </tr>
                </tbody>
            </table>
            <pageList filter-Options=""filterOptions"" get-Items-Callback=""getRequests""></pageList>
        </div>
        <!--request info-->
        <div c");
            WriteLiteral(@"lass=""col-md-4"" ng-if=""request"">
            <div class=""card request-card"">
                <!--card header-->
                <h5 class=""card-header"">
                    <div class=""row"">
                        <div class=""col-md-6"">
                            <div ng-if=""request.id"">
                                Заявка номер <span ng-bind=""request.id""></span>
                            </div>
                            <div ng-if=""!request.id"">
                                Новая заявка
                            </div>
                            <small class="""" ng-bind=""request.requestType.name""></small>
                        </div>
");
            EndContext();
#line 80 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
                         if (canEdit)
                        {

#line default
#line hidden
            BeginContext(4096, 304, true);
            WriteLiteral(@"                            <div class=""col-md-6 col-sm-6"">
                                <small>Статус заявки</small>
                                <select class=""custom-select custom-select-sm"" ng-change=""request.isChanged = true;"" ng-model=""request.status"">
                                    ");
            EndContext();
            BeginContext(4400, 129, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03706acf6b3a16e5da8a7e19c0c46816a18e19ef11644", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4529, 81, true);
            WriteLiteral("\r\n                                </select>\r\n                            </div>\r\n");
            EndContext();
#line 88 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(4637, 5775, true);
            WriteLiteral(@"                    </div>
                    <button type=""button"" class=""close request-close"" aria-label=""Close"" ng-click=""closeRequest()"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </h5>
                <!--card body-->
                <div class=""card-body overflow-auto"">
                    <div class=""form-group w-50"">
                        <label for=""typeSelect"">Тип заявки</label>
                        <select id=""typeSelect"" class=""custom-select custom-select-sm"" ng-change=""getRequestFieldsData()""
                                ng-options=""requestType.name for requestType in requestTypes track by requestType.id""
                                ng-model=""request.requestType""></select>
                    </div>

                    <div class=""form-group w-50"" ng-repeat=""typeField in request.requestType.requestTypeFields"">
                        <label class=""small-text"" for="""" ng-bind=""typeField.name""></label>
");
            WriteLiteral(@"                        <div ng-switch=""typeField.type"">
                            <div ng-switch-when=""Number"">
                                <input class=""form-control form-control-sm"" ng-model=""typeField.requestValue.intValue"" />
                            </div>
                            <div ng-switch-when=""String"">
                                <input class=""form-control form-control-sm"" ng-model=""typeField.requestValue.stringValue"" />
                            </div>
                            <div ng-switch-when=""Date"">
                                <datepicker date-format=""dd.MM.yyyy"" selector=""form-control"">
                                    <div class=""input-group"">
                                        <input id=""typeField{{typeField.id}}"" class=""form-control form-control-sm"" placeholder=""Выберите дату""
                                               ng-model=""typeField.requestValue.dateValue"" />
                                        <button type=""button"" class=""btn ");
            WriteLiteral(@"btn-sm btn-secondary"">
                                            <i class=""fa-svg-icon"">
                                                <svg width=""15"" height=""15"" viewBox=""0 0 1792 1792"" xmlns=""http://www.w3.org/2000/svg""><path d=""M192 1664h288v-288h-288v288zm352 0h320v-288h-320v288zm-352-352h288v-320h-288v320zm352 0h320v-320h-320v320zm-352-384h288v-288h-288v288zm736 736h320v-288h-320v288zm-384-736h320v-288h-320v288zm768 736h288v-288h-288v288zm-384-352h320v-320h-320v320zm-352-864v-288q0-13-9.5-22.5t-22.5-9.5h-64q-13 0-22.5 9.5t-9.5 22.5v288q0 13 9.5 22.5t22.5 9.5h64q13 0 22.5-9.5t9.5-22.5zm736 864h288v-320h-288v320zm-384-384h320v-288h-320v288zm384 0h288v-288h-288v288zm32-480v-288q0-13-9.5-22.5t-22.5-9.5h-64q-13 0-22.5 9.5t-9.5 22.5v288q0 13 9.5 22.5t22.5 9.5h64q13 0 22.5-9.5t9.5-22.5zm384-64v1280q0 52-38 90t-90 38h-1408q-52 0-90-38t-38-90v-1280q0-52 38-90t90-38h128v-96q0-66 47-113t113-47h64q66 0 113 47t47 113v96h384v-96q0-66 47-113t113-47h64q66 0 113 47t47 113v96h128q52 0 90 38t38 90z"" /></svg>
       ");
            WriteLiteral(@"                                     </i>
                                        </button>
                                    </div>
                                </datepicker>
                            </div>
                            <div ng-switch-when=""File"">
                                <div class=""input-group small-input mb-1"">
                                    <div class=""custom-file"">
                                        <input type=""file"" class=""custom-file-input"" id=""inputGroupFile"" ng-model=""typeField.file"" ng-change=""uploadFile(typeField)"" ngf-select
                                               ngf-max-size=""1000MB"" ng-disabled=""isLoading || typeField.isFileLoading"" />
                                        <label class=""custom-file-label textWrapper"" for=""inputGroupFile"">Выберите файл</label>
                                    </div>
                                </div>
                                <div ng-bind=""typeField.requestValue.fileName""></div>
      ");
            WriteLiteral(@"                          <div class=""progress"" ng-if=""typeField.isFileLoading"">
                                    <div class=""progress-bar progress-bar-striped progress-bar-animated"" role=""progressbar"" ng-style=""{'width': typeField.fileLoadProgress + '%'}""><span ng-bind=""typeField.fileLoadProgress + '%'""></span></div>
                                </div>
                            </div>
                            <div ng-switch-when=""Time"">
                                <div class=""input-group""
                                     moment-picker=""typeField.requestValue.timeValue""
                                     locale=""ru""
                                     format=""HH:mm:ss"">
                                    <div class=""input-group-prepend input-group-sm"">
                                        <span class=""input-group-text"">
                                            <i class=""far fa-clock""></i>
                                        </span>
                                ");
            WriteLiteral(@"    </div>
                                    <input class=""form-control form-control-sm""
                                           placeholder=""Выберите или введите время""
                                           ng-model=""typeField.requestValue.timeValue""
                                           ng-model-options=""{updateOn: 'blur'}"">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--card footer-->
                <div class=""card-footer"">
                    <div class=""crud-buttons-panel btn-group-sm"">
");
            EndContext();
#line 160 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
                         if (canApply)
                        {

#line default
#line hidden
            BeginContext(10479, 129, true);
            WriteLiteral("                            <button ng-if=\"!request.id\" class=\"btn btn-outline-dark\" ng-click=\"addRequest()\">Отправить</button>\r\n");
            EndContext();
#line 163 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(10635, 24, true);
            WriteLiteral("                        ");
            EndContext();
#line 164 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
                         if (canEdit)
                        {

#line default
#line hidden
            BeginContext(10701, 112, true);
            WriteLiteral("                            <button class=\"btn btn-outline-dark\" ng-click=\"updateRequest()\">Сохранить</button>\r\n");
            EndContext();
#line 167 "C:\Users\rogue-one\Desktop\TestProject\TestProject.WebServer\Views\Request\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(10840, 108, true);
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            BeginContext(10948, 170, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03706acf6b3a16e5da8a7e19c0c46816a18e19ef20907", async() => {
                BeginContext(10983, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(10989, 60, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03706acf6b3a16e5da8a7e19c0c46816a18e19ef21304", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(11049, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(11055, 47, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03706acf6b3a16e5da8a7e19c0c46816a18e19ef22562", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(11102, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Include = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(11118, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(11120, 107, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03706acf6b3a16e5da8a7e19c0c46816a18e19ef24829", async() => {
                BeginContext(11154, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(11160, 51, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "03706acf6b3a16e5da8a7e19c0c46816a18e19ef25226", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(11211, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Include = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AppUser> UserManager { get; private set; }
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

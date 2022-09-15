#pragma checksum "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79855bfd9b5243e1af44f782222927b6d263aee2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_User_SearchUser), @"mvc.1.0.view", @"/Areas/Admin/Views/User/SearchUser.cshtml")]
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
#nullable restore
#line 1 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\_ViewImports.cshtml"
using CarRentService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\_ViewImports.cshtml"
using DataModel.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\_ViewImports.cshtml"
using DataModel.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79855bfd9b5243e1af44f782222927b6d263aee2", @"/Areas/Admin/Views/User/SearchUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51a49b6e7428a1ff6e7203629da5bbabc7560e4e", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_User_SearchUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SearchUser>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
  
    ViewData["Title"] = "Search User";
    Layout = "~/Areas/Admin/Views/Shared/_ServersideLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""content-header"">
    <div class=""container-fluid"">
        <div class=""row mb-2"">
            <div class=""col-sm-6"">
                <h5 class=""m-0 text-dark"">Search User</h5>
            </div><!-- /.col -->
            <div class=""col-sm-6"">
                <ol class=""breadcrumb float-sm-right"">
                    <li class=""breadcrumb-item""><a href=""#"">Dashboard</a></li>
                    <li class=""breadcrumb-item active"">Search User</li>
                </ol>
            </div><!-- /.col -->
        </div><!-- /.row -->
    </div><!-- /.container-fluid -->
</div>

<div style=""margin:auto;width:600px"">
");
#nullable restore
#line 24 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
     using (Html.BeginForm("SearchUser", "User", new { area = "Admin" }, FormMethod.Get))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"form-group col-auto\">\r\n            <a class=\"tn btn-info btn-sm\"");
            BeginWriteAttribute("href", " href=\"", 989, "\"", 1048, 1);
#nullable restore
#line 28 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
WriteAttributeValue("", 996, Url.Action("AllUsers", "User", new { area="Admin"}), 996, 52, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <i class=\"fa fa-users\"></i> All Users\r\n            </a>\r\n        </div>\r\n        <div class=\"form-group col-8\">\r\n            ");
#nullable restore
#line 33 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
       Write(Html.TextBoxFor(model => model.data, new { @placeholder = "Seach for user here ...", @class = "form-control form-control-sm" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"form-group col-auto\">\r\n            <button type=\"submit\" class=\"btn btn-warning btn-sm\">Search <i class=\"fa fa-search\"></i></button>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 39 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>

<div class=""row"" style=""margin-top:30px"">
    <div class=""col-12"">
        <div class=""card"">
            <div class=""card-header"">
                <h3 class=""card-title"">Super Admin List</h3>
            </div>
            <!-- /.card-header -->
            <div class=""card-body table-responsive p-0"" style=""height: 300px;"">
                <table class=""table table-head-fixed text-nowrap"">
                    <thead>
                        <tr>
                            <th>FIRST NAME</th>
                            <th>LAST NAME</th>
                            <th>E-Mail</th>
                            <th>STATUS</th>
                            <th>USER TYPE</th>
                            <th>CREATED AT</th>
                            <th>ACTIONS</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 63 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                         if (ViewBag.SearchResult.Count<1)
                        {



#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td style=\"color:red;\">No record found!</td>\r\n                            </tr>\r\n");
#nullable restore
#line 70 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                        }
                        else
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                             foreach (var user in ViewBag.SearchResult)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 77 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                    Write(user.FirstName != null ? user.FirstName:"Name");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 80 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                    Write(user.LastName != null ? user.LastName:"Surname");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n                                    <td>");
#nullable restore
#line 82 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                   Write(user.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>\r\n");
#nullable restore
#line 84 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                         if (user.AccessFailedCount != 1)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span style=\"background-color: forestgreen; color: white; padding: 0 5px 0 5px; border-radius: 5px; font-size: 13px\">Active</span>\r\n");
#nullable restore
#line 87 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span style=\"background-color: red; color: white; padding: 5px; border-radius: 5px; font-size: 13px\">Dectivated</span>");
#nullable restore
#line 89 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                                                                                                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </td>\r\n                                    <td>\r\n");
#nullable restore
#line 92 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                         foreach(var role in user.ApplicationUserRole)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <mark>");
#nullable restore
#line 94 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                             Write(role.ApplicationRole);

#line default
#line hidden
#nullable disable
            WriteLiteral("</mark>\r\n");
#nullable restore
#line 95 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </td>\r\n                                    <td>");
#nullable restore
#line 97 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                   Write(user.CreatedAt);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "79855bfd9b5243e1af44f782222927b6d263aee213411", async() => {
                WriteLiteral("\r\n                                            <i class=\"fa fa-edit text-white\"></i>\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 99 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                                                                                                          WriteLiteral(user.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                        <i onclick=\"deleteAction(this)\" class=\"btn btn-danger btn-sm\"");
            BeginWriteAttribute("delete-data", " delete-data=\"", 4757, "\"", 4779, 1);
#nullable restore
#line 102 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
WriteAttributeValue("", 4771, user.Id, 4771, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <i class=\"fa fa-trash text-white\"></i>\r\n                                        </i>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 107 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 107 "F:\VisualStudioProjects\CarRentService\CarRentService\Areas\Admin\Views\User\SearchUser.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </tbody>\r\n                </table>\r\n            </div>\r\n            <!-- /.card-body -->\r\n        </div>\r\n        <!-- /.card -->\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SearchUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
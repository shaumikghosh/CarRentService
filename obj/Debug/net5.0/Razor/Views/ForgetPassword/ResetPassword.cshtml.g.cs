#pragma checksum "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00b8b59994c15e2c2477f6b4ee42e99bd4c431f1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ForgetPassword_ResetPassword), @"mvc.1.0.view", @"/Views/ForgetPassword/ResetPassword.cshtml")]
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
#line 1 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\_ViewImports.cshtml"
using CarRentService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\_ViewImports.cshtml"
using DataModel.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\_ViewImports.cshtml"
using DataModel.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00b8b59994c15e2c2477f6b4ee42e99bd4c431f1", @"/Views/ForgetPassword/ResetPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51a49b6e7428a1ff6e7203629da5bbabc7560e4e", @"/Views/_ViewImports.cshtml")]
    public class Views_ForgetPassword_ResetPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChangeForgotPassword>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
  
    ViewData["Title"] = "Reset Password";
    Layout = "~/Views/Shared/_WebLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 8 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
 if (ViewBag.TokenError != true) {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div style=""background: url('https://mcarsstatic.cachefly.net/img/social/facebook/Mitsubishi-suv-crossovers-fb.jpg') no-repeat; background-size: cover"">
        <div class=""login-area"" style=""width:500px;margin:auto;padding:150px 0 150px 0"">
            <div class=""card"">
                <div class=""card-header"">
                    <h4 class=""text-center font-weight-bold"">Reset Your Password Now</h4>
                </div>
                <div class=""card-body"">
");
#nullable restore
#line 16 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                     using (Html.BeginForm("ResetPassword", "ForgetPassword", null, FormMethod.Post)) {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                   Write(Html.ValidationSummary(true));

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div>\r\n                            <i class=\"fas fa-envelope\"></i> ");
#nullable restore
#line 19 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                                                       Write(Html.LabelFor(model => model.Password, new { @class = "form-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            ");
#nullable restore
#line 20 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                       Write(Html.PasswordFor(model => model.Password, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            ");
#nullable restore
#line 21 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                       Write(Html.ValidationMessageFor(model => model.Password, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                        <div class=\"mt-3\">\r\n                            <i class=\"fas fa-lock\"></i> ");
#nullable restore
#line 24 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                                                   Write(Html.LabelFor(model => model.ConfirmPassword, new { @class = "form-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            ");
#nullable restore
#line 25 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                       Write(Html.PasswordFor(model => model.ConfirmPassword, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            ");
#nullable restore
#line 26 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                       Write(Html.ValidationMessageFor(model => model.ConfirmPassword, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                        <button type=\"submit\" class=\"btn btn-info btn-block mt-3\"><i class=\"fas fa-exchange-alt\"></i> Submit</button>\r\n");
#nullable restore
#line 29 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 34 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"
} else {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 1920, "\"", 1928, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n        <h1 class=\"text-danger text-center\"><i class=\"fas fa-skull-crossbones\"></i>Token is expired or invalid!</h1>\r\n    </div>\r\n");
#nullable restore
#line 38 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\ForgetPassword\ResetPassword.cshtml"

}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChangeForgotPassword> Html { get; private set; }
    }
}
#pragma warning restore 1591

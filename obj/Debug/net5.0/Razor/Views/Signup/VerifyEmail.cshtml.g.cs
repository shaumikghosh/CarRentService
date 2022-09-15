#pragma checksum "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8850fd131b70f8964e26ad51c93e0ef503d4970c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Signup_VerifyEmail), @"mvc.1.0.view", @"/Views/Signup/VerifyEmail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8850fd131b70f8964e26ad51c93e0ef503d4970c", @"/Views/Signup/VerifyEmail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51a49b6e7428a1ff6e7203629da5bbabc7560e4e", @"/Views/_ViewImports.cshtml")]
    public class Views_Signup_VerifyEmail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
  
    ViewData["Title"] = "Verify Email";
    Layout = "~/Views/Shared/_WebLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section id=\"about\">\r\n    <div class=\"container\">\r\n");
#nullable restore
#line 9 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
         if (TempData["EmailVerified"] != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h1 class=\"text-success text-center\"><i class=\"far fa-check-circle\"></i> ");
#nullable restore
#line 11 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
                                                                                Write(TempData["EmailVerified"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h4 class=\"text-info text-center d-block\">Going back in <span id=\"result\">5</span> seconds</h4>\r\n");
#nullable restore
#line 13 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
         if (TempData["TokenError"] != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h1 class=\"text-danger text-center\"><i class=\"fas fa-skull-crossbones\"></i> ");
#nullable restore
#line 16 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
                                                                                   Write(TempData["TokenError"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h4 class=\"text-danger text-center d-block\">Going back in <span id=\"result\">5</span> seconds</h4>\r\n");
#nullable restore
#line 18 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
         if (TempData["TokenExpired"] != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h1 class=\"text-danger text-center\"><i class=\"fas fa-skull-crossbones\"></i> ");
#nullable restore
#line 21 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
                                                                                   Write(TempData["TokenExpired"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n            <h4 class=\"text-danger text-center d-block\">Going back in <span id=\"result\">5</span> seconds</h4>\r\n");
#nullable restore
#line 23 "F:\VisualStudioProjects\CarRentService\CarRentService\Views\Signup\VerifyEmail.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</section><!-- #about -->\r\n\r\n<script>\r\n    setTimeout(function () {\r\n        window.location = \"/user/profile\";\r\n    }, 5000);\r\n</script>");
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
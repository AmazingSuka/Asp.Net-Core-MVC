#pragma checksum "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bfc91e9308bec6653d35a74a3ab742d3ba6b8d79"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Message_Detail), @"mvc.1.0.view", @"/Views/Message/Detail.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Message/Detail.cshtml", typeof(AspNetCore.Views_Message_Detail))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bfc91e9308bec6653d35a74a3ab742d3ba6b8d79", @"/Views/Message/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Views/_ViewImports.cshtml")]
    public class Views_Message_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CoreBB.Web.Models.Message>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(47, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml"
  
    ViewBag.Title = $"Message";

#line default
#line hidden
            BeginContext(89, 96, true);
            WriteLiteral("\r\n<div class=\"container\" style=\"margin-top:120px\">\r\n    <table class=\"table table-borderless\">\r\n");
            EndContext();
#line 9 "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml"
         foreach (var m in Model)
        {
            if (m.FromUser.Name == User.Identity.Name)
            {

#line default
#line hidden
            BeginContext(302, 123, true);
            WriteLiteral("                <tr>\r\n                    <td style=\"width:500px\"></td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(426, 44, false);
#line 16 "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml"
                   Write(await Html.PartialAsync("_MessageLayout", m));

#line default
#line hidden
            EndContext();
            BeginContext(470, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 19 "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml"
            }
            else
            {

#line default
#line hidden
            BeginContext(570, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(643, 44, false);
#line 24 "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml"
                   Write(await Html.PartialAsync("_MessageLayout", m));

#line default
#line hidden
            EndContext();
            BeginContext(687, 103, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"width:500px\"></td>\r\n                </tr>\r\n");
            EndContext();
#line 28 "C:\Users\dulck\source\repos\Asp.Net Core MVC\CoreBB.Web\CoreBB.Web\Views\Message\Detail.cshtml"
            }
        }

#line default
#line hidden
            BeginContext(816, 20, true);
            WriteLiteral("    </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CoreBB.Web.Models.Message>> Html { get; private set; }
    }
}
#pragma warning restore 1591

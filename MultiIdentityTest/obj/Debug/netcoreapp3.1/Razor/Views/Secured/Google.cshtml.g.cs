#pragma checksum "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\Secured\Google.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ff1b3c723e279347aa889a681d12e03c6ab1cf8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Secured_Google), @"mvc.1.0.view", @"/Views/Secured/Google.cshtml")]
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
#line 1 "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\_ViewImports.cshtml"
using MultiIdentityTest;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ff1b3c723e279347aa889a681d12e03c6ab1cf8", @"/Views/Secured/Google.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd530241716bf9ff8be1c3a02a1ee40ff6979bf4", @"/Views/_ViewImports.cshtml")]
    public class Views_Secured_Google : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h1>Simply secured</h1>\r\n<h2>Welcome ");
#nullable restore
#line 2 "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\Secured\Google.cshtml"
       Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<ul>\r\n");
#nullable restore
#line 4 "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\Secured\Google.cshtml"
     foreach (var c in User.Claims)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>");
#nullable restore
#line 6 "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\Secured\Google.cshtml"
       Write(c.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 6 "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\Secured\Google.cshtml"
                Write(c.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 7 "C:\Users\Giuseppe.Daucelli\source\repos\MultiIdentityTest\MultiIdentityTest\Views\Secured\Google.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>");
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
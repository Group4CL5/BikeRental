#pragma checksum "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "757d96fa12cd1bc6decf22f2ee3db656a23ba263"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_CartDisplay), @"mvc.1.0.view", @"/Views/Users/CartDisplay.cshtml")]
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
#line 1 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\_ViewImports.cshtml"
using BikeRental.MVCUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\_ViewImports.cshtml"
using BikeRental.MVCUI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"757d96fa12cd1bc6decf22f2ee3db656a23ba263", @"/Views/Users/CartDisplay.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87f01640b69ca4f5e46a8a00b71998dd00fd021", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_CartDisplay : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BikeRental.Models.Cart>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n    <div>\r\n        <h4>Total Count: ");
#nullable restore
#line 4 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml"
                     Write(Model.Accessories.Count + Model.Bicycles.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n");
#nullable restore
#line 5 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml"
          
            foreach (var item in Model.Bicycles)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>\r\n                    <b>");
#nullable restore
#line 9 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml"
                  Write(item.Brand);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                </p>\r\n");
#nullable restore
#line 11 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml"
            }
            foreach (var item in Model.Accessories)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>\r\n                    <b>");
#nullable restore
#line 15 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml"
                  Write(item.AccessoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                </p>\r\n");
#nullable restore
#line 17 "D:\(4) Studios\BikeRental\BikeRental.MVCUI\Views\Users\CartDisplay.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("        <button type=\"submit\" class=\"btn btn-primary\">Place Order</button>\r\n    </div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BikeRental.Models.Cart> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "92e6f9273090440216f6673e60e04a6be50e6d05"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Media_ShowMedia), @"mvc.1.0.view", @"/Views/Media/ShowMedia.cshtml")]
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
#line 1 "E:\EFcore\EFcoreMVC\Views\_ViewImports.cshtml"
using EFcoreMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\EFcore\EFcoreMVC\Views\_ViewImports.cshtml"
using EFcoreMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"92e6f9273090440216f6673e60e04a6be50e6d05", @"/Views/Media/ShowMedia.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba6869af36159c4679a7311423a2f817a13ce788", @"/Views/_ViewImports.cshtml")]
    public class Views_Media_ShowMedia : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
.grid-container {
  display: grid;
  grid-template-columns: auto auto auto;
}
.grid-item {
  background-color: rgba(255, 255, 255, 0.8);
  padding: 20px;
  text-align: center;
}
</style>
<p><b><a class=""btn btn-edit btn-primary"" href=""/Media/AddMedia"">Add Media</a></b></p>
<section id=""about"" class=""wow fadeInUp"">
    <div class=""container"">
        <div class=""grid-container"">
");
#nullable restore
#line 19 "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml"
              
                foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"grid-item\">\r\n                        <video width=\"300\" height=\"200\" controls>\r\n                            <source");
            BeginWriteAttribute("src", " src=\"", 685, "\"", 753, 2);
            WriteAttributeValue("", 691, "https://localhost:5001/Videos/", 691, 30, true);
#nullable restore
#line 24 "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml"
WriteAttributeValue("", 721, Url.Content(item.MediaPostPath), 721, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"mediaVideo\">\r\n                        </video>\r\n                        <label>");
#nullable restore
#line 26 "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml"
                          Write(item.MediaTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>&nbsp;\r\n                        <label>");
#nullable restore
#line 27 "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml"
                          Write(item.PublishDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                    </div>\r\n");
#nullable restore
#line 29 "E:\EFcore\EFcoreMVC\Views\Media\ShowMedia.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    </div>\r\n</section>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "92e6f9273090440216f6673e60e04a6be50e6d055644", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
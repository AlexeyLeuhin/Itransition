#pragma checksum "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8d48cad83675bc36aa11001a77b328b673189d74"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8d48cad83675bc36aa11001a77b328b673189d74", @"/Pages/Index.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/star-rating/star-rating.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/tag-cloud/jQWCloudv3.4.1.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/index.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 5 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
  
    Layout = "../Views/Shared/_Layout.cshtml";
    Html.AntiForgeryToken();

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<div class=\"container\">\r\n    <h2 style=\"margin-bottom: 40px;\">");
#nullable restore
#line 13 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                Write(localizer["LatestCreatedTales"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    <div class=\"card-columns\">\r\n");
#nullable restore
#line 15 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
         foreach (var tale in Model.DateSortedTales)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card text-white bg-dark current-tale\" style=\"width: 18rem;\">\r\n                <div class=\"card-body\">\r\n                    <h3 class=\"card-title text-center text-capitalize\">");
#nullable restore
#line 19 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                                  Write(tale.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                    <h5 class=\"text-left\" style=\"text-decoration: underline;\">");
#nullable restore
#line 20 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                                         Write(tale.Ganre.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <p class=\"card-text\">");
#nullable restore
#line 21 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                    Write(tale.ShortDiscription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 867, "\"", 902, 2);
            WriteAttributeValue("", 874, "/TaleDetails?taleId=", 874, 20, true);
#nullable restore
#line 22 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 894, tale.Id, 894, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-primary"">Read</a>
                </div>
                <div class=""card-footer text-muted"">
                    <div class=""text-left"">
                        <div class=""row"" style=""margin-left: 0px;"">
                            <input");
            BeginWriteAttribute("id", " id=\"", 1164, "\"", 1188, 2);
            WriteAttributeValue("", 1169, "input-date-", 1169, 11, true);
#nullable restore
#line 27 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 1180, tale.Id, 1180, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"text\" class=\"rating\"");
            BeginWriteAttribute("value", " value=\"", 1216, "\"", 1243, 1);
#nullable restore
#line 27 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 1224, tale.AverageRating, 1224, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-size=\"xs\" data-show-clear=\"false\" data-show-caption=\"false\">\r\n                            <span style=\"margin-left:10px;\">");
#nullable restore
#line 28 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                       Write(tale.NumberOfRatings);

#line default
#line hidden
#nullable disable
            WriteLiteral(" votes</span>\r\n                        </div>\r\n                        ");
#nullable restore
#line 30 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                   Write(tale.CreationTime.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                    <p>\r\n");
#nullable restore
#line 33 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                         foreach (var tag in tale.Tags)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 1671, "\"", 1729, 2);
            WriteAttributeValue("", 1678, "/SearchResult?handler=SearchByTag&tagName=", 1678, 42, true);
#nullable restore
#line 35 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 1720, tag.Name, 1720, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-link\">#");
#nullable restore
#line 35 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                                                                        Write(tag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 36 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </p>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 40 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n    <h2 style=\"margin-top:80px; margin-bottom:40px;\">");
#nullable restore
#line 44 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                Write(localizer["TopRatedTales"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    <div class=\"card-columns\">\r\n");
#nullable restore
#line 46 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
         foreach (var tale in Model.RatingSortedTales)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card text-white bg-dark current-tale\" style=\"width: 18rem;\">\r\n                <div class=\"card-body\">\r\n                    <h3 class=\"card-title text-center text-capitalize\">");
#nullable restore
#line 50 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                                  Write(tale.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                    <h5 class=\"text-left\" style=\"text-decoration: underline;\">");
#nullable restore
#line 51 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                                         Write(tale.Ganre.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <p class=\"card-text\">");
#nullable restore
#line 52 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                    Write(tale.ShortDiscription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 2488, "\"", 2523, 2);
            WriteAttributeValue("", 2495, "/TaleDetails?taleId=", 2495, 20, true);
#nullable restore
#line 53 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 2515, tale.Id, 2515, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-primary"">Read</a>
                </div>
                <div class=""card-footer text-muted"">
                    <div class=""text-left"">
                        <div class=""row"" style=""margin-left: 0px;"">
                            <input");
            BeginWriteAttribute("id", " id=\"", 2785, "\"", 2811, 2);
            WriteAttributeValue("", 2790, "input-rating-", 2790, 13, true);
#nullable restore
#line 58 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 2803, tale.Id, 2803, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"text\" class=\"rating\"");
            BeginWriteAttribute("value", " value=\"", 2839, "\"", 2866, 1);
#nullable restore
#line 58 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 2847, tale.AverageRating, 2847, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-size=\"xs\" data-show-clear=\"false\" data-show-caption=\"false\">\r\n                            <span style=\"margin-left:10px;\">");
#nullable restore
#line 59 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                       Write(tale.NumberOfRatings);

#line default
#line hidden
#nullable disable
            WriteLiteral(" votes</span>\r\n                        </div>\r\n                        ");
#nullable restore
#line 61 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                   Write(tale.CreationTime.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                    <p>\r\n");
#nullable restore
#line 64 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                         foreach (var tag in tale.Tags)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 3294, "\"", 3352, 2);
            WriteAttributeValue("", 3301, "/SearchResult?handler=SearchByTag&tagName=", 3301, 42, true);
#nullable restore
#line 66 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
WriteAttributeValue("", 3343, tag.Name, 3343, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-link\">#");
#nullable restore
#line 66 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                                                                        Write(tag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 67 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </p>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 71 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n\r\n    <h2 style=\"margin-top:80px; margin-bottom:70px;\">");
#nullable restore
#line 76 "D:\itra\itra_rep\kursovaya\Fanfic\Fanfic\Pages\Index.cshtml"
                                                Write(localizer["MostPopularTags"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    <div id=\"wordCloud\"></div>\r\n\r\n</div>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8d48cad83675bc36aa11001a77b328b673189d7415664", async() => {
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
            WriteLiteral("\r\n\r\n\r\n\r\n<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js\"></script>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8d48cad83675bc36aa11001a77b328b673189d7416810", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8d48cad83675bc36aa11001a77b328b673189d7417854", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8d48cad83675bc36aa11001a77b328b673189d7418894", async() => {
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
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Mvc.Localization.IViewLocalizer localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Fanfic.Pages.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Fanfic.Pages.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Fanfic.Pages.IndexModel>)PageContext?.ViewData;
        public Fanfic.Pages.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

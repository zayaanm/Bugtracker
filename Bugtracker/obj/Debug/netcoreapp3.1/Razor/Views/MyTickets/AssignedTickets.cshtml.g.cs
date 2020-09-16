#pragma checksum "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0f1d149aa6da0b004aeb5b2859d33d4e5cbf8ff2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MyTickets_AssignedTickets), @"mvc.1.0.view", @"/Views/MyTickets/AssignedTickets.cshtml")]
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
#line 1 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/_ViewImports.cshtml"
using Bugtracker;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/_ViewImports.cshtml"
using Bugtracker.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f1d149aa6da0b004aeb5b2859d33d4e5cbf8ff2", @"/Views/MyTickets/AssignedTickets.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2615f2e0cca016a8ffd328f8de0102b00f9e00e7", @"/Views/_ViewImports.cshtml")]
    public class Views_MyTickets_AssignedTickets : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Tickets>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Ticket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "IndividualTicket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<div class=""card shadow mb-4"">
    <div class=""card-header py-3"">
        <h6 class=""m-0 font-weight-bold text-primary"">Assigned Tickets</h6>
    </div>
    <div class=""card-body"">
        <div class=""table-responsive"">
            <table class=""table table-bordered"" id=""AssignedTickets"" width=""100%"" cellspacing=""0"">
                <thead>
                    <tr>
                        <th style=""text-align:center"">Name</th>
                        <th style=""text-align:center"">Priority</th>
                        <th style=""text-align:center"">Status</th>
                        <th style=""text-align:center"">Type</th>
                        <th style=""text-align:center"">Created</th>
                        <th style=""text-align:center"">Actions</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th style=""text-align:center"">Title</th>
                        <th style=""text-align:center"">Priority</th>
                        <");
            WriteLiteral(@"th style=""text-align:center"">Status</th>
                        <th style=""text-align:center"">Type</th>
                        <th style=""text-align:center"">Created</th>
                        <th style=""text-align:center"">Actions</th>
                    </tr>
                </tfoot>
                <tbody>

");
#nullable restore
#line 33 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                     foreach (var ticket in Model)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td align=\"center\">");
#nullable restore
#line 37 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                          Write(ticket.TicketName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td align=\"center\">");
#nullable restore
#line 38 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                          Write(ticket.TicketPriority);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td align=\"center\">");
#nullable restore
#line 39 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                          Write(ticket.TicketStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td align=\"center\">");
#nullable restore
#line 40 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                          Write(ticket.TicketType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td align=\"center\">");
#nullable restore
#line 41 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                          Write(ticket.TicketCreated);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td align=\"center\">\n                                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1921, "\"", 2085, 5);
            WriteAttributeValue("", 1931, "showInPopup(\'", 1931, 13, true);
#nullable restore
#line 43 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
WriteAttributeValue("", 1944, Url.Action("EditTickets","Ticket", new { projectId = ticket.ProjectId, ticketId = ticket.TicketId}, Context.Request.Scheme), 1944, 124, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2068, "\',", 2068, 2, true);
            WriteAttributeValue(" ", 2070, "\'Edit", 2071, 6, true);
            WriteAttributeValue(" ", 2076, "Ticket\')", 2077, 9, true);
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" class=\"btn btn-success d-inline\">Edit</button>\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f1d149aa6da0b004aeb5b2859d33d4e5cbf8ff28839", async() => {
                WriteLiteral("\n                                    <input type=\"submit\" value=\"View\" class=\"btn btn-info\" />\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-projectID", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 44 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                                                                                     WriteLiteral(ticket.ProjectId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["projectID"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-projectID", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["projectID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 44 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                                                                                                                                            WriteLiteral(ticket.TicketId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["ticketId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-ticketId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["ticketId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 2509, "\"", 2677, 5);
            WriteAttributeValue("", 2519, "showInPopup(\'", 2519, 13, true);
#nullable restore
#line 47 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
WriteAttributeValue("", 2532, Url.Action("DeleteTickets","Ticket", new { projectId = ticket.ProjectId, ticketId = ticket.TicketId}, Context.Request.Scheme), 2532, 126, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2658, "\',", 2658, 2, true);
            WriteAttributeValue(" ", 2660, "\'Delete", 2661, 8, true);
            WriteAttributeValue(" ", 2668, "Ticket\')", 2669, 9, true);
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" class=\"btn btn-danger d-inline\">Delete</button>\n                            </td>\n                        </tr>\n");
#nullable restore
#line 50 "/Users/zayaanmomin/Documents/Programming_side_projects/Bugtracker2/Bugtracker/Bugtracker/Views/MyTickets/AssignedTickets.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n                </tbody>\n            </table>\n        </div>\n    </div>\n</div>\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Tickets>> Html { get; private set; }
    }
}
#pragma warning restore 1591

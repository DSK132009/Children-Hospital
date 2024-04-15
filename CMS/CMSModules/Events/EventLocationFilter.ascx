<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventLocationFilter.ascx.cs" Inherits="CMSApp.CMSModules.Events.EventLocationFilter" %>

<asp:PlaceHolder runat="server">
    <cms:CMSDropDownList ID="drpLocations" runat="server" DataTextField="LocationName" DataValueField="LocationGuid" />
</asp:PlaceHolder>
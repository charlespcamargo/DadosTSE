<%@ Page Title="" Language="C#" MasterPageFile="~/Models/MasterPage.Master" AutoEventWireup="true" CodeBehind="DataWarehouse.aspx.cs" Inherits="UFSCar.DB.UI.Pages.BancoDados.DataWarehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="DataWarehouse.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3 class="page-title">Dashboard 
        <small>Métricas sobre os dados do TSE</small>
    </h3>

    <ul class="breadcrumb">
        <li>
            <i class="icon-home"></i>
            <a href="default.aspx">Home</a>
            <i class="icon-angle-right"></i>
        </li>
        <li><a href="#">Dashboard</a></li>
        <li class="pull-right no-text-shadow">
            <div id="dashboard-report-range" class="dashboard-date-range tooltips no-tooltip-on-touch-device responsive" data-tablet="" data-desktop="tooltips" data-placement="top" data-original-title="Change dashboard date range">
                <i class="icon-calendar"></i>
                <span></span>
                <i class="icon-angle-down"></i>
            </div>
        </li>
    </ul>

    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span12">

                <div class="portlet box grey">
                    <div class="portlet-title">
                        <div class="caption"><i class="icon-cogs"></i>Filtros</div>
                        <div class="tools">
                            <a href="javascript:;" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        
                    </div>
                </div>

                <div class="portlet box grey">
                    <div class="portlet-title">
                        <div class="caption"><i class="icon-cogs"></i>Bens por Candidato</div>
                        <div class="tools">
                            <a href="javascript:;" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div id="chart_div"></div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            oDW.init();
        });
    </script>



</asp:Content>

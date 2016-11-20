<%@ Page Title="UFSCar - Evolução Patrimonial" Language="C#" MasterPageFile="~/Models/MasterPage.Master" AutoEventWireup="true" CodeBehind="DataWarehouse.aspx.cs" Inherits="UFSCar.DB.UI.Pages.BancoDados.DataWarehouse" %>

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
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="row-fluid">
                            <div class="controls span2">
                                <h4>Ano Eleitoral</h4>
                                <select class="span12 m-wrap" id="ddlAnoEleitoral" data-placeholder="Selecione">
                                    <option value=""></option>
                                    <option value="2016">2016</option>
                                    <option value="2014">2014</option>
                                    <option value="2012">2012</option>
                                    <option value="2010">2010</option>
                                    <option value="2008">2008</option>
                                    <option value="2006">2006</option>
                                </select>
                            </div>
                            <div class="controls span2">
                                <h4>Sexo</h4>
                                <select class="span12 m-wrap" id="ddlSexo" data-placeholder="Selecione">
                                    <option value=""></option>
                                    <option value="M">Masculino</option>
                                    <option value="F">Feminino</option>
                                </select>
                            </div>
                            <div class="controls span4">
                                <h4>Escolaridade</h4>
                                 <select class="span12 m-wrap" id="ddlEscolaridade" data-placeholder="Selecione">
                                    <option value=""></option>
                                    <option value="0">Analfabeto</option>
                                    <option value="1">Ensino Fundamental Completo</option>
                                    <option value="2">Ensino Fundamental Incompleto</option>
                                    <option value="3">Ensino Médio Completo</option>
                                    <option value="4">Ensino Médio Incompleto</option>
                                    <option value="5">Lê e Escreve</option>
                                    <option value="6">Não Informado</option>
                                    <option value="7">Superior Completo</option>
                                    <option value="8">Superior Incompleto</option>
                                </select>
                            </div>
                               <div class="controls span4">
                                <h4>Ocupação</h4>
                                <input type="hidden" class="select2-offscreen" id="hfOcupacao" style="width: 100%" value="" />
                                <input type="text" class="hidden" id="ddlOcupacao" name="hfOcupacao" value="" />                      
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="controls span4">
                                <h4>Região</h4>
                                <select class="span12 m-wrap" id="ddlRegiao" data-placeholder="Selecione">
                                    <option value=""></option>
                                    <option value="Centro-Oeste">Centro-Oeste</option>
                                    <option value="Nordeste">Nordeste</option>
                                    <option value="Norte">Norte</option>
                                    <option value="Sudeste">Sudeste</option>
                                    <option value="Sul">Sul</option>
                                </select>
                            </div>
                            <div class="controls span4">
                                <h4>Estado</h4>
                                <input type="hidden" class="select2-offscreen" id="hfEstado" style="width: 100%" value="" />
                                <input type="text" class="hidden" id="ddlEstado" name="hfEstado" value="" />
                            </div>
                            <div class="controls span4">
                                <h4>Município</h4>
                                <input type="hidden" class="select2-offscreen" id="hfMunicipio" style="width: 100%" value="" />
                                <input type="text" class="hidden" id="ddlMunicipio" name="hfMunicipio" value="" />
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="controls span10">
                                </div>
                             <div class="controls span2">
                                <div class="btn-group" style="">
                                    <a id="btnBuscar" class="btn btn-margin-5px">Buscar <i class="icon-search"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="portlet box grey">
                    <div class="portlet-title">
                        <div class="caption"><i class="icon-cogs"></i>Evolução Patrimonial</div>
                        <div class="tools">
                            <a href="javascript:;" class="collapse"></a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div id="chart_div" style="min-height:100px">
                        </div>
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

<%@ Page Title="UFSCar - Evolução Patrimonial" Language="C#" MasterPageFile="~/Models/MasterPage.Master" AutoEventWireup="true" CodeBehind="EvolucaoPatrimonial.aspx.cs" Inherits="UFSCar.DB.UI.Pages.BancoDados.EvolucaoPatrimonial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="EvolucaoPatrimonial.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3 class="page-title">Consultas 
        <small>Métricas sobre os dados do TSE</small>
    </h3>

    <ul class="breadcrumb">
        <li>
            <i class="icon-home"></i>
            <a href="default.aspx">Home</a>
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="#">Banco de Dados</a>
            <i class="icon-angle-right"></i>
        </li>
        <li>
            <a href="default.aspx">Evolução Patrimonial</a>
        </li>
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
                                    <option value="MASCULINO">Masculino</option>
                                    <option value="FEMININO">Feminino</option>
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
                            <div class="controls span4">
                                <h4>Partido</h4>
                                <select class="span12 m-wrap" id="ddlPartido" data-placeholder="Selecione">
                                    <option value=""></option>
                                    <option value="DEM">DEM</option>
                                    <option value="NOVO">NOVO</option>
                                    <option value="PAN">PAN</option>
                                    <option value="PC do B">PC do B</option>
                                    <option value="PCB">PCB</option>
                                    <option value="PCO">PCO</option>
                                    <option value="PDT">PDT</option>
                                    <option value="PEN">PEN</option>
                                    <option value="PFL">PFL</option>
                                    <option value="PHS">PHS</option>
                                    <option value="PL">PL</option>
                                    <option value="PMB">PMB</option>
                                    <option value="PMDB">PMDB</option>
                                    <option value="PMN">PMN</option>
                                    <option value="PP">PP</option>
                                    <option value="PPL">PPL</option>
                                    <option value="PPS">PPS</option>
                                    <option value="PR">PR</option>
                                    <option value="PRB">PRB</option>
                                    <option value="PRONA">PRONA</option>
                                    <option value="PROS">PROS</option>
                                    <option value="PRP">PRP</option>
                                    <option value="PRTB">PRTB</option>
                                    <option value="PSB">PSB</option>
                                    <option value="PSC">PSC</option>
                                    <option value="PSD">PSD</option>
                                    <option value="PSDB">PSDB</option>
                                    <option value="PSDC">PSDC</option>
                                    <option value="PSL">PSL</option>
                                    <option value="PSOL">PSOL</option>
                                    <option value="PSTU">PSTU</option>
                                    <option value="PT do B">PT do B</option>
                                    <option value="PT">PT</option>
                                    <option value="PTB">PTB</option>
                                    <option value="PTC">PTC</option>
                                    <option value="PTN">PTN</option>
                                    <option value="PV">PV</option>
                                    <option value="REDE">REDE</option>
                                    <option value="SD">SD</option>
                                </select>
                            </div>
                            <div class="controls span4">
                                <h4>Cargo Pretendido</h4>
                                <select class="span12 m-wrap" id="ddlCargoPretendido" data-placeholder="Selecione">
                                    <option value=""></option>
                                    <option value="0">Deputado Distrital</option>
                                    <option value="1">Deputado Estadual</option>
                                    <option value="2">Governador</option>
                                    <option value="3">Senador</option>
                                    <option value="10">1º Suplente Senador</option>                                    
                                    <option value="4">2º Suplente Senador</option>
                                    <option value="5">Vereador</option>
                                    <option value="6">Deputado Federal</option>
                                    <option value="7">Presidente</option>
                                    <option value="8">Prefeito</option>
                                    <option value="9">Vice-Prefeito</option>
                                </select>
                            </div>
                            <div class="controls span4">
                                <div class="btn-group" style="margin-top:45px">
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
                        <div id="chart_div" style="min-height: 100px">
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            EvolucaoPatrimonial.init();
        });
    </script>



</asp:Content>

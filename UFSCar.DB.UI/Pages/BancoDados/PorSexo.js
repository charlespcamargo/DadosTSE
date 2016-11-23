var PorSexo = function () {


    var lst = [];


    return {

        init: function () {
            PorSexo.inicializarGoogleCharts();
            PorSexo.eventos();
            PorSexo.inicializarControles();
        },

        eventos: function () {
            $("#btnBuscar").on("click", function () {
                PorSexo.buscar();
            });

            $("#ddlRegiao").on("change", function () {
                PorSexo.alterouRegiao();
            });
        },

        inicializarControles: function () {
            PorSexo.carregarCombos();
            PorSexo.inicializarGrid();
        },

        inicializarGrid: function () {
            HelperJS.dataTableResult("gridPorSexo", PorSexo.montarColunasGrid(), [[0, 'asc'], [1, 'asc']], lst);
        },

        inicializarGoogleCharts: function () {
            google.charts.load('current', { 'packages': ['bar'] });
        },

        alterouRegiao: function () {
            PorSexo.carregarComboUF();
            PorSexo.carregarComboMunicipio();
        },


        carregarCombos: function () {
            $('#ddlAnoEleitoral').chosen({ allow_single_deselect: true });
            $('#ddlSexo').chosen({ allow_single_deselect: true });
            $('#ddlRegiao').chosen({ allow_single_deselect: true });
            $('#ddlEscolaridade').chosen({ allow_single_deselect: true });
            $('#ddlPartido').chosen({ allow_single_deselect: true });
            $('#ddlCargoPretendido').chosen({ allow_single_deselect: true });

            PorSexo.carregarComboOcupacao();
            PorSexo.carregarComboUF();
            PorSexo.carregarComboMunicipio();
        },

        carregarComboOcupacao: function () {

            HelperJS.popularSelect2("hfOcupacao", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfOcupacao", "ddlOcupacao", "Digite um código ou nome", "ocupacao/autocomplete/", false,
               PorSexo.FormataResultadoOcupacao, PorSexo.FormataGrupoOcupacao, PorSexo.FuncaoGrupoOcupacao, 2, null, true);
        },
        FormataResultadoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FormataGrupoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FuncaoGrupoOcupacao: function (item) { return item.ID; },


        carregarComboUF: function () {

            HelperJS.popularSelect2("hfEstado", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfEstado", "ddlEstado", "Digite um código ou nome", "localidade/autocompleteestado/" + $("#ddlRegiao").val(), false,
               PorSexo.FormataResultadoUF, PorSexo.FormataGrupoUF, PorSexo.FuncaoGrupoUF, 1, PorSexo.onMudarUF, true);

        },
        FormataResultadoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FormataGrupoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FuncaoGrupoUF: function (item) { return item.Sigla; },
        onMudarUF: function () {
            PorSexo.carregarComboMunicipio();
        },


        carregarComboMunicipio: function () {

            HelperJS.popularSelect2("hfMunicipio", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfMunicipio", "ddlMunicipio", "Digite um código ou nome", "localidade/autocompletemunicipio/" + $("#hfEstado").val(), false,
               PorSexo.FormataResultadoMunicipio, PorSexo.FormataGrupoMunicipio, PorSexo.FuncaoGrupoMunicipio, 2, null, true);
        },
        FormataResultadoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FormataGrupoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FuncaoGrupoMunicipio: function (item) { return item.ID; },




        buscar: function () {
            var filtro = PorSexo.montarFiltro();
            HelperJS.callApi(APIs.API_TSE, "/consultas/porsexo/", "POST", filtro, PorSexo.buscar_sucesso, HelperJS.showError);
        },

        montarFiltro: function () {
            var filtro = {};

            if (HelperJS.temValor($("#ddlAnoEleitoral").val()))
                filtro.Ano = $("#ddlAnoEleitoral").val();
            else
                filtro.Ano = 0;

            if (HelperJS.temValor($("#ddlSexo").val()))
                filtro.Sexo = $("#ddlSexo").val();
            else
                filtro.Sexo = "";

            if (HelperJS.temValor($("#ddlEscolaridade").val()))
                filtro.EscolaridadeID = $("#ddlEscolaridade").val();
            else
                filtro.EscolaridadeID = 0;

            if (HelperJS.temValor($("#hfOcupacao").val()))
                filtro.Ocupacao = $("#hfOcupacao").val();
            else
                filtro.Ocupacao = 0;

            if (HelperJS.temValor($("#ddlRegiao").val()))
                filtro.Regiao = $("#ddlRegiao").val();
            else
                filtro.Regiao = "";

            if (HelperJS.temValor($("#hfEstado").val()))
                filtro.EstadoSigla = $("#hfEstado").val();
            else
                filtro.EstadoSigla = "";

            if (HelperJS.temValor($("#hfMunicipio").val()))
                filtro.MunicipioID = $("#hfMunicipio").val();
            else
                filtro.MunicipioID = 0;

            if (HelperJS.temValor($("#ddlPartido").val()))
                filtro.PartidoSigla = $("#ddlPartido").val();
            else
                filtro.PartidoSigla = "";


            if (HelperJS.temValor($("#ddlCargoPretendido").val()))
                filtro.CargoPretendidoID = $("#ddlCargoPretendido").val();
            else
                filtro.CargoPretendidoID = 0;


            return filtro;
        },

        buscar_sucesso: function (data) {
            lst = data;

            var columns = PorSexo.montarColunasGrid();

            var sorter = [];
            var bPaginate = false;
            var bSort = false;
            var fnDrawCallback = undefined;
            console.log(data);
            HelperJS.dataTableResult('gridPorSexo', columns, sorter, data, bPaginate, bSort, fnDrawCallback)

            google.charts.setOnLoadCallback(PorSexo.drawChart);
        },



        drawChart: function () {

            PorSexo.tooltipOptions = {
                title: 'Evolução no Estado',
                //legend: 'Teste'
                hAxis: {
                    title: 'Ano'
                },
                vAxis: {
                    title: '% Feminina',
                    minValue: 0,
                    maxValue: 100
                },
                series: [{ color: 'red', visibleInLegend: false }
                ]
            };

            PorSexo.primaryOptions = {
                region: 'BR', resolution: 'provinces',
                colorAxis: { colors: ['#64B5F6', 'red'], minValue: 0, maxValue: 100 },
                tooltip: { isHtml: true }
            };

            var qtdFemino = {};
            var qtdTotal = {};

            lst.forEach(function (item, index) {
                if (qtdFemino[item.SiglaEstado] == undefined) {
                    qtdFemino[item.SiglaEstado] = 0;
                    qtdTotal[item.SiglaEstado] = 0;

                }
                qtdFemino[item.SiglaEstado] += item.QtdFeminino;
                qtdTotal[item.SiglaEstado] += item.QtdTotal;
            });

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Estado');
            data.addColumn('number', 'PorcentagemFeminina');
            data.addColumn({
                type: 'string',
                label: 'Tooltip Chart',
                role: 'tooltip',
                'p': { 'html': true }
            });

            for (prop in qtdTotal) {
                console.log(prop);
                data.addRow(['BR-' + prop, (qtdFemino[prop] / qtdTotal[prop]) * 100, '']);
            }

            PorSexo.primaryData = data;


            google.charts.load('upcoming', { 'packages': ['geochart', 'corechart'] });

            google.charts.setOnLoadCallback(PorSexo.drawTooltipCharts);
        },
        drawRegionsMap: function () {

            var chart = new google.visualization.GeoChart(document.getElementById('chart_div'));
            chart.draw(PorSexo.primaryData, PorSexo.primaryOptions);
        },
        // Draws your charts to pull the PNGs for your tooltips.
        drawTooltipCharts: function () {

            var tooltipData = [];
            tooltipData.push([]);
            tooltipData[0].push('Ano');

            var indexEstado = {};
            for (var i = 0; i < PorSexo.primaryData.Tf.length; i++) {
                var estado = PorSexo.primaryData.Tf[i].c[0].v;
                tooltipData[0].push(estado);
                indexEstado[estado.substring(3)] = i;
            }

            var anoEstado = {};

            lst.forEach(function (item, index) {
                if (anoEstado[item.Ano] == undefined) {
                    anoEstado[item.Ano] = {};
                }
                if (anoEstado[item.Ano][item.SiglaEstado] == undefined) {
                    anoEstado[item.Ano][item.SiglaEstado] = {};
                    anoEstado[item.Ano][item.SiglaEstado].QtdFeminino = 0;
                    anoEstado[item.Ano][item.SiglaEstado].QtdTotal = 0;
                }
                anoEstado[item.Ano][item.SiglaEstado].QtdFeminino += item.QtdFeminino;
                anoEstado[item.Ano][item.SiglaEstado].QtdTotal += item.QtdTotal;
            });
            i = 1;
            console.log(anoEstado);
            for (ano in anoEstado) {
                tooltipData.push([]);
                tooltipData[i].push(ano);

                for (estado in indexEstado) {

                    if (anoEstado[ano][estado] == undefined) {
                        tooltipData[i].push(0);
                    }
                    else {
                        var atual = anoEstado[ano][estado];
                        tooltipData[i].push((atual.QtdFeminino / atual.QtdTotal) * 100);
                    }
                }
                i++;
            }


            console.log(tooltipData);
            var data = new google.visualization.arrayToDataTable(tooltipData);
            var view = new google.visualization.DataView(data);


            // For each row of primary data, draw a chart of its tooltip data.
            for (var i = 0; i < PorSexo.primaryData.Tf.length; i++) {

                // Set the view for each event's data
                view.setColumns([0, i + 1]);

                var hiddenDiv = document.getElementById('chart_tooltip_div');
                var tooltipChart = new google.visualization.LineChart(hiddenDiv);

                google.visualization.events.addListener(tooltipChart, 'ready', function () {
                    // Get the PNG of the chart and set is as the src of an img tag.
                    var tooltipImg =
                            //"QtdMasculino"
                            //"QtdFeminino"
                            //"QtdTotal"
                            //"PercentualFeminino"
                        PorSexo.primaryData.Tf[i].c[0].v +
                        '<br/><img src="' + tooltipChart.getImageURI() + '">';
                    // Add the new tooltip image to your data rows.

                    PorSexo.primaryData.Tf[i].c[2].v = tooltipImg;
                });
                tooltipChart.draw(view, PorSexo.tooltipOptions);
            }
            PorSexo.drawRegionsMap();
        },
        montarColunasGrid: function () {

            var colunas = [];

            colunas.push({
                "mData": "Ano"
            });
            colunas.push({
                "mData": "Regiao"
            });
            colunas.push({
                "mData": "SiglaEstado"
            });
            colunas.push({
                "mData": "Municipio"
            });
            colunas.push({
                "mData": "Partido"
            });
            colunas.push({
                "mData": "QtdMasculino"
            });
            colunas.push({
                "mData": "QtdFeminino"
            });
            colunas.push({
                "mData": "QtdTotal"
            });
            colunas.push({
                "mData": "PercentualFeminino",
                "mRender": function (source, type, full) {
                    return HelperJS.formatarDecimalBR(full.PercentualFeminino);
                }
            });
            colunas.push({
                "mData": "PercentualMasculino",
                "mRender": function (source, type, full) {
                    return HelperJS.formatarDecimalBR(full.PercentualMasculino);
                }

            });
            //colunas.push({
            //    "mData": "Codigo",
            //    "mRender": function (source, type, full) {
            //        if (full.Codigo != null && full.Codigo > 0) {
            //            return full.Codigo;
            //        }
            //        else {
            //            return "-";
            //        }
            //    }
            //});
            return colunas;
        },
    }
}();
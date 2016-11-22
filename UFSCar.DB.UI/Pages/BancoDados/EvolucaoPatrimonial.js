var EvolucaoPatrimonial = function () {


    var lst = [];


    return {

        init: function () {
            EvolucaoPatrimonial.inicializarGoogleCharts();
            EvolucaoPatrimonial.eventos();
            EvolucaoPatrimonial.inicializarControles();
            EvolucaoPatrimonial.hardCode();
        },

        eventos: function () {
            $("#btnBuscar").on("click", function () {
                EvolucaoPatrimonial.buscar();
            });

            $("#ddlRegiao").on("change", function () {
                EvolucaoPatrimonial.alterouRegiao();
            });
        },

        inicializarControles: function () {
            EvolucaoPatrimonial.carregarCombos();
        },

        inicializarGoogleCharts: function () {
            google.charts.load('current', { 'packages': ['corechart'] });
        },

        alterouRegiao: function () {
            EvolucaoPatrimonial.carregarComboUF();
            EvolucaoPatrimonial.carregarComboMunicipio();
        },


        carregarCombos: function () {
            $('#ddlAnoEleitoral').chosen({ allow_single_deselect: true });
            $('#ddlSexo').chosen({ allow_single_deselect: true });
            $('#ddlRegiao').chosen({ allow_single_deselect: true });
            $('#ddlEscolaridade').chosen({ allow_single_deselect: true });
            $('#ddlPartido').chosen({ allow_single_deselect: true });
            $('#ddlCargoPretendido').chosen({ allow_single_deselect: true });

            EvolucaoPatrimonial.carregarComboOcupacao();
            EvolucaoPatrimonial.carregarComboUF();
            EvolucaoPatrimonial.carregarComboMunicipio();
        },

        carregarComboOcupacao: function () {

            HelperJS.popularSelect2("hfOcupacao", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfOcupacao", "ddlOcupacao", "Digite um código ou nome", "ocupacao/autocomplete/", false,
               EvolucaoPatrimonial.FormataResultadoOcupacao, EvolucaoPatrimonial.FormataGrupoOcupacao, EvolucaoPatrimonial.FuncaoGrupoOcupacao, 2, null, true);
        },
        FormataResultadoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FormataGrupoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FuncaoGrupoOcupacao: function (item) { return item.ID; },


        carregarComboUF: function () {

            HelperJS.popularSelect2("hfEstado", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfEstado", "ddlEstado", "Digite um código ou nome", "localidade/autocompleteestado/" + $("#ddlRegiao").val(), false,
               EvolucaoPatrimonial.FormataResultadoUF, EvolucaoPatrimonial.FormataGrupoUF, EvolucaoPatrimonial.FuncaoGrupoUF, 1, EvolucaoPatrimonial.onMudarUF, true);

        },
        FormataResultadoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FormataGrupoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FuncaoGrupoUF: function (item) { return item.Sigla; },
        onMudarUF: function () {
            EvolucaoPatrimonial.carregarComboMunicipio();
        },


        carregarComboMunicipio: function () {

            HelperJS.popularSelect2("hfMunicipio", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfMunicipio", "ddlMunicipio", "Digite um código ou nome", "localidade/autocompletemunicipio/" + $("#hfEstado").val(), false,
               EvolucaoPatrimonial.FormataResultadoMunicipio, EvolucaoPatrimonial.FormataGrupoMunicipio, EvolucaoPatrimonial.FuncaoGrupoMunicipio, 2, null, true);
        },
        FormataResultadoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FormataGrupoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FuncaoGrupoMunicipio: function (item) { return item.ID; },




        buscar: function () {
            var filtro = EvolucaoPatrimonial.montarFiltro();
            HelperJS.callApi(APIs.API_TSE, "/consultas/patrimonio/", "POST", filtro, EvolucaoPatrimonial.buscar_sucesso, HelperJS.showError);
        },

        montarFiltro: function () {
            var filtro = {};

            if (HelperJS.temValor($("#ddlAnoEleitoral").val()))
                filtro.Ano = $("#ddlAnoEleitoral").val();
            else
                filtro.Ano = 0;

            filtro.TodosAnos = $("#chkTodosAnos").prop("checked");

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
            google.charts.setOnLoadCallback(EvolucaoPatrimonial.drawChart);
        },

        drawChart: function () {
            console.log(lst);

            var anos = [];

            var candidatos = [];

            for (var i = 1; i < lst.length; i++) {
                anos.push(lst[i].lstColuna[0].Valor);
            }
            console.log(anos);



            var pointPlacement = (((lst[0].lstColuna.length) / 2)
                 / 10.0
                ) * -1;

            
            var step = 0.2;


            var size = pointPlacement * -0.9;

            pointPlacement += 0.15;

            for (var i = 1; i < lst[0].lstColuna.length; i += 2) {
                var cand = lst[0].lstColuna[i].Valor;

                candidatos.push(
                    {
                        name: cand,
                        //color: 'rgba(255,0,0,1)',
                        data: HelperJS.getCol(lst, i),
                        pointPlacement: pointPlacement,
                        pointPadding: size,

                    }
                );
                console.log(cand);
                console.log(pointPlacement);
                console.log(size);

                candidatos.push(
                    {
                        name: cand + 'Media',
                        color: 'rgba(255,0,0,1)',
                        data: HelperJS.getCol(lst, i + 1),
                        pointPlacement: pointPlacement,
                        pointPadding: size + (size * 0.2),

                    }
                );
                pointPlacement += step;
            }
            console.log(candidatos);
            Highcharts.chart('grafico', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Efficiency Optimization by Branch'
                },
                xAxis: {
                    categories: anos
                },
                yAxis: [{
                    min: 0,
                    title: {
                        text: 'Employees'
                    }
                }, {
                    title: {
                        text: 'Profit (millions)'
                    },
                    opposite: true
                }],
                legend: {
                    shadow: false
                },
                tooltip: {
                    shared: true
                },
                plotOptions: {
                    column: {
                        grouping: false,
                        shadow: false,
                        borderWidth: 0
                    }
                },
                series: candidatos
            });

        },

        /*
        drawChart: function () {

            var lstDados = [];
            var item = {};
            var ocupacao = {};

            var posicao = [
                            -0.9,
                            -0.7,
                            -0.5,
                            -0.3,
                            -0.1,
                            0.1,
                            0.3,
                            0.5,
                            0.7,
                            0.9
            ];


            $.each(lst, function (i, obj)
            { 
                item = {};
                item.name = obj.Nome;
                item.data = obj.lstVlrTotalDeclarado;
                item.tooltip = { valuePrefix: 'R$' };
                item.pointPadding = 0.1;
                //item.pointPlacement = "on";//posicao[i];
                lstDados.push(item);

                ocupacao = {};
                ocupacao.name = obj.Ocupacao;
                ocupacao.data = obj.lstVlrMedioOcupacao;
                ocupacao.tooltip = { valuePrefix: 'R$' };
                ocupacao.pointPadding = 0.2;
                //ocupacao.pointPlacement = "on";//posicao[i];
                lstDados.push(ocupacao);
            });
              

            $("#grafico").highcharts({
                chart: { type: 'column' },
                title: { text: 'Evolução Patrimonial' },
                xAxis: { categories: [2006, 2008, 2010, 2012, 2014, 2016 ] },
                yAxis: [{ min: 0, title: { text: 'Candidatos' } },
                        {
                            title: { text: 'Valor declardo de Bens' },
                            opposite: true
                        }],
                legend: { shadow: false },
                tooltip: { shared: true },
                plotOptions:
                {
                    column: {
                        stacking: 'normal'
                    }
                },
                series: lstDados
            });


        },

        */

        hardCode: function () {
            $("#ddlAnoEleitoral").val('2016').trigger("liszt:updated");
            var votorantim = {};
            votorantim.ID = 5580;
            votorantim.Nome = "Votorantim";
            HelperJS.popularSelect2("hfMunicipio", votorantim);
            $("#ddlCargoPretendido").val('8').trigger("liszt:updated");
        },

    }
}();
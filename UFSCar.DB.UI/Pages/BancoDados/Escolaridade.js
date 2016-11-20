var Escolaridade = function () {


    var lst = [];


    return {

        init: function () {
            Escolaridade.inicializarGoogleCharts();
            Escolaridade.eventos();
            Escolaridade.inicializarControles();
        },

        eventos: function () {
            $("#btnBuscar").on("click", function () {
                Escolaridade.drawChart();
            });

            $("#ddlRegiao").on("change", function () {
                Escolaridade.alterouRegiao();
            });
        },

        inicializarControles: function () {
            Escolaridade.carregarCombos();
        },

        inicializarGoogleCharts: function () {
            google.charts.load('current', { 'packages': ['bar'] });
        },

        alterouRegiao: function () {
            Escolaridade.carregarComboUF();
            Escolaridade.carregarComboMunicipio();
        },


        carregarCombos: function () {
            $('#ddlAnoEleitoral').chosen({ allow_single_deselect: true });
            $('#ddlSexo').chosen({ allow_single_deselect: true });
            $('#ddlRegiao').chosen({ allow_single_deselect: true });
            $('#ddlEscolaridade').chosen({ allow_single_deselect: true });
            $('#ddlPartido').chosen({ allow_single_deselect: true });
            $('#ddlCargoPretendido').chosen({ allow_single_deselect: true });

            Escolaridade.carregarComboOcupacao();
            Escolaridade.carregarComboUF();
            Escolaridade.carregarComboMunicipio();
        },

        carregarComboOcupacao: function () {

            HelperJS.popularSelect2("hfOcupacao", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfOcupacao", "ddlOcupacao", "Digite um código ou nome", "ocupacao/autocomplete/", false,
               Escolaridade.FormataResultadoOcupacao, Escolaridade.FormataGrupoOcupacao, Escolaridade.FuncaoGrupoOcupacao, 2, null, true);
        },
        FormataResultadoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FormataGrupoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FuncaoGrupoOcupacao: function (item) { return item.ID; },


        carregarComboUF: function () {

            HelperJS.popularSelect2("hfEstado", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfEstado", "ddlEstado", "Digite um código ou nome", "localidade/autocompleteestado/" + $("#ddlRegiao").val(), false,
               Escolaridade.FormataResultadoUF, Escolaridade.FormataGrupoUF, Escolaridade.FuncaoGrupoUF, 1, Escolaridade.onMudarUF, true);

        },
        FormataResultadoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FormataGrupoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FuncaoGrupoUF: function (item) { return item.Sigla; },
        onMudarUF: function () {
            Escolaridade.carregarComboMunicipio();
        },


        carregarComboMunicipio: function () {

            HelperJS.popularSelect2("hfMunicipio", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfMunicipio", "ddlMunicipio", "Digite um código ou nome", "localidade/autocompletemunicipio/" + $("#hfEstado").val(), false,
               Escolaridade.FormataResultadoMunicipio, Escolaridade.FormataGrupoMunicipio, Escolaridade.FuncaoGrupoMunicipio, 2, null, true);
        },
        FormataResultadoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FormataGrupoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FuncaoGrupoMunicipio: function (item) { return item.ID; },




        buscar: function () {
            var filtro = Escolaridade.montarFiltro();
            HelperJS.callApi(APIs.API_TSE, "/exemplo/exemplificar/", "GET", filtro, Escolaridade.buscar_sucesso, HelperJS.showError);
        },

        montarFiltro: function ()
        {
            var filtro = {};

            if (HelperJS.temValor($("#ddlAnoEleitoral").val()))
                filtro.Ano = $("#ddlAnoEleitoral").val();
            else
                filtro.Ano = 0;

            if (HelperJS.temValor($("#ddlSexo").val()))
                filtro.Sexo = $("#ddlSexo").val();
            else
                filtro.Sexo = 0;

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
                filtro.EstadoID = $("#hfEstado").val();
            else
                filtro.EstadoID = 0;

            if (HelperJS.temValor($("#hfMunicipio").val()))
                filtro.MunicipioID = $("#hfMunicipio").val();
            else
                filtro.MunicipioID = 0;

            if (HelperJS.temValor($("#ddlPartido").val()))
                filtro.PartidoSigla = $("#ddlPartido").val();
            else
                filtro.PartidoSigla = 0;


            if (HelperJS.temValor($("#ddlCargoPretendido").val()))
                filtro.CargoPretendidoID = $("#ddlCargoPretendido").val();
            else
                filtro.CargoPretendidoID = 0;


            return filtro;
        },

        buscar_sucesso: function (data) {
            lst = data;
            google.charts.setOnLoadCallback(Escolaridade.drawChart);
        },

        drawChart: function () {
            var options =
            {
                chart:
                {
                    title: 'Declaração ',
                    subtitle: '',
                },
                bars: 'horizontal',
                hAxis: { format: 'decimal' },
                height: 400
            };


            if (lst != null && lst.length > 0) {
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Nome');
                data.addColumn('number', '2006');
                data.addColumn('number', '2008');
                data.addColumn('number', '2010');
                data.addColumn('number', '2012');
                data.addColumn('number', '2014');
                data.addColumn('number', '2016');

                $.each(lst, function (i, obj) {
                    data.addRow([obj.NomeUrna, obj.Bens2006, obj.Bens2008, obj.Bens2010, obj.Bens2012, obj.Bens2014, obj.Bens2016]);
                });

                chart = new google.charts.Bar(document.getElementById('chart_div'));
                chart.draw(data, google.charts.Bar.convertOptions(options));
            }
        },


    }
}();
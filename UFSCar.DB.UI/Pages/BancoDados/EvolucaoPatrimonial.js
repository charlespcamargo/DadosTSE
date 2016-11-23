var EvolucaoPatrimonial = function () {


    var lst = [];
    var dataSource = [];


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
            EvolucaoPatrimonial.inicializarGrid();
        },

        inicializarGrid: function () {
            HelperJS.dataTableResult("gridResultado", EvolucaoPatrimonial.montarColunasGrid(), [[0, 'asc'], [1, 'asc']], lst);
        },

        inicializarGoogleCharts: function () {

            google.charts.load('current', { packages: ['corechart', 'bar'] });
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
            if (data != null)
                lst = data;
            else
                lst = [];
            EvolucaoPatrimonial.datasource = data;

            HelperJS.dataTableResult("gridResultado", EvolucaoPatrimonial.montarColunasGrid(), [[0, 'asc'], [1, 'asc']], lst);
        },

        exibirGrafico: function (cpf) {
            $("#modalGrafico").modal();
            EvolucaoPatrimonial.exibirGrafico_sucesso(cpf);
        },


        //google.charts.setOnLoadCallback(EvolucaoPatrimonial.drawChart);

        exibirGrafico_sucesso: function (cpf) {
            EvolucaoPatrimonial.cpf = cpf;
            google.charts.setOnLoadCallback(EvolucaoPatrimonial.drawChart);

        },

        montarColunasGrid: function () {

            var colunas = [];

            colunas.push({ "mData": "Ano" });
            colunas.push({ "mData": "SiglaEstado" });
            colunas.push({ "mData": "Municipio" });
            colunas.push({ "mData": "Nome" });
            colunas.push({ "mData": "Ocupacao" });
            colunas.push({ "mData": "VlrMedioOcupacao" });
            colunas.push({ "mData": "VlrTotalDeclarado" });
            colunas.push({ "mData": "DiferencaMedia" });
            colunas.push({ "mData": "CargoPolitico" });
            colunas.push({ "mData": "Partido" });
            colunas.push({
                "mData": "CPF",
                "mRender": function (source, type, full) {
                    var exibirGrafico = "<a class='icons-dataTable tooltips' data-toggle='tooltip' data-original-title='Exibir Gráfico' onclick=\"EvolucaoPatrimonial.exibirGrafico('" + full.CPF + "');\" href='javascript:;'><i class='icon-bar-chart'></i></a>";
                    return "<center>" + exibirGrafico + "</center>";
                }
            });

            return colunas;
        },


        drawChart: function () {
            var data = new google.visualization.DataTable();
            var rows = [];
            EvolucaoPatrimonial.current = undefined;
            for (var i = 0; i < EvolucaoPatrimonial.datasource.length; i++) {
                var item = EvolucaoPatrimonial.datasource[i];
                if (item.CPF == EvolucaoPatrimonial.cpf) {
                    EvolucaoPatrimonial.current = item;
                    rows.push([item.Ano + '', HelperJS.toNumber(item.VlrTotalDeclarado), HelperJS.toNumber(item.VlrMedioOcupacao)]);
                }
            }

            data.addColumn('string', 'Ano');
            data.addColumn('number', 'Vlr. Declarado');
            data.addColumn('number', 'Vlr. Média Ocupação');

            data.addRows(rows);

            var options = {
                title: 'Evolução Patrimonial - ' + EvolucaoPatrimonial.current.Nome,
                vAxis: {
                    title: 'Valor em R$'
                },
                chartArea: { left: 150,  width: '65%'},
                hAxis: {
                    title: 'Ano',
                    minValue: 2006
                },
                width: 900,
                heigth: 400,
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('grafico'));
            chart.draw(data, options);


        },
        hardCode: function () {
            $("#ddlAnoEleitoral").val('2016').trigger("liszt:updated");
            var votorantim = {};
            votorantim.ID = 5580;
            votorantim.Nome = "Votorantim";
            HelperJS.popularSelect2("hfMunicipio", votorantim);
            $("#ddlCargoPretendido").val('8').trigger("liszt:updated");


        }
    }
}();
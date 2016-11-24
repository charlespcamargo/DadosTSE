var Escolaridade = function () {


    var lst = [];


    return {

        init: function () {
            Escolaridade.eventos();
            Escolaridade.inicializarControles();
            Escolaridade.hardCode();
        },

        eventos: function () {
            $("#btnBuscar").on("click", function () {
                Escolaridade.buscar();
            });

            $("#ddlRegiao").on("change", function () {
                Escolaridade.alterouRegiao();
            });
        },

        inicializarControles: function () {
            Escolaridade.carregarCombos();
            Escolaridade.inicializarGrid();
        },


        alterouRegiao: function () {
            Escolaridade.carregarComboUF();
            Escolaridade.carregarComboMunicipio();
        },


        inicializarGrid: function () {
            HelperJS.dataTableResult("gridResultado", Escolaridade.montarColunasGrid(), [[0, 'asc'], [1, 'asc']], lst);
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
        FuncaoGrupoOcupacao: function (item) { return item.Descricao; },


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
            HelperJS.callApi(APIs.API_TSE, "/consultas/escolaridade/", "POST", filtro, Escolaridade.buscar_sucesso, HelperJS.showError);
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

            if (HelperJS.temValor($("#ddlEscolaridade").val()) && $("#ddlEscolaridade").val())
                filtro.EscolaridadeID = $("#ddlEscolaridade").val();
            else
                filtro.EscolaridadeID = -1;

            if (HelperJS.temValor($("#hfOcupacao").val()))
                filtro.Ocupacao = $("#hfOcupacao").val();
            else
                filtro.Ocupacao = "";

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


            if (HelperJS.temValor($("#ddlCargoPretendido").val()) && $("#ddlCargoPretendido").val() != 0)
                filtro.CargoPretendidoID = $("#ddlCargoPretendido").val();
            else
                filtro.CargoPretendidoID = -1;


            return filtro;
        },

        buscar_sucesso: function (data) {

            if (data != null)
                lst = data;
            else
                lst = [];

            HelperJS.dataTableResult("gridResultado", Escolaridade.montarColunasGrid(), [[0, 'asc'], [1, 'asc']], lst);

        },




        montarColunasGrid: function () {

            var colunas = [];

            colunas.push({ "mData": "Ano" });
            colunas.push({ "mData": "Regiao" });
            colunas.push({ "mData": "SiglaEstado" });
            colunas.push({ "mData": "Municipio" });
            colunas.push({ "mData": "CargoPolitico" });
            colunas.push({ "mData": "Partido" });
            colunas.push({ "mData": "Escolaridade" });
            colunas.push({ "mData": "Quantidade" });
            colunas.push({ "mData": "Total" });
            colunas.push({
                "mData": "Percentual",
                "mRender": function (source, type, full) {
                    return  source + "%";
                }
            });
            colunas.push({ "mData": "Fracao" });

            return colunas;
        },

        hardCode: function () {
            $("#ddlAnoEleitoral").val('2016').trigger("liszt:updated");
            var cidade = {};
            cidade.ID = 4884;
            cidade.Nome = "São Paulo";
            HelperJS.popularSelect2("hfMunicipio", cidade);
            //$("#ddlCargoPretendido").val('8').trigger("liszt:updated");
        }

    }
}();
var oDW = function () {


    return {

        init: function ()
        {
            oDW.inicializarGoogleCharts();
            oDW.eventos();
            oDW.inicializarControles();
        },

        eventos: function ()
        {
            $("#btnBuscar").on("click", function ()
            {
                oDW.drawChart();
            });

            $("#ddlRegiao").on("change", function () {
                oDW.alterouRegiao();
            });
        },

        inicializarControles:function()
        {
            oDW.carregarCombos();        
        },
        
        inicializarGoogleCharts:function()
        {
            google.charts.load('current', { 'packages': ['bar'] });
            google.charts.setOnLoadCallback(oDW.drawChart);        
        },

        alterouRegiao: function () 
        {        
            oDW.carregarComboUF();
            oDW.carregarComboMunicipio();
        },


        carregarCombos:function()
        {
            $('#ddlAnoEleitoral').chosen({ allow_single_deselect: true });
            $('#ddlSexo').chosen({ allow_single_deselect: true });
            $('#ddlRegiao').chosen({ allow_single_deselect: true });
            $('#ddlEscolaridade').chosen({ allow_single_deselect: true });
            
            oDW.carregarComboOcupacao();
            oDW.carregarComboUF();
            oDW.carregarComboMunicipio();
        },

        carregarComboOcupacao: function () {

            HelperJS.popularSelect2("hfOcupacao", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfOcupacao", "ddlOcupacao", "Digite um código ou nome", "ocupacao/autocomplete/", false,
               oDW.FormataResultadoOcupacao, oDW.FormataGrupoOcupacao, oDW.FuncaoGrupoOcupacao, 2, null, true);
        },
        FormataResultadoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FormataGrupoOcupacao: function (item) { return item.ID + " - " + item.Descricao; },
        FuncaoGrupoOcupacao: function (item) { return item.ID; },


        carregarComboUF: function () {

            HelperJS.popularSelect2("hfEstado", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfEstado", "ddlEstado", "Digite um código ou nome",  "localidade/autocompleteestado/" + $("#ddlRegiao").val(), false,
               oDW.FormataResultadoUF, oDW.FormataGrupoUF, oDW.FuncaoGrupoUF, 1, oDW.onMudarUF, true);

        },
        FormataResultadoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FormataGrupoUF: function (item) { return item.Sigla + " - " + item.Nome; },
        FuncaoGrupoUF: function (item) { return item.ID; },
        onMudarUF: function ()
        {
            oDW.carregarComboMunicipio();
        },
                

        carregarComboMunicipio: function () {

            HelperJS.popularSelect2("hfMunicipio", null);

            HelperJS.ComboAutoComplete(APIs.API_TSE, "hfMunicipio", "ddlMunicipio", "Digite um código ou nome", "localidade/autocompletemunicipio/" + $("#hfEstado").val(), false,
               oDW.FormataResultadoMunicipio, oDW.FormataGrupoMunicipio, oDW.FuncaoGrupoMunicipio, 2, null, true);
        },
        FormataResultadoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FormataGrupoMunicipio: function (item) { return item.ID + " - " + item.Nome; },
        FuncaoGrupoMunicipio: function (item) { return item.ID; },




        drawChart: function ()
        {


            HelperJS.callApi(APIs.API_TSE, "/exemplo/exemplificar/", "GET", null, oDW.drawChart_sucesso, HelperJS.showError);
        },

        drawChart_sucesso: function (lst) {

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


            if (lst != null && lst.length > 0)
            {
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Nome');
                data.addColumn('number', '2006');
                data.addColumn('number', '2008');
                data.addColumn('number', '2010');
                data.addColumn('number', '2012');
                data.addColumn('number', '2014');
                data.addColumn('number', '2016');

                $.each(lst, function (i, obj)
                {
                    data.addRow([obj.NomeUrna, obj.Bens2006, obj.Bens2008, obj.Bens2010, obj.Bens2012, obj.Bens2014, obj.Bens2016]);
                });

                chart = new google.charts.Bar(document.getElementById('chart_div'));
                chart.draw(data, google.charts.Bar.convertOptions(options));
            }
        },


    }
}();
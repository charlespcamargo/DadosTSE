var oExemplo = function () {


    return {

        init: function () {
            google.charts.load('current', { 'packages': ['bar'] });
            google.charts.setOnLoadCallback(oExemplo.drawChart);
            oExemplo.eventos();
        },

        eventos: function () {
            $("button").on("click", function (e) {
                options.hAxis.format = e.target.id === 'none' ? '' : e.target.id;
            });
        },

        drawChart: function () {
            HelperJS.callApi(APIs.API_TSE, "/exemplo/exemplificar/", "GET", null, oExemplo.drawChart_sucesso, HelperJS.showError);
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
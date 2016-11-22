var GraficoApp = function () {
    return {
        load: function (ID, textoTitle, textoSubtitle, categorias, series, legenda, plotOptions, widthChart) {
            $("#" + ID).highcharts({
                chart: {
                    zoomType: 'xy',
                    width: widthChart,
                },
                title: {
                    text: textoTitle
                },
                subtitle: {
                    text: textoSubtitle
                },
                xAxis: [{
                    categories: categorias,
                }],
                yAxis: (legenda == undefined ? GraficoApp.configurarLegendaPadrao() : legenda),
                tooltip: {
                    formatter: function () {
                        var s = '<b>' + this.x + '</b>';

                        $.each(this.points, function (i, point) {
                            var percentageDecimals = point.series.tooltipOptions.percentageDecimals;
                            s += '<br/><span style="color:' + point.series.color + '">\u25CF</span>: <b>' + point.series.name + ': ' + "</b>" + UFSCar.formatMoney(point.y, percentageDecimals, ".", ",") + point.series.tooltipOptions.valueSuffix;
                        });

                        return s;
                    },
                    shared: true,
                },
                legend: {
                    layout: 'horizontal',
                    verticalAlign: 'bottom',
                    floating: false,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                },
                //scrollbar: {
                //    enabled: true
                //},
                series: series,
                plotOptions: plotOptions != undefined || plotOptions != null ? plotOptions : {}
            });
        },

        configurarLegendaPadrao: function () {
            var legendas = [{ // Primary yAxis
                labels: {
                    format: this.y,
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                },
                title: {
                    text: 'Millions',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                }

            }, { // Secondary yAxis
                title: {
                    text: ' ',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    },
                },
                labels: {
                    format: '{value} %',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                min: 0,
                max: 100,
                opposite: true
            }];
            return legendas;
        },

        montarStackedColumn: function () {
            var plotOptions = {
                column: {
                    stacking: 'normal',
                    dataLabels: {
                        enabled: true,
                        color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                        style: {
                            textShadow: '0 0 3px black'
                        }
                    }
                }
            };
            return plotOptions;
        },

        montarPiePlotOptions: function () {
            var plotOptions = {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
            }
            return plotOptions;
        },
    };

}();
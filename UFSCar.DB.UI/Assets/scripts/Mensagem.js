// Para implementar a mensagem crie a div abaixo na pagina que você irá exibir.
// <div data-alerts="alerts" data-titles="{&quot;warning&quot;: &quot;&lt;em&gt;Warning!&lt;/em&gt;&quot;}" data-ids="myid" data-fade="6000"></div>
var OPTIONS;

var Mensagem = function () {
    var data;
    var $window = $(window);
    // side bar
    setTimeout(function () {
        $('.bs-docs-sidenav').affix({
            offset: {
                top: function () { return $window.width() <= 980 ? 250 : 170 }
            , bottom: 270
            }
        })
    }, 100)

    return {
        //Exibir mensagem espera dois parametros 1º- texto da mensagem / 2º- tipo de mensagem (success, error, info)
        ExibirMensagem: function (texto, tipoMensagem) {
            //window.prettyPrint && prettyPrint();
            //$(document).trigger("add-alerts", [
            //  {
            //      'message': texto,
            //      'priority': tipoMensagem
            //  }
            //]);
            var titulo = "";
            if (tipoMensagem == "success")
                titulo = "SUCESSO";
            else if (tipoMensagem == "error")
                titulo = "ERRO";
            else if (tipoMensagem == "info")
                titulo = "ATENÇÃO";

            $.gritter.add({ title: titulo, text: texto, sticky: false });
        },
    };
}();

var Options = function () {
    var OPTIONS = {
        animation: true,
        placement: "top",
        btnOkLabel: "<i style='width:25px' class='icon-ok-sign'></i> SIM",
        btnCancelLabel: "<i style='width:25px' class='icon-ok-sign'></i> NÃO",
        singleton: true,
        href: 'javascript:;'
    };
    return {
        Getconfirm: function () {
            return OPTIONS
        }
    }
}();

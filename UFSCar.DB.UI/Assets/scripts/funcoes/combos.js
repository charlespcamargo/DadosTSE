//classe que contem métodos comuns que utilizaremos em várias telas, por exemplo, pra carregar o combo de clientes, basta passar o controle hidden seguido pelo nome da função
// **************
//EX de chamada de um controle na página: $('hfCliente').autoCompleteCliente();
// **************

// function combo de cliente
(function ($) {

    $.fn.autoCompleteCliente = function () {
        var hiddenId = $(this).prop('id');
        var selectId = $(this).prop('for');
        UFSCar.ComboAutoComplete(APIs.API_CLIENTE, hiddenId, selectId, "Digite um código ou nome", "clientecadastro/listarautocomplete", false,
                   formataResultadoClientes, formataClientes, funcaoClientes, 3, null, true);


    };


    function formataResultadoClientes(item) {
        var codigo = item.IDCliente < 0 ? "" : item.IDCliente + " - ";

        if (item.Nome != null && item.Nome != '')
            return codigo + item.Nome;
        else
            return codigo + item.NomeFantasia;
    };

    function formataClientes(item) {
        var codigo = item.IDCliente < 0 ? "" : item.IDCliente + " - ";

        if (item.Nome != null && item.Nome != '')
            return codigo + item.Nome;
        else
            return codigo + item.NomeFantasia;
    };

    function funcaoClientes(item) { return item.IDCliente; };

})(jQuery);



// function combo de ATV

(function ($) {
    var controleId = "";
    $.fn.carregarRegiaoComercial = function (settings) {
        var ano;
        var nivel;
        if (settings != null && settings.ano != undefined && settings.ano != null) {
            ano = settings.ano
        }
        else {
            ano = (new Date).getFullYear();
        }

        controleId = $(this).prop('id');
        if (settings != null && settings.nivel != undefined && settings.nivel != null) {
            nivel = settings.nivel;
        }
        else {
            nivel = "DCM";
        }

        UFSCar.callApi(APIs.API_TAIO, "cliente/listarRegioes/" + ano + "/" + nivel, "GET", null, carregarRegiaoComercialSucesso, UFSCar.showError);
    };

    function carregarRegiaoComercialSucesso(data) {
        $('#' + controleId).empty();

        var combo = '<option value=""></option>';
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                combo += '<option value="' + data[i].rcs_regiao_comercial + '" chapa="' + data[i].rcs_chapa_funcionario + '">' + data[i].rcs_regiao_comercial_msk + " - ( " + data[i].rcs_chapa_funcionario + " ) " + (data[i].Funcionario != null ? data[i].Funcionario.Apelido : "") + '</option>';
            }
        }

        $('#' + controleId).append(combo);
        $('#' + controleId).chosen({ allow_single_deselect: true });
        $('#' + controleId).trigger("liszt:updated");
    }

})(jQuery);




// function combo de Estados

(function ($) {
    var controleId = "";
    var oSettings = new Object();
    $.fn.UF = function (settings, onChange) {
        controleId = $(this).prop('id');
        oSettings.IgnorarVazio = settings.IgnorarVazio;
        oSettings.UfIgnorada = settings.UfIgnorada;
        oSettings.ValorSelecionado = settings.ValorSelecionado;

        UFSCar.callApi(APIs.API_GENERICA, "estado/listar", "GET", null,
             function (data) {
                 var controle = '#' + controleId;
                 $(controle).empty().trigger("liszt:updated");

                 if (oSettings.IgnorarVazio != null && oSettings.IgnorarVazio != undefined && oSettings.IgnorarVazio == true) {
                     $(controle).append($("<option />", { value: '', text: '' }));
                 }

                 if (data.length > 0) {
                     for (var i = 0; i < data.length; i++) {
                         if (oSettings.UfIgnorada != null && oSettings.UfIgnorada != undefined && oSettings.UfIgnorada.length > 0) {
                             if (oSettings.UfIgnorada.toString().indexOf(data[i].Sigla.toString().trim()) == -1)
                                 $(controle).append($("<option />", { value: data[i].Sigla, text: data[i].Sigla }));
                         }
                         else {
                             $(controle).append($("<option />", { value: data[i].Sigla, text: data[i].Sigla }));
                         }
                     }

                     if (oSettings.ValorSelecionado != undefined && oSettings.ValorSelecionado != null) {
                         $(controle).val(oSettings.ValorSelecionado);
                     }
                     $(controle + '_chzn input').css({ "width": '40' });
                     $(controle).trigger("liszt:updated");
                     $(controle).chosen({ allow_single_deselect: true });

                     $(controle).on("change", function (e) {
                         if (onChange != null && onChange != undefined)
                             onChange($(this).val());
                     });
                 }
             }
         , UFSCar.showError);
    };
})(jQuery);



// function combo de cidade
(function ($) {
    $.fn.autoCompleteCidade = function (settings) {
        debugger;
        var hiddenId = $(this).prop('id');
        var selectId = $(this).attr('for');
        if (settings.Uf != undefined && settings.Uf != null && settings.Uf != "") {
            UFSCar.ComboAutoComplete(APIs.API_GENERICA, hiddenId, selectId, "Digite um código ou nome", "cidade/listarautocomplete/" + (settings.Uf == "*" ? "EX" : settings.Uf), false,
                FormataResultadoCidade, FormataCidade, FuncaoCidade, 3, null, true);
        } else {
            UFSCar.ComboAutoComplete(APIs.API_GENERICA, hiddenId, selectId, "Digite um código ou nome", "cidade/listarautocomplete/" + "EX", false,
             FormataResultadoCidade, FormataCidade, FuncaoCidade, 3, null, true);
            UFSCar.popularSelect2(hiddenId, null);
        }
    };

    function FormataResultadoCidade(item) { return item.IDCidade + " - " + item.Nome; }
    function FormataCidade(item) {
        if (item.IDCidade != 0) {
            return item.IDCidade + " - " + item.Nome;
        }
        else
            return "";
    }
    function FuncaoCidade(item) { return item.IDCidade; }

})(jQuery);

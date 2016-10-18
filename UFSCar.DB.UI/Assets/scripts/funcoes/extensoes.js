
//Obtem o texto de um controle
$.fn.getText = function () {
    ///chama o getType do Ihara.js para pegar o tipo do controle
    var tipo = $(this).getType();
    var controleId = $(this).attr('Id');
    var valores = null;
    switch (tipo) {
        case "chosen":  // se o controle for do tipo chosen então eu retorno uma array separado com os valores, pode ser do tipo multiplo e único.
            valores = new Array();
            valores = $("#" + controleId + " option:selected").map(function () {
                return $(this).text();
            }).get(); //Precisa usar o map pra poder pegar os delimitadores entre os texto, pq o texto do controle que usa multiple não tem virgula entre os texto, como é nos valores
            break;
        case "select":
            valores = $("#" + controleId + " option:selected").text();
            break;
    }
    return valores;
}
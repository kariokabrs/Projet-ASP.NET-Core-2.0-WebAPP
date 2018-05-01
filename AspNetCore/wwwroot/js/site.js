﻿$('#Salvar').click(function (e) {
    e.preventDefault;
    var _this = $(this);
    // Aqui desabilita o button de ser apertado novamente
    _this.attr('disabled', 'disabled');
    //show the loading image e hide o icone salvar atual
    $("#icon").hide();
    $("#progress").show();
    var url = "/usuarios/additem";
    var nome = $("#Nome").val();

    $.post(url, { Nome: nome }, function (data) {
        // checa a response do controller and ativa o button novamente
        _this.removeAttr("disabled", "disabled");
        //hide the progress bar div e show o icone salvar
        $("#icon").show();
        $("#progress").hide();
        $("#msg").html(data);

    });

});
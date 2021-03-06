﻿// PartialViewNovo

$('#Salvar').click(function (e) {

    e.preventDefault;
    var _this = $(this);
    var url = "/usuarios/additem";
    var nome = $("#Nome").val();
    var sobrenome = $("#Sobrenome").val();
    // Aqui desabilita o button de ser apertado novamente
    _this.attr('disabled', 'disabled');
    //show the loading image e hide o icone salvar atual
    $("#msg").hide();
    $("#icon").hide();
    $("#progress").show();


    $.post(url, { Nome: nome, Sobrenome: sobrenome }, function (data) {
        // checa a response do controller and ativa o button novamente
        _this.removeAttr("disabled", "disabled");
        //hide the progress bar div e show o icone salvar
        $("#icon").show();
        $("#progress").hide();
        $("#msg").show();
        $("#msg").html(data);

    }).done(function () {
        var listaUsuario = $("#listaUsuario");
        var url = listaUsuario.data("url");
        listaUsuario.load(url);
    });
});

// end


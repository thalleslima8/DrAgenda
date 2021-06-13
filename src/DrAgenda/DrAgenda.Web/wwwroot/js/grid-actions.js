function imageItem(item) {
    if (item) {
        return "<img src='" + item + "' class='img-rounded' style='width:50px'/>";
    } else {
        return "";
    }
}

function renderImage(data, url, tamanhopx) {
    if (data.Id) {
        return `<a data-fancybox href="${url}/${data.Id}"><img src="${url}/${data.Id}" onerror="/img/no-image.png" class="img-rounded fancy" style="width: ${tamanhopx}px"/></a>`;
    } else {
        return "";
    }
}

function renderImagens(data, url, tamanhopx) {

    if (data && data.Imagens && data.Imagens.length > 0)
    {
        var imagem = data.Imagens[0];

        if (imagem) {
            return `<a data-fancybox href='${url}/${imagem}'><img src='${url}/${imagem}' class='img-rounded fancy' onerror='/img/no-image.png' style='width:${tamanhopx}px'/></a>`;
        } else {
            return "";
        }
    }

    return "";
}

function visualizarPosicaoTarja(data) {

    switch (data.PosicaoTarja) {
        case 0:
            return "<span class='badge badge-info'><i class='fa fa-clock-o'></i> Sobrepor no Topo </span>";
        case 1:
            return "<span class='badge badge-info'><i class='fa fa-check'></i> Sobrepor no Rodapé </span>";
        case 2:
            return "<span class='badge badge-info'><i class='fa fa-exclamation-triangle'></i> Cocatenar no Topo </span>";
        case 3:
            return "<span class='badge badge-info'><i class='fa fa-times-circle'></i> Cocatenar no Rodapé </span>";
        default:
            return "";
    }
}

function booleanItem(isTrue) {
    if (isTrue)
        return "<i class='fa fa-check text-success'></i>";
    return "";
}

function corBox(data) {
    return `<a class="btn btn-sm" href="#" style="background-color: ${data.Cor};color: ${data.Cor};">###</a>`;
}

function checkItem(isChecked) {
    var checked = isChecked ? "checked" : "";
    return "<input type='checkbox' value='" + isChecked + "' " + checked + ">";
}

function setDate(item, format) {
    return kendo.format('{0:' + format + '}', kendo.parseDate(item));
}

function customAction(item, action, text) {
    var html = kendo.format("<a class=\"btn btn-sm btn-default\" href=\"{0}/{1}\">{2}</a>", action, item.Id, text);
    return html;
}


function editItem(item, action) {
    var html = kendo.format("<a class=\"btn btn-sm btn-warning\" href=\"{0}/{1}\"><i class=\"fa fa-pencil\"></i></a>", action, item.Id);
    return html;
}

function detailItem(item, action) {
    var html = kendo.format("<a class=\"btn btn-sm btn-info\" href=\"{0}/{1}\"><i class=\"fa fa-file-text\"></i></a>", action, item.Id);
    return html;
}

function deleteItem(item, action) {
    var html = kendo.format("<a class=\"btn btn-sm btn-danger\" href=\"javascript:deleteItemGrid('{0}', '{1}')\"><i class=\"fa fa-times\"></i></a>", action, item.Id);
    return html;
}

function editAndDeleteItem(item, actionEdit, actionDelete) {
    var html = kendo.format('<div class="btn-group btn-group-sm" role="group">' +
        '<a href="{1}/{0}" class="btn btn-warning"><i class="fa fa-edit"></i></a>' +
        '<a href="javascript:deleteItemGrid(\'{2}\', \'{0}\')" class="btn btn-danger"><i class="fa fa-times"></i></a>' +
        '</div>', item.Id, actionEdit, actionDelete);
    return html;
}

function editAndDeleteItemJS(item, editJsFunction, actionDelete, gridName) {
    var html = kendo.format('<div class="btn-group btn-group-sm" role="group">' +
        '<a href="javascript:{1}(\'{0}\')" class="btn btn-warning"><i class="fa fa-edit"></i></a>' +
        '<a href="javascript:deleteItemGrid(\'{2}\', \'{0}\', \'{3}\')" class="btn btn-danger"><i class="fa fa-times"></i></a>' +
        '</div>',
        item.Id,
        editJsFunction,
        actionDelete,
        gridName);
    return html;
}

function itemStatus(status) {

    switch (status) {
        case 1:
            return '<span class="badge badge-success"> Ok </span>';
        case 2:
            return '<span class="badge badge-danger"> Erro </span>';
        default:
            return '<span class="badge badge-warning"> Pendente </span>';
    }

    return "";
}

function statusProcessamento(status) {
    switch (status) {
    case 0:
        return '<span class="badge badge-info"> Triagem </span>';
    case 1:
        return '<span class="badge badge-info"> Classificação </span>';
    case 2:
        return '<span class="badge badge-info"> Reavaliar </span>';
    case 3:
        return '<span class="badge badge-warning"> Consultar </span>';
    case 4:
        return '<span class="badge badge-dark"> Processada </span>';
    case 5:
        return '<span class="badge badge-danger"> Descartada </span>';
    case 6:
        return '<span class="badge badge-success"> Auditar </span>';
    default:
        return "";
    }
}


function deleteItemGrid(action, id, gridName) {

    if (!gridName)
        gridName = "Grid";

    swal({
        title: 'Confirma a exclusão?',
        text: "Essa ação não poderá ser revertida!",
        type: 'question',
        showCancelButton: true,
        confirmButtonClass: 'btn primary',
        cancelButtonClass: 'btn danger m-r-md',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não',
        reverseButtons: true,
        buttonsStyling: false
    }).then(function (result) {
        $.blockUI();
        if (result.value) {
            $.ajax({
                type: "POST",
                url: action,
                data: { id: id },
                dataType: 'json',
                success: function (data) {
                    $.unblockUI();
                    if (data === "" || data.result === true) {

                        swal({
                            title: 'Ok!',
                            text: 'Seu registro foi excluido.',
                            type: 'success'
                        }).then(function () {
                            var grid = $("#" + gridName).data("kendoGrid");
                            grid.dataSource.read();
                        });
                    }
                    else if (data.result === false && data.message) {
                        swal(
                            'Não permitido!',
                            data.message,
                            'error'
                        );
                    } else {
                        swal(
                            'Não permitido!',
                            'Ocorreu um erro ao excluir o registro.',
                            'error'
                        );
                    }
                }
            });

        }
        else 
            $.unblockUI();

    });
}

function saveAsExcel(grid) {
    $("#" + grid).getKendoGrid().saveAsExcel();
}
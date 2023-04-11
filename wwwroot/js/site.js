// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SubmitItem() {
    debugger;
    $('#form-loader').show();
    $('.validateErrorBorder').removeClass('validateErrorBorder');
    var fileData = new FormData();
    var input = "<input type='text' id='{id}' name='{name}' value='{type={type}, value:{value}}' />";
    var columnValue = '';
    $('#form_createrecord [section]').each(function (tblIndex, section) {
        var tablename = $(section).attr('section'),
            rows = $(section).attr('rows'),
            order = $(section).attr('displayseqno'),
            othercolumns = $(section).attr('othercolumns');
        rows = rows ?? 1;
        order = order ?? 9999;
        othercolumns = othercolumns ?? "";
        isNewRecord = $(section).attr('isnewrecord');
        isNewRecord = isNewRecord ?? "1";
        tableRowBeforeDelete = $(section).attr('beforedelete');
        tableRowBeforeDelete = tableRowBeforeDelete ?? "Table:''";
        deletedColumnName = $(section).attr('deletedColumnName');
        deletedColumnName = deletedColumnName ?? "";

        var inputCopy = input.replace('{tablename}', tablename).replace('{rows}', rows);
        fileData.append('table-' + tablename, '{TableName:"' + tablename + '", Rows:' + rows + ', TableOtherColumns:[{' + othercolumns + '}], DisplaySeqNo:' + order + ', IsNewRecord:' + isNewRecord + ', TableRowBeforeDelete:{' + tableRowBeforeDelete + '}, DeletedColumnName:"' + deletedColumnName + '" }');
        //$(newForm).append("<input type='text' id='table-" + tablename + "' name='table-" + tablename + "' value='{tableName:[" + tablename + "], rows:[" + rows + "], otherColumns:[" + othercolumns + "], order:[" + order + "] }' />");
        $("[name^='" + tablename + "']:not([rel-item])").each(function (colIndex, column) {
            var inputCopy_ = inputCopy;
            var result = getValueFromElement(column, inputCopy_, fileData);
            columnValue = result.columnValue;
            inputCopy_ = result.inputCopy;
            //$(newForm).append(inputCopy_);
            //fileData.append($(inputCopy_).attr('id'), $(inputCopy_).val());
        })
    });
    if ($('.validateErrorBorder').length > 0) {
        $([document.documentElement, document.body]).animate({
            scrollTop: ($('#form_createrecord [section]').offset().top - 10)
        }, 1000);
        $('#form-loader').hide();
    }
    else {
        $.ajax({
            url: "/Dynamic/SaveUpdateRecord",
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false,
            data: fileData,
            success: function (data) {
                if (data.success == true) {
                    //alert('Data Saved Successfully.');
                    $.toast({
                        text: "Item has been successfully added!.",
                        heading: 'Congratulations!',
                        icon: 'success', // Type of toast icon
                        showHideTransition: 'fade', // fade, slide or plain
                        allowToastClose: true, // Boolean value true or false
                        hideAfter: 5000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                        position: 'top-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                        class: 'larger-font',
                        textAlign: 'left',  // Text alignment i.e. left, right or center
                        loader: true,  // Whether to show loader or not. True by default
                        loaderBg: '#4caf50',  // Background color of the toast loader
                        beforeShow: function () { }, // will be triggered before the toast is shown
                        afterShown: function () { }, // will be triggered after the toat has been shown
                        beforeHide: function () { }, // will be triggered before the toast gets hidden
                        afterHidden: function () { }  // will be triggered after the toast has been hidden
                    });

                    setTimeout(function () {
                        location.reload();
                    }, 6000);
                }
                else {
                    //alert(data.Message);
                    $('#form-loader').hide();
                    $.toast({
                        text: data.message,
                        heading: 'Error!',
                        icon: 'error', // Type of toast icon
                        showHideTransition: 'fade', // fade, slide or plain
                        allowToastClose: true, // Boolean value true or false
                        hideAfter: 5000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                        stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                        position: 'top-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                        class: 'larger-font',
                        textAlign: 'left',  // Text alignment i.e. left, right or center
                        loader: true,  // Whether to show loader or not. True by default
                        loaderBg: '#c4161d',  // Background color of the toast loader
                        beforeShow: function () { }, // will be triggered before the toast is shown
                        afterShown: function () { }, // will be triggered after the toat has been shown
                        beforeHide: function () { }, // will be triggered before the toast gets hidden
                        afterHidden: function () { }  // will be triggered after the toast has been hidden
                    });
                }
            }
        });
    }
    return false;
}

function getValueFromElement(element, inputCopy, fileData) {
    var nodeName = $(element).prop('nodeName').toLowerCase();
    var columnType = $(element).attr('type');
    var columnId = $(element).attr('id');
    var columnName = $(element).attr('name');
    var columnValue = $(element).val();
    var columnRequired = $(element).attr('column-required');
    var isPrimaryKey = $(element).attr('rel-primarykey');
    isPrimaryKey = isPrimaryKey == undefined ? 0 : 1;
    inputCopy = inputCopy.replace('{type}', 'text' ?? 'noType');
    inputCopy = inputCopy.replace('{id}', columnId);
    inputCopy = inputCopy.replace('{name}', columnName);
    inputCopy = inputCopy.replace('{columnname}', columnName ?? 'noName');
    if (nodeName == 'select') {
        if (columnRequired == "true" && columnValue == "0") {
            fieldRequired(element);
            columnValue = 'field Requried';
        }
    }
    else if (nodeName == 'input') {
        if (columnType == 'text' || columnType == 'textarea') {
            if (columnRequired == "true" && columnValue == "") {
                fieldRequired(element);
                columnValue = 'field Requried';
            }
        }
        else if (columnType == 'file') {
            var fileUpload = $(element).get(0);
            var file = fileUpload.files[0];
            if (file != undefined) {
                fileData.append(columnId, file);
            }
            columnValue = $(element).attr('value');
            columnValue = columnValue != undefined ? columnValue.replace(/\\{1,}/g, '\\\\') : columnValue;
        }
        else if (columnType == 'checkbox') {
            var elementName = $(element).attr('name');
            if (elementName.startsWith('BranchItemVariants-BusinessBranchID-')) {
                columnValue = $(element).attr('value');
            }
            else {
                columnValue = $(element).prop("checked") ? 1 : 0;
            }
        }
    }
    else if (nodeName == "textarea") {
        columnValue = $(element).val();
    }
    else {
        columnValue = $(element).attr("value");
    }
    columnValue = columnValue ?? '';
    columnValue = columnValue.toString().replace(/&/g, '&amp;').replace(/'/g, '&apos;').replace(/"/g, '&quot;');
    inputCopy = inputCopy.replace('{value}', columnValue == undefined ? 'noValue' : columnValue);

    columnType = columnType ?? "";
    var actionType = $(element).attr('actiontype');
    actionType = actionType ?? "0";
    var isNewRecord = 1;
    if (isPrimaryKey == 1) {
        isNewRecord = $(element).attr('isnewrecord');
        isNewRecord = isNewRecord ?? 1;
    }
    var isStringColumn = $(element).attr('column-isstringcolumn');
    isStringColumn = isNaN(isStringColumn) ? 1 : isStringColumn;
    columnValue = '{ColumnName:"' + columnId.split('-')[1] + '", IsPrimaryKey:' + isPrimaryKey + ', IsAutoGenerated: 0, Type:"' + columnType + '", Value:"' + columnValue + '", IsStringColumn:' + isStringColumn + ', IsNewRecord:' + isNewRecord + ', ActionType:' + actionType + ' }';
    fileData.append(columnId, columnValue);
    return { 'columnValue': columnValue, 'inputCopy': inputCopy };
}

function fieldRequired(element) {
    //alert($(element).attr('column-name') + ' : is required field');
    //$([document.documentElement, document.body]).animate({
    //    scrollTop: ($(element).offset().top - 20)
    //}, 1000);
    $(element).addClass('validateErrorBorder');
}

function RecordEdit(url) {
    location.href = url;
}

function RecordDelete(tableName, primaryColumn, primaryColumnValue) {
    var fileData = new FormData();
    fileData.append('tableName', tableName);
    fileData.append('primaryColumn', primaryColumn);
    fileData.append('primaryColumnValue', primaryColumnValue);
    $.ajax({
        url: "/Dynamic/DeleteRecord",
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false,
        data: fileData,
        success: function (data) {
            if (data.success == true) {
                //alert('Data Saved Successfully.');
                $.toast({
                    text: "Item has been successfully Deleted!.",
                    heading: 'Congratulations!',
                    icon: 'success', // Type of toast icon
                    showHideTransition: 'fade', // fade, slide or plain
                    allowToastClose: true, // Boolean value true or false
                    hideAfter: 5000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                    stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                    position: 'top-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                    class: 'larger-font',
                    textAlign: 'left',  // Text alignment i.e. left, right or center
                    loader: true,  // Whether to show loader or not. True by default
                    loaderBg: '#4caf50',  // Background color of the toast loader
                    beforeShow: function () { }, // will be triggered before the toast is shown
                    afterShown: function () { }, // will be triggered after the toat has been shown
                    beforeHide: function () { }, // will be triggered before the toast gets hidden
                    afterHidden: function () { }  // will be triggered after the toast has been hidden
                });

                setTimeout(function () {
                    location.reload();
                }, 6000);
            }
            else {
                //alert(data.Message);
                $.toast({
                    text: data.message,
                    heading: 'Error!',
                    icon: 'error', // Type of toast icon
                    showHideTransition: 'fade', // fade, slide or plain
                    allowToastClose: true, // Boolean value true or false
                    hideAfter: 5000, // false to make it sticky or number representing the miliseconds as time after which toast needs to be hidden
                    stack: 5, // false if there should be only one toast at a time or a number representing the maximum number of toasts to be shown at a time
                    position: 'top-right', // bottom-left or bottom-right or bottom-center or top-left or top-right or top-center or mid-center or an object representing the left, right, top, bottom values
                    class: 'larger-font',
                    textAlign: 'left',  // Text alignment i.e. left, right or center
                    loader: true,  // Whether to show loader or not. True by default
                    loaderBg: '#c4161d',  // Background color of the toast loader
                    beforeShow: function () { }, // will be triggered before the toast is shown
                    afterShown: function () { }, // will be triggered after the toat has been shown
                    beforeHide: function () { }, // will be triggered before the toast gets hidden
                    afterHidden: function () { }  // will be triggered after the toast has been hidden
                });
            }
        }
    });

}

function ToggleContent(div, button) {
    if ($('.' + div).is(':visible')) {
        $('.' + div).hide(1000);
        $('.' + button).show(1000);
    }
    else {
        formOpen();
        $('.' + div).show(1000);
        $('.' + button).hide(1000);
    }
}

function CloseForm(div, button) {
    ToggleContent(div, button);
}

function formOpen() { }
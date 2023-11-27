(function () {
    (window.jQuery || document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>'));

})();

function runAfterEverythingElse() {

}


_spBodyOnLoadFunctionNames.push("runAfterEverythingElse");

function PreSaveAction() {

    var isFileValid=true;

    var documentField = getField('AICDocumentUrl','UrlFieldUrl');
    var documentValue = documentField.val();

    if ( documentValue.trim().toLowerCase()!='http://' && documentValue.trim().toLowerCase()!='https://' && documentValue.trim()!='' )
    {

        var fileExt= documentValue.split('.').pop();
        if (fileExt.toLowerCase() != "pdf" && fileExt.toLowerCase() != "doc" && fileExt.toLowerCase() != "docx")
        {
            if ($('#fileValidation').length) {
                isFileValid=false;
            }
            else
            {
                var errorHtml;
                if (_spPageContextInfo.currentLanguage === 1025) {
                    errorHtml = '<span class="ms-formvalidation" id="fileValidation"><span role="alert"><br> الملفات المسموحة Word, PDF</span></span>';
                } else {
                    errorHtml = '<span class="ms-formvalidation" id="fileValidation"><span role="alert"><br>Allowed files are word and PDF only.</span></span>';
                }
                documentField.parent().append(errorHtml);
                isFileValid=false;
            }
        }
        else{
            if ($('#fileValidation').length) {
                $('#fileValidation').remove();
            }
            isFileValid=true;
        }
    }

    
    //SPClientForms.ClientFormManager.SubmitClientForm('WPQ1');
    if (isFileValid ) {
        return true;
    }
    else {
        return false;
    }
}

function getField(fieldName, fieldType) {
    var control = $('[id^=' + fieldName + '_][id$=' + fieldType );
    return control;
}

function removeValidation(validation) {
    if ($('#' + validation + '').length) {
        $('#' + validation + '').remove();
    }
}

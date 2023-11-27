
document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>');
document.write('<script src="/_catalogs/masterpage/AIC/js/jquery.SPServices.min.js"><\/script>');

function runAfterEverythingElse() {
    $('[id^=Title][id$=TextField]').prop("readonly", true);
    $('[id^=AICEmail][id$=TextField]').prop("readonly", true);
    $('[id^=AICPhoneNumber][id$=TextField]').prop("readonly", true);
    $('[id^=AICDate][id$=DateTimeFieldDate]').prop("readonly", true);
    $( "td.ms-dtinput > a" ).attr('onclick','').unbind('click');
    $('[id^=Country][id$=LookupField]').attr("style", "pointer-events: none;");
    $('[id^=AICCity][id$=LookupField]').attr("style", "pointer-events: none;");
    $('[id^=AICWorkEducationalOrganization][id$=TextField]').prop("readonly", true);
    $('[id^=AICMessage][id$=TextField]').prop("readonly", true);
    $('[id^=AICIsReplySent][id$=BooleanField]').parent().parent().parent().attr('style','display:none')
}


_spBodyOnLoadFunctionNames.push("runAfterEverythingElse");

function PreSaveAction() {

    var isReplyValid=true;

    var replyField = getField('AICReply','TextField');
    var replyValue = replyField.val();

    if ( replyValue.trim()=='' )
    {
            if ($('#replyValidation').length) {
                isReplyValid=false;
            }
            else
            {
                var errorHtml;
                if (_spPageContextInfo.currentLanguage === 1025) {
                    errorHtml = '<span class="ms-formvalidation" id="replyValidation"><span role="alert"><br> لا يمكنك ترك هذا الحقل فارغاً</span></span>';
                } else {
                    errorHtml = '<span class="ms-formvalidation" id="replyValidation"><span role="alert"><br>You can\'t leave this blank.</span></span>';
                }
                replyField.parent().append(errorHtml);
                isReplyValid=false;
            }
        
    }
    else{
        if ($('#replyValidation').length) {
            $('#replyValidation').remove();
        }
        isReplyValid=true;
    }

    
    //SPClientForms.ClientFormManager.SubmitClientForm('WPQ1');
    if (isReplyValid ) {
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

(function () {
    (window.jQuery || document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>'));

})();

function runAfterEverythingElse() {
    

    if ($('[id^=AICMenuURL][id$=LookupField]').get(0)) {
        $('[id^=AICMenuURL][id$=LookupField]').off('change');
        $('[id^=AICMenuURL][id$=LookupField]').on('change', function (e) {
            urlChanged();
        });
    }

    if ($('[id^=AICHasChildren][id$=BooleanField').get(0)) {
        $('[id^=AICHasChildren][id$=BooleanField]').off('change');
        $('[id^=AICHasChildren][id$=BooleanField]').on('change', function (e) {
            hasChildrenChanged();
        });
    }
    showHideUrls();
}

function urlChanged() {
    $('[id^=AICUrl][id$=UrlFieldUrl]').val('http://');
    $('[id^=AICUrl][id$=UrlFieldDescription]').val('');

    
    if ($('#otherUrlValidation').length) {
        $('#otherUrlValidation').remove();
    }
    showHideOther();   
}
function hasChildrenChanged() {
    
    $('[id^=AICMenuURL][id$=LookupField]').val('0');
    $('[id^=AICUrl][id$=UrlFieldUrl]').val('http://');
    $('[id^=AICUrl][id$=UrlFieldDescription]').val('');


    if ($('#otherUrlValidation').length) {
        $('#otherUrlValidation').remove();
    }
    if ($('#urlValidation').length) {
        $('#urlValidation').remove();
    }
    showHideUrls();   
	showHideParent()
}

function showHideUrls()
{
    var hasChildrenChecked= $('[id^=AICHasChildren][id$=BooleanField]').is(":checked")
    var url =getField('AICMenuURL','LookupField');
    var otherUrl =getField('AICUrl','UrlFieldUrl');

    if (hasChildrenChecked)
    {
        hideField(url);
        hideField(otherUrl);
    }
    else 
    {
        showField(url);
        hideField(otherUrl);
    }
    
}

function showHideParent()
{
    var hasChildrenChecked= $('[id^=AICHasChildren][id$=BooleanField]').is(":checked")
    var parent =getField('AICMenuParent','LookupField');

    if (hasChildrenChecked)
    {
        hideField(parent);
    }
    else 
    {
        showField(parent);
    }
    
}
function showHideOther()
{
    var urlValue= $('[id^=AICMenuURL][id$=LookupField] option:selected').text();
    var otherUrl =getField('AICUrl','UrlFieldUrl');

    if (urlValue.toLowerCase().indexOf('other')>-1 )
    {
        showField(otherUrl);
    }
    else 
    {
        hideField(otherUrl);
    }
    
}
function hideField(field){
    field.parent().parent().parent().hide();
}
function showField(field){
    field.parent().parent().parent().show();
}



_spBodyOnLoadFunctionNames.push("runAfterEverythingElse");

function PreSaveAction() {

    var isOtherUrlValid=true;
    var isUrlValid=true;

    var hasChildrenChecked= $('[id^=AICHasChildren][id$=BooleanField]').is(":checked")
    if (hasChildrenChecked)
    {
        isOtherUrlValid=true;
        isUrlValid=true;

        if ($('#otherUrlValidation').length) {
            $('#otherUrlValidation').remove();
        }

        if ($('#urlValidation').length) {
            $('#urlValidation').remove();
        }
    }
    else
    {
        var url =getField('AICMenuURL','LookupField');
        var urlVal = url.val();
        if ( urlVal==0)
        {
            if ($('#urlValidation').length) {
                $('#urlValidation').remove();
            }
            if ($('#otherUrlValidation').length) {
                $('#otherUrlValidation').remove();
            }
        
            var errorHtml;
            if (_spPageContextInfo.currentLanguage === 1025) {
                errorHtml = '<span class="ms-formvalidation" id="otherUrlValidation"><span role="alert"><br> لا يمكنك ترك هذا الحقل فارغاً</span></span>';
            } else {
                errorHtml = '<span class="ms-formvalidation" id="otherUrlValidation"><span role="alert"><br>You can\'t leave this blank.</span></span>';
            }
            url.parent().append(errorHtml);
            isUrlValid = false;
            isOtherUrlValid= true;
        }
        else
        {
            isUrlValid = true;
            var urlValue= $('[id^=AICMenuURL][id$=LookupField] option:selected').text();

            if (urlValue.toLowerCase().indexOf('other')>-1 )
            {
                var otherUrl = getField('AICUrl','UrlFieldUrl');
                var otherUrlValue = otherUrl.val();
                if ( otherUrlValue.trim().toLowerCase()=='http://' ||otherUrlValue.trim().toLowerCase()=='https://' || otherUrlValue.trim()=='' )
                {
                    if ($('#otherUrlValidation').length) {
                        $('#otherUrlValidation').remove();
                    }
                
                    var errorHtml;
                    if (_spPageContextInfo.currentLanguage === 1025) {
                        errorHtml = '<span class="ms-formvalidation" id="otherUrlValidation"><span role="alert"><br> لا يمكنك ترك هذا الحقل فارغاً</span></span>';
                    } else {
                        errorHtml = '<span class="ms-formvalidation" id="otherUrlValidation"><span role="alert"><br>You can\'t leave this blank.</span></span>';
                    }
                    otherUrl.parent().append(errorHtml);
                    isOtherUrlValid = false;
                
    
                }
                else
                { 
                    if (!otherUrlValue.toLowerCase().startsWith('http://') && !otherUrlValue.toLowerCase().startsWith('https://')){
                        var errorHtml;
                        if ($('#otherUrlValidation').length) {
                            $('#otherUrlValidation').remove();
                        }
                        if (_spPageContextInfo.currentLanguage === 1025) {
                            errorHtml = '<span class="ms-formvalidation" id="otherUrlValidation"><span role="alert"><br>Invalid URL: '+ otherUrlValue+ '</span></span>';
                        } else {
                            errorHtml = '<span class="ms-formvalidation" id="otherUrlValidation"><span role="alert"><br>Invalid URL: '+ otherUrlValue+ '.</span></span>';
                        }
                        otherUrl.parent().append(errorHtml);
                        isOtherUrlValid = false;
                    }
                    else
                    {
                        if ($('#otherUrlValidation').length) {
                            $('#otherUrlValidation').remove();
                        }
                        isOtherUrlValid = true;
                    }
    
                    
                }
            }
            else 
            {
                if ($('#otherUrlValidation').length) {
                    $('#otherUrlValidation').remove();
                }
                isOtherUrlValid=true;
            }
        }


        
    }
    //SPClientForms.ClientFormManager.SubmitClientForm('WPQ1');
    if (isOtherUrlValid && isUrlValid) {
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

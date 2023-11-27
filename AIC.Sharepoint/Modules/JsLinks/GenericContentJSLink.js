(function () {
    (window.jQuery || document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>'));

})();

function runAfterEverythingElse() {
    

    if ($('[id^=AICGenericContentType][id$=LookupField]').get(0)) {
        $('[id^=AICGenericContentType][id$=LookupField]').off('change');
        $('[id^=AICGenericContentType][id$=LookupField]').on('change', function (e) {
            typeChanged();
        });
    }
    showHideModelImage();
}

function typeChanged() {
    // $('[id^=AICUrl][id$=UrlFieldUrl]').val('http://');
    // $('[id^=AICUrl][id$=UrlFieldDescription]').val('');

    
    
    showHideModelImage();   
}


function showHideModelImage()
{
    var typeValue= $('[id^=AICGenericContentType][id$=LookupField] option:selected').text();
    var modelImage =$("#AICPartnershipModelImage");

    if (typeValue.toLowerCase().indexOf('aic partnership model')>-1 || typeValue.toLowerCase().indexOf('نموذج شراكة مركز الابتكار التطبيقي')>-1 )
    {
        modelImage.parent().parent().show();
    }
    else 
    {
        modelImage.parent().parent().hide();
    }
    
}



_spBodyOnLoadFunctionNames.push("runAfterEverythingElse");



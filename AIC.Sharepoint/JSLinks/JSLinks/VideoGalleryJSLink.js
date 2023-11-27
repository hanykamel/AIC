
document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>');
document.write('<script src="/_catalogs/masterpage/AIC/js/jquery.SPServices.min.js"><\/script>');

function runAfterEverythingElse() {
    $('[id^=FileLeafRef_]').attr('maxlength', 80);

    if (_spPageContextInfo.currentLanguage === 1025) {
        $('[id^=FileLeafRef_]').parent().append('<span class="ms-metadata">الحد الأقصى هو 80 حرف</span>')
    } else {
        $('[id^=FileLeafRef_]').parent().append('<span class="ms-metadata">The maximum length is 80 charcters</span>')
    }
}
_spBodyOnLoadFunctionNames.push("runAfterEverythingElse");
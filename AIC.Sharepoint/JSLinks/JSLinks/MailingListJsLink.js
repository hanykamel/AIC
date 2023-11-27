(function () {
    (window.jQuery || document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>'));

})();

function PreSaveAction() {
	var url = $('[id^=FileUrl][id$=UrlFieldUrl]');
	var urlValue = url.val();
	if(urlValue.endsWith('.pdf'))
	{
		if ($('#UrlValidation').length) {
                $('#UrlValidation').remove();
            }
		return true;
	}
	else if ( urlValue.trim().toLowerCase()=='http://' ||urlValue.trim().toLowerCase()=='https://' || urlValue.trim()=='' ){
			if ($('#UrlValidation').length) {
                $('#UrlValidation').remove();
        }
		return true;
	}
	else
	{
		if ($('#UrlValidation').length) {
                $('#UrlValidation').remove();
            }
		 var errorHtml;
            if (_spPageContextInfo.currentLanguage === 1025) {
                errorHtml = '<span class="ms-formvalidation" id="UrlValidation"><span role="alert"><br> يجب ان يكون الملف pdf</span></span>';
            } else {
                errorHtml = '<span class="ms-formvalidation" id="UrlValidation"><span role="alert"><br>file must be only pdf</span></span>';
            }
            url.parent().append(errorHtml);
			return false;
	}
}

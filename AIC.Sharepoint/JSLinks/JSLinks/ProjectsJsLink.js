(function () {
    (window.jQuery || document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>'));

})();

function PreSaveAction() {
	var technologyDomain =getField('AICTechnologyDomain','LookupField');
 if($('[id^=AICApplicationDomain][id$=LookupField]').val() == '0' && $('[id^=AICTechnologyDomain][id$=LookupField]').val() == '0')
 {
    var errorHtml;
            if (_spPageContextInfo.currentLanguage === 1025) {
                errorHtml = '<span class="ms-formvalidation" id="domainValidation"><span role="alert"><br> عفوا يجب الاختيار من المجال التطبيقي او من مجال التكنولوجيا</span></span>';
            } else {
                errorHtml = '<span class="ms-formvalidation" id="domainValidation"><span role="alert"><br>must choose Application domain or Technology domain</span></span>';
            }
            technologyDomain.parent().append(errorHtml);
			
			return false;
           
 }
 else{
	  if ($('#domainValidation').length) {
            $('#domainValidation').remove();
        }
		return true;
 }
}

function getField(fieldName, fieldType) {
    var control = $('[id^=' + fieldName + '_][id$=' + fieldType );
    return control;
}
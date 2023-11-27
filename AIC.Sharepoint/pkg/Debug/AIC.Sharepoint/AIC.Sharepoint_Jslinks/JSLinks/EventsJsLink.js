(function () {
    (window.jQuery || document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>'));

})();

function PreSaveAction() {
	var date = getField('AICEventEndDate','DateTimeFieldDate');
	var endDate = $('[id^=AICEventEndDate][id$=DateTimeFieldDate]').val();
	var startDate = $('[id^=AICDate][id$=DateTimeFieldDate]').val();
 if(new Date(endDate.split('/')[2],endDate.split('/')[1],endDate.split('/')[0]) < new Date(startDate.split('/')[2],startDate.split('/')[1],startDate.split('/')[0]))
 {
    var errorHtml;
            if (_spPageContextInfo.currentLanguage === 1025) {
                errorHtml = '<span class="ms-formvalidation" id="dateValidation"><span role="alert"><br> يجب أن يكون تاريخ الانتهاء بعد تاريخ البدء</span></span>';
            } else {
                errorHtml = '<span class="ms-formvalidation" id="dateValidation"><span role="alert"><br>End Date must be after the Start Date</span></span>';
            }
            date.parent().append(errorHtml);
			
			return false;
           
 }
 else{
	  if ($('#dateValidation').length) {
            $('#dateValidation').remove();
        }
		return true;
 }
}

function getField(fieldName, fieldType) {
    var control = $('[id^=' + fieldName + '_][id$=' + fieldType );
    return control;
}